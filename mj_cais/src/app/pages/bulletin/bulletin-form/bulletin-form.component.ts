import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { BulletinForm } from "./models/bulletin.form";
import { BulletinModel } from "./models/bulletin.model";
import { BulletinResolverData } from "./data/bulletin.resolver";
import { BulletinService } from "./data/bulletin.service";
import { BulletinOffencesFormComponent } from "./tabs/bulletin-offences-form/bulletin-offences-form.component";
import { BulletinSanctionsFormComponent } from "./tabs/bulletin-sanctions-form/bulletin-sanctions-form.component";
import { BulletinDecisionFormComponent } from "./tabs/bulletin-decision-form/bulletin-decision-form.component";
import { BulletinDocumentFormComponent } from "./tabs/bulletin-documents-form/bulletin-document-form.component";
import { BulletinPersonAliasForm } from "./models/bulletin-person-alias.form";
import {
  IgxDialogComponent,
  IgxGridComponent,
  IgxGridRowComponent,
} from "@infragistics/igniteui-angular";

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
  implements OnInit
{
  @ViewChild("bulletineOffences", {
    read: BulletinOffencesFormComponent,
  })
  public bulletineOffencesForm: BulletinOffencesFormComponent;

  @ViewChild("bulletineSanctions", {
    read: BulletinSanctionsFormComponent,
  })
  public bulletineSanctionsForm: BulletinSanctionsFormComponent;

  @ViewChild("bulletineDecisions", {
    read: BulletinDecisionFormComponent,
  })
  public bulletineDescitionForm: BulletinDecisionFormComponent;

  @ViewChild("bulletineDocuments", {
    read: BulletinDocumentFormComponent,
  })
  public bulletineDocumentsForm: BulletinDocumentFormComponent;

  //#region Person alias variables

  public bulletinPersonAliasForm = new BulletinPersonAliasForm();
  @ViewChild("bulletinPersonAliasGrid", { read: IgxGridComponent })
  public bulletinPersonAliasGrid: IgxGridComponent;

  @ViewChild("bulletinPersonAliasDialog", { read: IgxDialogComponent })
  public bulletinPersonAliasDialog: IgxDialogComponent;

  //#endregion

  constructor(service: BulletinService, public injector: Injector) {
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
    this.fullForm.statusId.disable();
    this.fullForm.csAuthorityName.disable(); 
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  submitFunction = () => {
    let offancesTransactions =
      this.bulletineOffencesForm.offencesGrid.transactions.getAggregatedChanges(
        true
      );

    this.fullForm.offancesTransactions.setValue(offancesTransactions);

    let sanctionsTransactions =
      this.bulletineSanctionsForm.sanctionGrid.transactions.getAggregatedChanges(
        true
      );

    this.fullForm.sanctionsTransactions.setValue(sanctionsTransactions);

    let decisionsTransactions =
      this.bulletineDescitionForm.decisionsGrid.transactions.getAggregatedChanges(
        true
      );

    this.fullForm.decisionsTransactions.setValue(decisionsTransactions);

    let docsTransactions =
      this.bulletineDocumentsForm.documentsGrid.transactions.getAggregatedChanges(
        true
      );

    this.fullForm.documentsTransactions.setValue(docsTransactions);

    let personAliasTransactions =
      this.bulletinPersonAliasGrid.transactions.getAggregatedChanges(true);
    this.fullForm.personAliasTransactions.setValue(personAliasTransactions);

    this.validateAndSave(this.fullForm);
  };

  //#region Person alias functions

  public onOpenEditBulletinPersonAlias(event: IgxGridRowComponent) {
    this.bulletinPersonAliasForm.group.patchValue(event.rowData);
    this.bulletinPersonAliasDialog.open();
  }

  public onDeleteBulletinPersonAlias(event: IgxGridRowComponent) {
    this.bulletinPersonAliasGrid.deleteRow(event.rowData.id);
    this.bulletinPersonAliasGrid.data =
      this.bulletinPersonAliasGrid.data.filter((d) => d.id != event.rowData.id);
  }

  onAddOrUpdateBulletinPersonAliasRow() {
    if (!this.bulletinPersonAliasForm.group.valid) {
      this.bulletinPersonAliasForm.group.markAllAsTouched();
      return;
    }

    if (this.bulletinPersonAliasForm.typeId.value) {
      var typeObj =  (this.dbData.personAliasTypes as any).find(
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
  }

  onCloseBulletinPersonAliasDilog() {
    this.bulletinPersonAliasForm = new BulletinPersonAliasForm();
    this.bulletinPersonAliasDialog.close();
  }

  //#endregion
}
