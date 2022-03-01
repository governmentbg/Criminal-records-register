import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { BulletinForm } from "./models/bulletin.form";
import { BulletinModel } from "./models/bulletin.model";
import { BulletinResolverData } from "./data/bulletin.resolver";
import { BulletinService } from "./data/bulletin.service";
import { IgxDialogComponent, IgxGridComponent, IgxGridRowComponent } from "@infragistics/igniteui-angular";
import { BulletinOffenceForm } from "./models/bulletin-offance.form";
import { DateFormatService } from "../../../@core/services/common/date-format.service";

@Component({
  selector: "cais-bulletin-form",
  templateUrl: "./bulletin-form.component.html",
  styleUrls: ["./bulletin-form.component.scss"],
})
export class BulletinFormComponent
  extends CrudForm<
  BulletinModel,
  BulletinForm,
  BulletinResolverData,
  BulletinService
  >
  implements OnInit {

  @ViewChild("offencesGrid", {
    read: IgxGridComponent,
  })
  public offencesGrid: IgxGridComponent;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  public bulletinOffenceForm =
    new BulletinOffenceForm();

  constructor(service: BulletinService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
  ) {
    super(service, injector);
    this.backUrl = "pages/bulletins";
    this.setDisplayTitle("бюлетин");
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: BulletinModel) {
    return new BulletinModel(object);
  }

  ngOnInit(): void {
    this.fullForm = new BulletinForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  submitFunction = () => {
    let aggregatedTransactions = this.offencesGrid.transactions.getAggregatedChanges(true);
    this.fullForm.offancesTransactions.setValue(aggregatedTransactions);

    this.validateAndSave(this.fullForm);
  };

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

    let currentRow = this.offencesGrid.getRowByKey(
      this.bulletinOffenceForm.id.value
    );

    if (currentRow) {
      currentRow.update(this.bulletinOffenceForm.group.value);
    } else {
      this.offencesGrid.addRow(
        this.bulletinOffenceForm.group.value
      );
    }

    this.onCloseBulletinOffenceDilog();
  }

  onCloseBulletinOffenceDilog() {
    this.bulletinOffenceForm = new BulletinOffenceForm();
    this.dialog.close();
  }

  getOffenceCategoryNameByValue(id: string) {
    var offencesCategories = this.dbData.offencesCategories as any;
    
    return offencesCategories.find(
      (d) => d.id?.toString() === id
    )?.name;
  }

  getEcrisOffCatNameByValue(id: string): string {
    var ecrisOffCategories = this.dbData.ecrisOffCategories as any;
    return ecrisOffCategories.find(
      (d) => d.id?.toString() === id
    )?.name;
  }
  
}
