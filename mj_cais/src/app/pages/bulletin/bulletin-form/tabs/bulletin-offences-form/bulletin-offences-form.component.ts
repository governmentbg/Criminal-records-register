import { Component, Input, OnInit, ViewChild } from "@angular/core";
import {
  IgxDialogComponent,
  IgxGridComponent,
  IgxGridRowComponent,
} from "@infragistics/igniteui-angular";
import { BulletinOffenceForm } from "./models/bulletin-offance.form";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { InputTypeConstants } from "../../../../../@core/constants/input-type.constants";

@Component({
  selector: "cais-bulletin-offences-form",
  templateUrl: "./bulletin-offences-form.component.html",
  styleUrls: ["./bulletin-offences-form.component.scss"],
})
export class BulletinOffencesFormComponent implements OnInit {
  @Input() bulletinOffenceTransactions: string;
  @Input() dbData: any;
  @Input() isForPreview: boolean;
  public InputTypeConstants = InputTypeConstants;

  @ViewChild("offencesGrid", {
    read: IgxGridComponent,
  })
  public offencesGrid: IgxGridComponent;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  public bulletinOffenceForm = new BulletinOffenceForm();

  constructor(public dateFormatService: DateFormatService) {}

  ngOnInit(): void {}

  public onOpenEditBulletinOffence(event: IgxGridRowComponent) {
    this.bulletinOffenceForm.group.patchValue(event.rowData);
    this.dialog.open();
  }

  public onDeleteBulletinOffence(event: IgxGridRowComponent) {
    this.offencesGrid.deleteRow(event.rowData.id);
    this.offencesGrid.data = this.offencesGrid.data.filter(
      (d) => d.id != event.rowData.id
    );
  }

  onAddOrUpdateBulletineOffenceRow() {
    if (!this.bulletinOffenceForm.group.valid) {
      this.bulletinOffenceForm.group.markAllAsTouched();
      return;
    }

    if (this.bulletinOffenceForm.offenceCatId.value) {
      let offenceCatName = (this.dbData.offencesCategories as any).find(
        (x) => x.id === this.bulletinOffenceForm.offenceCatId.value
      ).name;
      this.bulletinOffenceForm.offenceCatName.patchValue(offenceCatName);
    }

    if (this.bulletinOffenceForm.ecrisOffCatId.value) {
      let ecrisOffCatName = (this.dbData.ecrisOffCategories as any).find(
        (x) => x.id === this.bulletinOffenceForm.ecrisOffCatId.value
      ).name;

      this.bulletinOffenceForm.ecrisOffCatName.patchValue(ecrisOffCatName);
    }

    if (this.bulletinOffenceForm.offLvlComplId.value) {
      let offLvlComplName = (this.dbData.completions as any).find(
        (x) => x.id === this.bulletinOffenceForm.offLvlComplId.value
      ).name;

      this.bulletinOffenceForm.offLvlComplName.patchValue(offLvlComplName);
    }

    if (this.bulletinOffenceForm.offLvlPartId.value) {
      let offLvlPartName = (this.dbData.parts as any).find(
        (x) => x.id === this.bulletinOffenceForm.offLvlPartId.value
      ).name;

      this.bulletinOffenceForm.offLvlPartName.patchValue(offLvlPartName);
    }

    let currentRow = this.offencesGrid.getRowByKey(
      this.bulletinOffenceForm.id.value
    );

    if (currentRow) {
      currentRow.update(this.bulletinOffenceForm.group.value);
    } else {
      this.offencesGrid.addRow(this.bulletinOffenceForm.group.value);
    }

    this.onCloseBulletinOffenceDilog();
  }

  onCloseBulletinOffenceDilog() {
    this.bulletinOffenceForm = new BulletinOffenceForm();
    this.dialog.close();
  }
}
