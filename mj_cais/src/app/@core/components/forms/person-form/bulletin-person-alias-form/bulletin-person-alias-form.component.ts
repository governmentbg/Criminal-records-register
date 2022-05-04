import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { BulletinPersonAliasForm } from "./_models/bulletin-person-alias.form";
import { BulletinPersonAliasModel } from "../../../shared/bulletin-person-info/_models/bulletin-person-alias.model";
import { BaseNomenclatureModel } from "../../../../models/nomenclature/base-nomenclature.model";
import {
  IgxDialogComponent,
  IgxGridComponent,
  IgxGridRowComponent,
} from "@infragistics/igniteui-angular";

@Component({
  selector: "cais-bulletin-person-alias-form",
  templateUrl: "./bulletin-person-alias-form.component.html",
  styleUrls: ["./bulletin-person-alias-form.component.scss"],
})
export class BulletinPersonAliasFormComponent implements OnInit {
  @Input() transactions: any;
  @Input() gridData: BulletinPersonAliasModel[];
  @Input() isGridEditable: boolean;
  @Input() personAliasTypes: BaseNomenclatureModel[];

  public bulletinPersonAliasForm = new BulletinPersonAliasForm();

  @ViewChild("bulletinPersonAliasGrid", { read: IgxGridComponent })
  public bulletinPersonAliasGrid: IgxGridComponent;

  @ViewChild("bulletinPersonAliasDialog", { read: IgxDialogComponent })
  public bulletinPersonAliasDialog: IgxDialogComponent;

  constructor() {}

  ngOnInit(): void {}

  public onOpenEditBulletinPersonAlias(event: IgxGridRowComponent) {
    this.bulletinPersonAliasForm.group.patchValue(event.rowData);
    this.bulletinPersonAliasDialog.open();
  }

  public onDeleteBulletinPersonAlias(event: IgxGridRowComponent) {
    this.bulletinPersonAliasGrid.deleteRow(event.rowData.id);
    this.bulletinPersonAliasGrid.data =
      this.bulletinPersonAliasGrid.data.filter((d) => d.id != event.rowData.id);

    // aggregate transaction
    let personAliasTransactions =
      this.bulletinPersonAliasGrid.transactions.getAggregatedChanges(true);

    this.transactions.setValue(personAliasTransactions);
  }

  onAddOrUpdateBulletinPersonAliasRow() {
    if (!this.bulletinPersonAliasForm.group.valid) {
      this.bulletinPersonAliasForm.group.markAllAsTouched();
      return;
    }

    if (this.bulletinPersonAliasForm.typeId.value) {
      var typeObj = (this.personAliasTypes as any).find(
        (x) => x.id === this.bulletinPersonAliasForm.typeId.value
      );

      this.bulletinPersonAliasForm.typeName.patchValue(typeObj?.name);
      this.bulletinPersonAliasForm.typeCode.patchValue(typeObj?.code);
    }

    let currentRow = this.bulletinPersonAliasGrid.getRowByKey(
      this.bulletinPersonAliasForm.id.value
    );

    if (currentRow) {
      currentRow.update(this.bulletinPersonAliasForm.group.value);
    } else {
      this.bulletinPersonAliasGrid.addRow(
        this.bulletinPersonAliasForm.group.value
      );
    }

    this.onCloseBulletinPersonAliasDilog();

    let personAliasTransactions =
      this.bulletinPersonAliasGrid.transactions.getAggregatedChanges(true);

    this.transactions.setValue(personAliasTransactions);
  }

  onCloseBulletinPersonAliasDilog() {
    this.bulletinPersonAliasForm = new BulletinPersonAliasForm();
    this.bulletinPersonAliasDialog.close();
  }
}
