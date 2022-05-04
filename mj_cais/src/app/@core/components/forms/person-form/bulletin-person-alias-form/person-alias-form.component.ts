import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { PersonAliasForm } from "./_models/person-alias.form";
import { BaseNomenclatureModel } from "../../../../models/nomenclature/base-nomenclature.model";
import {
  IgxDialogComponent,
  IgxGridComponent,
  IgxGridRowComponent,
} from "@infragistics/igniteui-angular";
import { PersonAliasModel } from "../../../../models/common/person-alias.model";

@Component({
  selector: "cais-person-alias-form",
  templateUrl: "./person-alias-form.component.html",
  styleUrls: ["./person-alias-form.component.scss"],
})
export class PersonAliasFormComponent implements OnInit {
  @Input() transactions: any;
  @Input() gridData: PersonAliasModel[];
  @Input() isGridEditable: boolean;
  @Input() personAliasTypes: BaseNomenclatureModel[];

  public personAliasForm = new PersonAliasForm();

  @ViewChild("personAliasGrid", { read: IgxGridComponent })
  public personAliasGrid: IgxGridComponent;

  @ViewChild("personAliasDialog", { read: IgxDialogComponent })
  public personAliasDialog: IgxDialogComponent;

  constructor() {}

  ngOnInit(): void {}

  public onOpenEditPersonAlias(event: IgxGridRowComponent) {
    this.personAliasForm.group.patchValue(event.rowData);
    this.personAliasDialog.open();
  }

  public onDeletePersonAlias(event: IgxGridRowComponent) {
    this.personAliasGrid.deleteRow(event.rowData.id);
    this.personAliasGrid.data = this.personAliasGrid.data.filter(
      (d) => d.id != event.rowData.id
    );

    // aggregate transaction
    let personAliasTransactions =
      this.personAliasGrid.transactions.getAggregatedChanges(true);

    this.transactions.setValue(personAliasTransactions);
  }

  onAddOrUpdatePersonAliasRow() {
    if (!this.personAliasForm.group.valid) {
      this.personAliasForm.group.markAllAsTouched();
      return;
    }

    if (this.personAliasForm.typeId.value) {
      var typeObj = (this.personAliasTypes as any).find(
        (x) => x.id === this.personAliasForm.typeId.value
      );

      this.personAliasForm.typeName.patchValue(typeObj?.name);
      this.personAliasForm.typeCode.patchValue(typeObj?.code);
    }

    let currentRow = this.personAliasGrid.getRowByKey(
      this.personAliasForm.id.value
    );

    if (currentRow) {
      currentRow.update(this.personAliasForm.group.value);
    } else {
      this.personAliasGrid.addRow(this.personAliasForm.group.value);
    }

    this.onClosePersonAliasDilog();

    let personAliasTransactions =
      this.personAliasGrid.transactions.getAggregatedChanges(true);

    this.transactions.setValue(personAliasTransactions);
  }

  onClosePersonAliasDilog() {
    this.personAliasForm = new PersonAliasForm();
    this.personAliasDialog.close();
  }
}
