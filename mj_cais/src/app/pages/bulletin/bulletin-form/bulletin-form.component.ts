import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { BulletinForm } from "./_models/bulletin.form";
import { BulletinModel } from "./_models/bulletin.model";
import { BulletinResolverData } from "./_data/bulletin.resolver";
import { BulletinService } from "./_data/bulletin.service";
import { BulletinOffencesFormComponent } from "./tabs/bulletin-offences-form/bulletin-offences-form.component";
import { BulletinSanctionsFormComponent } from "./tabs/bulletin-sanctions-form/bulletin-sanctions-form.component";
import { BulletinDecisionFormComponent } from "./tabs/bulletin-decision-form/bulletin-decision-form.component";
import { BulletinDocumentFormComponent } from "./tabs/bulletin-documents-form/bulletin-document-form.component";
import { BulletinPersonAliasForm } from "./_models/bulletin-person-alias.form";
import {
  IgxDialogComponent,
  IgxGridComponent,
  IgxGridRowComponent,
} from "@infragistics/igniteui-angular";
import { BulletinStatusTypeEnum } from "../bulletin-overview/_models/bulletin-status-type.constants";
import { EActions } from "@tl/tl-common";
import { ConfirmDialogComponent } from "../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { NbDialogService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";

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
  //#region Престъпления
  @ViewChild("bulletineOffences", {
    read: BulletinOffencesFormComponent,
  })
  public bulletineOffencesForm: BulletinOffencesFormComponent;
  public isOffancesEditable: boolean;
  //#endregion

  //#region Наказания
  @ViewChild("bulletineSanctions", {
    read: BulletinSanctionsFormComponent,
  })
  public bulletineSanctionsForm: BulletinSanctionsFormComponent;
  public isSanctionsEditable: boolean;
  //#endregion

  //#region Допълнителни сведения
  @ViewChild("bulletineDecisions", {
    read: BulletinDecisionFormComponent,
  })
  public bulletineDescitionForm: BulletinDecisionFormComponent;
  public isDecisionEditable: boolean = false;
  //#endregion

  //#region Докумнти
  @ViewChild("bulletineDocuments", {
    read: BulletinDocumentFormComponent,
  })
  public bulletineDocumentsForm: BulletinDocumentFormComponent;
  public isDocumentsEditable: boolean;
  //#endregion

  //#region Псевдоним или други използвани имена
  public bulletinPersonAliasForm = new BulletinPersonAliasForm();
  @ViewChild("bulletinPersonAliasGrid", { read: IgxGridComponent })
  public bulletinPersonAliasGrid: IgxGridComponent;

  @ViewChild("bulletinPersonAliasDialog", { read: IgxDialogComponent })
  public bulletinPersonAliasDialog: IgxDialogComponent;

  public isBulletinPersonAliasEditable = false;
  //#endregion

  public showForUpdate: boolean = false;
  public setEditToBeForPreview: boolean = false;
  
  constructor(
    service: BulletinService,
    public injector: Injector,
    private dialogService: NbDialogService,
    private translate: TranslateService
  ) {
    super(service, injector);
    this.setDisplayTitle("бюлетин");
  }

  ngOnInit(): void {
    let bulletinStatusId = (this.dbData.element as any)?.statusId;
    let locked = (this.dbData.element as any)?.locked;

    this.fullForm = new BulletinForm(bulletinStatusId, this.isEdit(), locked);
    this.fullForm.group.patchValue(this.dbData.element);

    this.initAllowedButtons(bulletinStatusId);

    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: BulletinModel) {
    return new BulletinModel(object);
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

  //#region Актуализация на бюлетин

  public openUpdateConfirmationDialog() {
    let dialogRef = this.dialogService.open(
      ConfirmDialogComponent,
      CommonConstants.defaultDialogConfig
    );

    dialogRef.componentRef.instance.confirmMessage = this.translate.instant(
      "BULLETIN.CONFIRM-MESSAGE-WHEN-UPDATE"
    );
    dialogRef.componentRef.instance.showHeder = false;

    dialogRef.onClose.subscribe((result) => {
      if (result) {
        this.service
          .changeStatus(this.fullForm.id.value, BulletinStatusTypeEnum.Active)
          .subscribe(
            (res) => {
              this.toastr.showToast(
                "success",
                this.translate.instant("BULLETIN.SUCCESS-UPDATE-STATUS")
              );
              this.router.navigate(['pages/bulletins']); 
            },
            (error) => {
              let title = this.dangerMessage;
              let errorText = error.status + " " + error.statusText;
              if (error.error && error.error.customMessage) {
                title = error.error.customMessage;
                errorText = "";
              }

              this.toastr.showBodyToast("danger", title, errorText);
            }
          );
      }
    });
  }

  //#endregion

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
      var typeObj = (this.dbData.personAliasTypes as any).find(
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

  private initAllowedButtons(bulletinStatusId: string) {
    let isGridsEditable =
      (!this.isForPreview &&
        bulletinStatusId == BulletinStatusTypeEnum.NewOffice) ||
      this.currentAction == EActions.CREATE;

    this.isBulletinPersonAliasEditable = isGridsEditable;
    this.isOffancesEditable = isGridsEditable;
    this.isSanctionsEditable = isGridsEditable;

    this.isDocumentsEditable =
      !this.isForPreview &&
      bulletinStatusId == BulletinStatusTypeEnum.NewOffice;

    this.isDecisionEditable =
      (!this.isForPreview &&
        bulletinStatusId == BulletinStatusTypeEnum.Active) ||
      bulletinStatusId == BulletinStatusTypeEnum.ForRehabilitation;

    let hideUpdateButton =
      bulletinStatusId == BulletinStatusTypeEnum.Rehabilitated ||
      bulletinStatusId == BulletinStatusTypeEnum.Deleted ||
      bulletinStatusId == BulletinStatusTypeEnum.ForDestruction ||
      bulletinStatusId == BulletinStatusTypeEnum.ReplacedAct425;
    this.setEditToBeForPreview = hideUpdateButton;

    this.showForUpdate =
      this.fullForm.statusIdDisplay.value == BulletinStatusTypeEnum.NewEISS ||
      this.fullForm.statusIdDisplay.value == BulletinStatusTypeEnum.NewOffice;
  }
}
