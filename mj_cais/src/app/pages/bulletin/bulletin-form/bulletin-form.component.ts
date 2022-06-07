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
import { BulletinStatusTypeEnum } from "../bulletin-overview/_models/bulletin-status-type.enum";
import { EActions } from "@tl/tl-common";
import { ConfirmDialogComponent } from "../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { NbDialogService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";

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

  public isNoSanctionCheck: boolean = false;
  public isBulletinPersonAliasEditable = false;
  public showForUpdate: boolean = false;
  public setEditToBeForPreview: boolean = false;
  public PersonContextEnum = PersonContextEnum;

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

    if (!this.isEdit()) {
      this.fullForm.person.nationalities.selectedForeignKeys.patchValue([
        "CO-00-100-BGR",
      ]);
      this.fullForm.person.nationalities.isChanged.patchValue(true);
    }

    this.isNoSanctionCheck = this.fullForm.noSanction.value;
    this.initAllowedButtons(bulletinStatusId, locked);

    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: BulletinModel) {
    return new BulletinModel(object);
  }

  submitFunction = () => {
    if (this.bulletineOffencesForm?.offencesGrid) {
      let offancesTransactions =
        this.bulletineOffencesForm.offencesGrid.transactions.getAggregatedChanges(
          true
        );

      this.fullForm.offancesTransactions.setValue(offancesTransactions);
    }

    // if noSanction is false
    // todo: remove sanction saved in db ???
    if (this.bulletineSanctionsForm?.sanctionGrid) {
      let sanctionsTransactions =
        this.bulletineSanctionsForm.sanctionGrid.transactions.getAggregatedChanges(
          true
        );

      this.fullForm.sanctionsTransactions.setValue(sanctionsTransactions);
    } else {
      this.fullForm.sanctionsTransactions.setValue([]);
    }

    if (this.bulletineDescitionForm?.decisionsGrid) {
      let decisionsTransactions =
        this.bulletineDescitionForm.decisionsGrid.transactions.getAggregatedChanges(
          true
        );

      this.fullForm.decisionsTransactions.setValue(decisionsTransactions);
    }
    this.validateAndSave(this.fullForm);
  };

  //override submit function
  //todo
  // onSubmitSuccess(data: any) {
  //   // if (data?.id) {
  //   //   const url = `pages/bulletins/preview/${data.id}`;
  //   //   this.router.navigateByUrl(url);
  //   // } else {
  //   //   super.reloadCurrentRoute();
  //   // }
  // }

  public onNoSanctionChange(event: any) {
    this.isNoSanctionCheck = event.target.checked;
  }

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
              this.router.navigate(["pages/bulletins"]);
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

  private initAllowedButtons(bulletinStatusId: string, isLocked: boolean) {
    let isGridsEditable =
      (!this.isForPreview &&
        (bulletinStatusId == BulletinStatusTypeEnum.NewOffice ||
          isLocked == false)) ||
      this.currentAction == EActions.CREATE;

    this.isBulletinPersonAliasEditable = isGridsEditable;
    this.isOffancesEditable = isGridsEditable;
    this.isSanctionsEditable = isGridsEditable;

    this.isDocumentsEditable = this.isEdit();

    this.isDecisionEditable =
      (!this.isForPreview &&
        (bulletinStatusId == BulletinStatusTypeEnum.Active ||
          isLocked == false)) ||
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
