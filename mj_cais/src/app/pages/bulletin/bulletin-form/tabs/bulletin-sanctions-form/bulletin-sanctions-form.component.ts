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

  private probationCode = "nkz_probacia";

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
    // clear grid and old transactions
    this.clearProbationData();
    this.dialog.open();
  }

  public onOpenEditSanction(event: IgxGridRowComponent) {
    let sanctionProbations = event.rowData.probations;
    this.gridBulletinProbation.data = sanctionProbations;
    this.bulletinSanctionForm.group.patchValue(event.rowData);
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
    this.showProbationData = selectedElement == this.probationCode;
    // todo:
  }

  public getSanctionCategoryNameById(arr: any): string {
    return this.sanctionProbCategoriesOptions.find((s) => s.id === arr)?.name;
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

    // remove data from probation grid and its transactions
    // after save it in sanction grid
    this.clearProbationData();
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
