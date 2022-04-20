import { Component, Input, ViewChild } from "@angular/core";
import {
  IgxDialogComponent,
  IgxGridComponent,
  IgxGridRowComponent,
} from "@infragistics/igniteui-angular";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { BulletinSanctionForm } from "./_models/bulletin-sanction.form";

@Component({
  selector: "cais-bulletin-sanctions-form",
  templateUrl: "./bulletin-sanctions-form.component.html",
  styleUrls: ["./bulletin-sanctions-form.component.scss"],
})
export class BulletinSanctionsFormComponent {
  @Input() bulletinSanctionsTransactions: string;
  @Input() dbData: any;
  @Input() isForPreview: boolean;

  @ViewChild("sanctionsGrid", {
    read: IgxGridComponent,
  })
  public sanctionGrid: IgxGridComponent;

  @ViewChild("sanctionDialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  public bulletinSanctionForm = new BulletinSanctionForm();
  public showProbationData: boolean = false;

  private probationCode = "nkz_probacia";
  constructor(public dateFormatService: DateFormatService) {}

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

    if (currentRow) {
      currentRow.update(this.bulletinSanctionForm.group.value);
    } else {
      this.sanctionGrid.addRow(this.bulletinSanctionForm.group.value);
    }

    this.onCloseSanctionDilog();
  }

  public onOpenEditSanction(event: IgxGridRowComponent) {
    this.bulletinSanctionForm.group.patchValue(event.rowData);
    this.showProbationData =
      this.bulletinSanctionForm.sanctCategoryId.value == this.probationCode;

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

  private GetNameById(data: any, id: string) {
    if (id) {
      let name = data.find((x) => x.id === id).name;
      return name;
    }

    return null;
  }
}
