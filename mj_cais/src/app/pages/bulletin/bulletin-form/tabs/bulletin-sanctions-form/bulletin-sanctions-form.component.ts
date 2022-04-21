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

  onAddOrUpdateSanctionRow() {
    if (!this.bulletinSanctionForm.group.valid) {
      this.bulletinSanctionForm.group.markAllAsTouched();
      return;
    }

    this.bulletinSanctionForm.sanctCategoryName.patchValue(
      this.GetNameById(
        this.dbData.sanctionCategories,
        this.bulletinSanctionForm.sanctCategoryId.value
      )
    );

    this.bulletinSanctionForm.ecrisSanctCategName.patchValue(
      this.GetNameById(
        this.dbData.ecrisSanctionCategories,
        this.bulletinSanctionForm.ecrisSanctCategId.value
      )
    );

    let currentRow = this.sanctionGrid.getRowByKey(
      this.bulletinSanctionForm.id.value
    );

    // data used only for vizualization in grid
    let allSavedProbationsData = this.bulletinSanctionForm.probations.value;

    // saved transactions
    let allSavedTransactions =
      this.bulletinSanctionForm.probationsTransactions.value;

    let newProbTransactions =
      this.gridBulletinProbation.transactions.getAggregatedChanges(true);

      newProbTransactions.forEach((newTransaction) => {
      debugger;
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
            (x) => x.id == newTransaction.id
          );
        }
      }

      if (newTransaction.type == TransactionType.DELETE) {
        // when has delete a new local element
        let tryToDeleteAddedEl = allSavedTransactions.some(
          (x) => x.id == newTransaction.id && x.type == TransactionType.ADD
        );

        if (tryToDeleteAddedEl) {
          allSavedTransactions = allSavedTransactions.filter(
            (x) => x.id != newTransaction.id
          );

          // ignore this transaction
          return;
        }

        // when delete element from server
        // remove it from collection but save transaction
        allSavedProbationsData = allSavedProbationsData.filter(
          (x) => x.id == newTransaction.id
        );
      }

      // when has transaction type added
      allSavedTransactions.push(newTransaction);
    });

    this.bulletinSanctionForm.probationsTransactions.patchValue(
      allSavedTransactions
    );

    // grid data
    // used only for visualization in dialog
    let probationsObjToAdd = allSavedProbationsData;
    // add element from transaction only from type new or update
    allSavedTransactions.forEach((element) => {
      if (
        element.type == TransactionType.ADD ||
        element.type == TransactionType.UPDATE
      ) {
        probationsObjToAdd.push(element.newValue);
      }
    });

    this.bulletinSanctionForm.probations.patchValue(probationsObjToAdd);

    // remove data from probation grid and its transactions
    // after save it in sanction grid 
    this.gridBulletinProbation.data = [];
    this.gridBulletinProbation.transactions.clear();

    if (currentRow) {
      currentRow.update(this.bulletinSanctionForm.group.value);
    } else {
      this.sanctionGrid.addRow(this.bulletinSanctionForm.group.value);
    }

    this.onCloseSanctionDilog();
  }

  public onOpenAddSanction() {
    // clear grid and old transactions
    this.gridBulletinProbation.data = [];
    this.gridBulletinProbation.transactions.clear();
    this.dialog.open();
  }
  public onOpenEditSanction(event: IgxGridRowComponent) {
    debugger;

    let sanctionProbations = event.rowData.probations;
    //let sanctionTransactions = event.rowData.probationsTransactions;

    // get all probation that are not deleted
    // let probationToAddedToGrid = [];
    // sanctionProbations.forEach((currentProbation) => {
    //   let skip = sanctionTransactions.some(
    //     (x) => x.id == currentProbation.id && (x.type == "delete" || x.type == "update")
    //   );
    //   if (!skip) {
    //     probationToAddedToGrid.push(currentProbation);
    //   }
    // });
    debugger;
    this.gridBulletinProbation.data = sanctionProbations;

    // if (
    //   event.rowData.probationsTransactions &&
    //   event.rowData.probationsTransactions.length > 0
    // ) {
    //   this.gridBulletinProbation.transactions.clear();
    //   debugger;

    //   event.rowData.probationsTransactions.forEach((transaction) => {
    //     if (transaction.type === "delete") {
    //       // dummy transaction
    //       let addedTransactionForDeleteState = {
    //         id: transaction.id,
    //         newValue: null,
    //         type: TransactionType.ADD,
    //       };

    //       this.gridBulletinProbation.transactions.add(
    //         addedTransactionForDeleteState
    //       );
    //     }

    //     this.gridBulletinProbation.transactions.add(transaction);
    //   });
    // } else {
    //   this.gridBulletinProbation.transactions.clear();
    // }

    this.bulletinSanctionForm.group.patchValue(event.rowData);
    // this.showProbationData =
    //   this.bulletinSanctionForm.sanctCategoryId.value == this.probationCode;

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

  public getSanctionProbMeasureNameById(arr: any): string {
    return this.sanctionProbMeasuresOptions.find((s) => s.id === arr)?.name;
  }

  public onGridOptionChange(id: number, cell: IgxGridCellComponent) {
    cell.value = id;
    cell.editValue = id;
  }

  private GetNameById(data: any, id: string) {
    if (id) {
      let name = data.find((x) => x.id === id).name;
      return name;
    }

    return null;
  }
}
