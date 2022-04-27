import { Component, Input, OnInit, ViewChild } from "@angular/core";
import {
  IgxDialogComponent,
  IgxGridCellComponent,
  IgxGridComponent,
  IgxGridRowComponent,
  TransactionType,
} from "@infragistics/igniteui-angular";
import { BaseNomenclatureModel } from "../../../../../@core/models/nomenclature/base-nomenclature.model";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { SanctionCategoryType } from "../../_models/sanction-category-type-constants";
import { BulletinSanctionForm } from "./_models/bulletin-sanction.form";

@Component({
  selector: "cais-bulletin-sanctions-form",
  templateUrl: "./bulletin-sanctions-form.component.html",
  styleUrls: ["./bulletin-sanctions-form.component.scss"],
})
export class BulletinSanctionsFormComponent implements OnInit {
  @Input() bulletinSanctionsTransactions: string;
  @Input() dbData: any;
  @Input() isForPreview: boolean;

  @ViewChild("sanctionsGrid", {
    read: IgxGridComponent,
  })
  public sanctionGrid: IgxGridComponent;

  @ViewChild("sanctionDialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  @ViewChild("gridBulletinProbation", {
    read: IgxGridComponent,
  })
  public gridBulletinProbation: IgxGridComponent;

  public bulletinSanctionForm = new BulletinSanctionForm();

  public showProbationData: boolean = false;
  public showFineData: boolean = false;
  public showPrisonData: boolean = false;
  public isSanctionPreview: boolean = false;
  public probations = [];

  public sanctionProbCategoriesOptions: BaseNomenclatureModel[];
  public sanctionProbMeasuresOptions: BaseNomenclatureModel[];

  constructor(public dateFormatService: DateFormatService) {}

  ngOnInit(): void {
    this.sanctionProbCategoriesOptions = this.dbData.sanctionProbCategories;
    this.sanctionProbMeasuresOptions = this.dbData.sanctionProbMeasures;
  }

  ngAfterViewInit(): void {
    // todo:
    this.gridBulletinProbation.resourceStrings.igx_grid_snackbar_addrow_label =
      "Добавен нов запис";
    this.gridBulletinProbation.resourceStrings.igx_grid_add_row_label =
      "Добави";
  }

  onAddOrUpdateSanctionRow() {
    if (!this.bulletinSanctionForm.group.valid) {
      this.bulletinSanctionForm.group.markAllAsTouched();
      return;
    }

    this.bulletinSanctionForm.sanctCategoryName.patchValue(
      this.getNameById(
        this.dbData.sanctionCategories,
        this.bulletinSanctionForm.sanctCategoryId.value
      )
    );

    this.bulletinSanctionForm.ecrisSanctCategName.patchValue(
      this.getNameById(
        this.dbData.ecrisSanctionCategories,
        this.bulletinSanctionForm.ecrisSanctCategId.value
      )
    );

    let currentRow = this.sanctionGrid.getRowByKey(
      this.bulletinSanctionForm.id.value
    );

    // todo: check if selected category is probation
    this.updateProbationsTransactionData();

    if (currentRow) {
      currentRow.update(this.bulletinSanctionForm.group.value);
    } else {
      this.sanctionGrid.addRow(this.bulletinSanctionForm.group.value);
    }

    this.onCloseSanctionDilog();
  }

  public onOpenAddSanction() {
    this.bulletinSanctionForm.group.enable();
    this.isSanctionPreview = false;
    // clear grid and old transactions
    this.clearProbationData();
    this.showOrHidBySanctionCategory("");
    this.dialog.open();
  }

  public onOpenEditSanction(event: IgxGridRowComponent) {
    this.bulletinSanctionForm.group.enable();
    this.isSanctionPreview = false;
    this.updateDialogData(event);
    this.dialog.open();
  }

  public onOpenPreviewSanction(event: IgxGridRowComponent) {
    this.bulletinSanctionForm.group.disable();
    this.isSanctionPreview = true;
    this.updateDialogData(event);
    this.dialog.open();
  }

  public onDeleteSanction(event: IgxGridRowComponent) {
    this.sanctionGrid.deleteRow(event.rowData.id);
    this.sanctionGrid.data = this.sanctionGrid.data.filter(
      (d) => d.id != event.rowData.id
    );
  }

  public onCloseSanctionDilog() {
    this.bulletinSanctionForm = new BulletinSanctionForm();
    this.dialog.close();
  }

  public onSanctionCategoryChange(selectedElement) {
    this.showOrHidBySanctionCategory(selectedElement);
  }

  public getSanctionCategoryNameById(arr: any): string {
    return this.sanctionProbCategoriesOptions.find((s) => s.id === arr)?.name;
  }

  private updateDialogData(event: IgxGridRowComponent) {
    let sanctionProbations = event.rowData.probations;
    this.gridBulletinProbation.data = sanctionProbations;
    this.bulletinSanctionForm.group.patchValue(event.rowData);

    this.showOrHidBySanctionCategory(event.rowData.sanctCategoryId);
  }

  //#region Probations actions

  private updateProbationsTransactionData() {
    // data used only for vizualization in grid
    let allSavedProbationsData =
      this.bulletinSanctionForm.probations.value ?? [];

    // saved transactions
    let allSavedTransactions =
      this.bulletinSanctionForm.probationsTransactions.value ?? [];

    let newProbTransactions =
      this.gridBulletinProbation.transactions.getAggregatedChanges(true);

    newProbTransactions.forEach((newTransaction) => {
      if (newTransaction.type == TransactionType.UPDATE) {
        // when has update on a new local element
        let tryToUpdateAddedEl = allSavedTransactions.some(
          (x) => x.id == newTransaction.id && x.type == TransactionType.ADD
        );

        // remove old transaction
        // make new transaction in status added
        if (tryToUpdateAddedEl) {
          allSavedTransactions = allSavedTransactions.filter(
            (x) => x.id != newTransaction.id
          );
          newTransaction.type = TransactionType.ADD;
        } else {
          // when update element from server
          // remove it from collection but save transaction
          allSavedProbationsData = allSavedProbationsData.filter(
            (x) => x.id != newTransaction.id
          );
        }
      }

      if (newTransaction.type == TransactionType.DELETE) {
        // when has delete a new local element
        let tryToDeleteAddedEl = allSavedTransactions.some(
          (x) => x.id == newTransaction.id && x.type == TransactionType.ADD
        );

        // when delete element remove it from collection
        allSavedProbationsData = allSavedProbationsData.filter(
          (x) => x.id != newTransaction.id
        );

        if (tryToDeleteAddedEl) {
          allSavedTransactions = allSavedTransactions.filter(
            (x) => x.id != newTransaction.id
          );

          // ignore this transaction
          return;
        }
      }

      // when has transaction type added
      // or delete element from server
      allSavedTransactions.push(newTransaction);
    });

    this.bulletinSanctionForm.probationsTransactions.patchValue(
      allSavedTransactions
    );

    // grid data
    // used only for visualization in dialog
    let probationsObjToAdd = allSavedProbationsData;
    // add element from transaction only from type new or update
    // and element does not exist
    allSavedTransactions.forEach((element) => {
      let isAddOrUpdate =
        element.type == TransactionType.ADD ||
        element.type == TransactionType.UPDATE;
      let exist = probationsObjToAdd.some((x) => x.id == element.id);
      if (isAddOrUpdate && !exist) {
        probationsObjToAdd.push(element.newValue);
      }
    });

    this.bulletinSanctionForm.probations.patchValue(probationsObjToAdd);

    let selectedCategoryType = this.getSelectedCategoryType(
      this.bulletinSanctionForm.sanctCategoryId.value
    );

    if (selectedCategoryType != SanctionCategoryType.Probation) {
      this.updateTransactionWhenCategoryIsNotProbation(
        allSavedTransactions,
        probationsObjToAdd
      );
    }

    if (selectedCategoryType != SanctionCategoryType.Fine) {
      this.bulletinSanctionForm.fineAmount.patchValue(null);
    }

    if (selectedCategoryType != SanctionCategoryType.Prison) {
      this.bulletinSanctionForm.suspentionDurationYears.patchValue(null);
      this.bulletinSanctionForm.suspentionDurationMonths.patchValue(null);
      this.bulletinSanctionForm.suspentionDurationDays.patchValue(null);
    }

    // remove data from probation grid and its transactions
    // after save it in sanction grid
    this.clearProbationData();
  }

  private updateTransactionWhenCategoryIsNotProbation(
    allSavedTransactions,
    probationsObjToAdd
  ) {
    debugger;
    let deleteTransactions = [];
    allSavedTransactions.forEach((savedTransaction) => {
      // when update saved element in db
      let updateOrDeleteElementFromDb =
        (savedTransaction.type == TransactionType.UPDATE ||
          savedTransaction.type == TransactionType.DELETE) &&
        !allSavedTransactions.some(
          (x) => x.id == savedTransaction.id && x.type == TransactionType.ADD
        );

      if (updateOrDeleteElementFromDb) {
        // must add delete transaction
        let deleteTransaction = {
          id: savedTransaction.id,
          newValue: null,
          type: TransactionType.DELETE,
        };
        deleteTransactions.push(deleteTransaction);
      }
    });

    // if has saved element
    probationsObjToAdd.forEach((probationToBeAdd) => {
      let isNotDeleted =
        deleteTransactions.length == 0 ||
        !deleteTransactions.some((x) => x.id == probationToBeAdd.id);

      // if element is not deleted or added locally
      if (
        isNotDeleted &&
        !allSavedTransactions.some(
          (x) => x.id == probationToBeAdd.id && x.type == TransactionType.ADD
        )
      ) {
        let deleteTransaction = {
          id: probationToBeAdd.id,
          newValue: null,
          type: TransactionType.DELETE,
        };
        deleteTransactions.push(deleteTransaction);
      }
    });

    // element to be deleted from db
    this.bulletinSanctionForm.probationsTransactions.patchValue(
      deleteTransactions
    );

    // clear saved grid data used for visualization
    this.bulletinSanctionForm.probations.patchValue([]);
  }

  public onSanctionProbRowDeleted(rowContext) {
    // when delete element from server or locally added
    // but saved in sanctions grid
    if (rowContext && rowContext.data && rowContext.data.id) {
      // dummy transaction
      let addedTransactionForDeleteState = {
        id: rowContext.data.id,
        newValue: null,
        type: TransactionType.ADD,
      };

      // first add transaction for this element
      this.gridBulletinProbation.transactions.add(
        addedTransactionForDeleteState
      );

      // then delete element from grid
      this.gridBulletinProbation.data = this.gridBulletinProbation.data.filter(
        (x) => x.id != rowContext.data.id
      );

      // update collection used for visualization
      let savedProbations = this.bulletinSanctionForm.probations.value;
      let filteredProbations = savedProbations.filter(
        (x) => x.id != rowContext.data.id
      );
      this.bulletinSanctionForm.probations.patchValue(filteredProbations);

      let savedTransactions =
        this.bulletinSanctionForm.probationsTransactions.value;

      // check if delete locally added element
      let existingLocallyAdded = savedTransactions.some(
        (x) => x.id == rowContext.data.id && x.type == TransactionType.ADD
      );

      // if element is locally added and deleted remove transactions
      if (existingLocallyAdded) {
        savedTransactions = savedTransactions.filter(
          (x) => x.id != rowContext.data.id
        );
      } else {
        // when delete lement saved in db
        // save transaction
        savedTransactions.push({
          id: rowContext.data.id,
          newValue: null,
          type: TransactionType.DELETE,
        });
      }

      this.bulletinSanctionForm.probationsTransactions.patchValue(
        savedTransactions
      );
    }
  }

  public addNewProbation() {
    this.gridBulletinProbation.gridAPI.grid.beginAddRowByIndex(null, -1, false);
  }

  private clearProbationData() {
    this.gridBulletinProbation.data = [];
    this.gridBulletinProbation.transactions.clear();
  }

  public getSanctionProbMeasureNameById(arr: any): string {
    return this.sanctionProbMeasuresOptions.find((s) => s.id === arr)?.name;
  }

  //#endregion

  //#region Helpers

  private showOrHidBySanctionCategory(sanctionCategoryType: string) {
    let selectedType = this.getSelectedCategoryType(sanctionCategoryType);

    this.showProbationData = selectedType == SanctionCategoryType.Probation;
    this.showFineData = selectedType == SanctionCategoryType.Fine;
    this.showPrisonData = selectedType == SanctionCategoryType.Prison;
  }

  private getSelectedCategoryType(sanctionCategoryId: string): string {
    let selectedType =
      this.dbData.sanctionCategories.filter(
        (x) => x.code == sanctionCategoryId
      )[0]?.type ?? "";

    return selectedType;
  }

  public onGridOptionChange(id: number, cell: IgxGridCellComponent) {
    cell.value = id;
    cell.editValue = id;
  }

  private getNameById(data: any, id: string) {
    if (id) {
      let name = data.find((x) => x.id === id).name;
      return name;
    }

    return null;
  }

  //#endregion
}
