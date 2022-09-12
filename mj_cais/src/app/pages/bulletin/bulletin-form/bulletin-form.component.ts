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
import { CommonConstants } from "../../../@core/constants/common.constants";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";
import { NgxSpinnerService } from "ngx-spinner";
import { NbDialogService } from "@nebular/theme";
import { ConfirmDialogComponent } from "../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { TranslateService } from "@ngx-translate/core";
import { Observable } from "rxjs";
import { UserAuthorityService } from "../../../@core/services/common/user-authority.service";

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
  private isFinalEdit: boolean = false;
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

  //#region Tabset

  public offencesTabTitle = "Престъпления";
  public sanctionsTabTitle = "Наказания";
  public decisionTabTitle = "Осъждане";
  public eventsTabTitle = "Уведомления";
  public documentsTabTitle = "Документи";
  public isinTabTitle = "Изтърпени наказания";
  public historyTabTitle = "Одит";
  public docEventTabTitle =
    "Уведомяване за променен съдебен статус на осъдено лице";

  public showOffencesTab: boolean = false;
  public showSanctionsTab: boolean = false;
  public showDecisionTab: boolean = false;
  public showEventsTab: boolean = false;
  public showDocumentsTab: boolean = false;
  public showIsinTab: boolean = false;
  public showHistoryTab: boolean = false;
  public showDocEventTab: boolean = false;

  //#endregion

  public isNoSanctionCheck: boolean = false;
  public isBulletinPersonAliasEditable = false;
  public showForUpdate: boolean = false;
  public setEditToBeForPreview: boolean = false;
  public showEditBtn: boolean = false;
  public PersonContextEnum = PersonContextEnum;

  constructor(
    service: BulletinService,
    public injector: Injector,
    private loaderService: NgxSpinnerService,
    private dialogService: NbDialogService,
    private translate: TranslateService,
    private userAuthorityService: UserAuthorityService
  ) {
    super(service, injector);
    this.setDisplayTitle("бюлетин");
  }

  ngOnInit(): void {
    let bulletinStatusId = (this.dbData.element as any)?.statusId;
    let locked = (this.dbData.element as any)?.locked;

    this.fullForm = new BulletinForm(bulletinStatusId, this.isEdit(), locked);
    this.fullForm.group.patchValue(this.dbData.element);

    let selectedForeignKeys =
      this.fullForm.person.nationalities.selectedForeignKeys.value;

    let mustAddDefaultCountry =
      !this.isEdit() &&
      (selectedForeignKeys == null || selectedForeignKeys.length == 0);

    if (mustAddDefaultCountry) {
      this.fullForm.person.nationalities.selectedForeignKeys.patchValue([
        CommonConstants.bgCountryId,
      ]);
    }

    this.fullForm.person.nationalities.isChanged.patchValue(true);
    this.isNoSanctionCheck = this.fullForm.noSanction.value;
    this.initAllowedButtons(bulletinStatusId, locked);
    if (
      this.isEdit() &&
      this.userAuthorityService.csAuthorityId !=
      (this.dbData.element as any).csAuthorityId
    ) {
      this.isForPreview = true;
    }
    this.formFinishedLoading.emit();


  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: BulletinModel) {
    return new BulletinModel(object);
  }

  submitFunction = () => {
    this.isFinalEdit = false;
    this.applyTransactions();
    this.validateAndSave(this.fullForm);
  };

  //override submit function
  //todo
  onSubmitSuccess(data: any) {
    this.loaderService.hide();
    if (this.isFinalEdit) {
      this.toastr.showToast(
        "success",
        this.translate.instant("BULLETIN.SUCCESS-UPDATE-STATUS")
      );
      this.router.navigate(["pages/bulletins/active"]);
      return;
    }
    super.onSubmitSuccess(data);
  }

  //override
  protected validateAndSave(form: any) {
    let isValid = this.validateSanctionAndOffences(form);

    if (!form.group.valid || !isValid) {
      form.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");

      this.scrollToValidationError();
    } else {
      this.loaderService.show();
      this.formObject = form.group.getRawValue();
      this.saveAndNavigate();
    }
  }

  protected errorHandler(errorResponse): void {
    this.loaderService.hide();
    super.errorHandler(errorResponse);
  }

  protected saveAndNavigate() {
    let model = this.formObject;
    let submitAction: Observable<BulletinModel>;
    if (this.isFinalEdit) {
      submitAction = this.service.updateFinal(this.formObject.id, model);
    } else if (this.isEdit()) {
      submitAction = this.service.update(this.formObject.id, model);
    } else {
      submitAction = this.service.save(model);
    }

    submitAction.subscribe({
      next: (data) => {
        this.toastr.showToast("success", this.successMessage);

        setTimeout(() => {
          this.onSubmitSuccess(data);
        }, this.navigateTimeout);
      },
      error: (errorResponse) => {
        this.errorHandler(errorResponse);
      },
    });
  }

  public onNoSanctionChange(event: any) {
    this.isNoSanctionCheck = event.target.checked;
  }

  public prevSuspSentChange(event: any) {
    const isPrevSuspSentCheck = event.target.checked;

    if (!isPrevSuspSentCheck) {
      this.fullForm?.prevSuspSentDescr.setValue(null);
    }

  }

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
        this.loaderService.show();
        this.isFinalEdit = true;
        this.applyTransactions();
        this.validateAndSave(this.fullForm);
      }
    });
  }

  public onChangeEventsTab(event) {
    this.showDocEventTab =
      !this.showDocEventTab && event.tabTitle == this.docEventTabTitle;
  }

  public print() {
    this.loaderService.show();
    this.service.print(this.objectId).subscribe({
      next: (response) => {
        this.loaderService.hide();
        this.downloadFile(response);
      },
      error: (errorResponse) => {
        this.loaderService.hide();
        this.errorHandler(errorResponse);
      },
    });
  }

  public onChangeTab(event) {
    let tabTitle = event.tabTitle;

    if (!this.showOffencesTab) {
      this.showOffencesTab = tabTitle == this.offencesTabTitle;
    }

    if (!this.showSanctionsTab) {
      this.showSanctionsTab = tabTitle == this.sanctionsTabTitle;
    }

    if (!this.showDecisionTab) {
      this.showDecisionTab = tabTitle == this.decisionTabTitle;
    }

    if (!this.showEventsTab) {
      this.showEventsTab = tabTitle == this.eventsTabTitle;
    }

    if (!this.showDocumentsTab) {
      this.showDocumentsTab = tabTitle == this.documentsTabTitle;
    }

    if (!this.showIsinTab) {
      this.showIsinTab = tabTitle == this.isinTabTitle;
    }

    if (!this.showHistoryTab) {
      this.showHistoryTab = tabTitle == this.historyTabTitle;
    }
  }

  private validateSanctionAndOffences(form): boolean {
    let isValid = true;
    let bulletinStatusId = form.statusId.value;
    let validateForSancAndOff =
      bulletinStatusId == null ||
      bulletinStatusId == BulletinStatusTypeEnum.NewEISS ||
      bulletinStatusId == BulletinStatusTypeEnum.NewOffice;
    if (validateForSancAndOff) {
      // when add new bulletin

      let showInvalidSanctionMsg = false;
      let showInvalidOffenceMsg = false;
      if (!this.isEdit()) {
        if (this.fullForm.offancesTransactions.value.length == 0) {
          showInvalidOffenceMsg = true;
          isValid = false;
        }

        if (
          (this.fullForm.noSanction.value == null ||
            this.fullForm.noSanction.value == false) &&
          this.fullForm.sanctionsTransactions.value.length == 0
        ) {
          showInvalidSanctionMsg = true;
          isValid = false;
        }
      } else {
        // edit bulletin
        if (
          (this.fullForm.noSanction.value == null ||
            this.fullForm.noSanction.value == false) &&
          this.bulletineSanctionsForm
        ) {
          let sanctionCount =
            this.bulletineSanctionsForm.sanctionGrid.data.length;
          let notDeletedSanctions =
            this.fullForm.sanctionsTransactions.value.filter(
              (x) => x.type != "delete"
            ).length;

          if (notDeletedSanctions == 0 && sanctionCount == 0) {
            showInvalidSanctionMsg = true;
            isValid = false;
          }
        }

        if (this.bulletineOffencesForm) {
          let offenceCount =
            this.bulletineOffencesForm.offencesGrid.data.length;
          let notDeletedOffences =
            this.fullForm.offancesTransactions.value.filter(
              (x) => x.type != "delete"
            ).length;

          if (offenceCount == 0 && notDeletedOffences == 0) {
            showInvalidOffenceMsg = true;
            isValid = false;
          }
        }
      }

      if (showInvalidSanctionMsg) {
        this.toastr.showToast(
          "danger",
          "Задължително е да въведете поне едно наказание!"
        );
      }

      if (showInvalidOffenceMsg) {
        this.toastr.showToast(
          "danger",
          "Задължително е да въведете поне едно престъпление!"
        );
      }
    }

    return isValid;
  }
  private initAllowedButtons(bulletinStatusId: string, isLocked: boolean) {
    let userHasDiffAuth = false;
    // create via button
    if (this.dbData.element) {
      userHasDiffAuth =
        this.userAuthorityService.csAuthorityId !=
        (this.dbData.element as any).csAuthorityId;
      // create via person form
      if (!this.isEdit()) {
        userHasDiffAuth = false;
      }
    }

    let isGridsEditable =
      !userHasDiffAuth &&
      !this.isForPreview &&
      (bulletinStatusId == BulletinStatusTypeEnum.NewOffice ||
        isLocked == false ||
        this.currentAction == EActions.CREATE);

    this.isBulletinPersonAliasEditable = isGridsEditable;
    this.isOffancesEditable = isGridsEditable;
    this.isSanctionsEditable = isGridsEditable;

    this.isDocumentsEditable = this.isEdit();

    this.isDecisionEditable =
      !userHasDiffAuth &&
      !this.isForPreview &&
      bulletinStatusId != undefined &&
      bulletinStatusId !== BulletinStatusTypeEnum.NewEISS &&
      bulletinStatusId !== BulletinStatusTypeEnum.NewOffice;

    let hideUpdateButton =
      //bulletinStatusId == BulletinStatusTypeEnum.Rehabilitated ||
      bulletinStatusId == BulletinStatusTypeEnum.Deleted ||
      bulletinStatusId == BulletinStatusTypeEnum.ForDestruction ||
      bulletinStatusId == BulletinStatusTypeEnum.ReplacedAct425;

    this.setEditToBeForPreview =
      hideUpdateButton || this.isForPreview || userHasDiffAuth;

    this.showForUpdate =
      (this.fullForm.statusIdDisplay.value == BulletinStatusTypeEnum.NewEISS ||
        this.fullForm.statusIdDisplay.value ==
        BulletinStatusTypeEnum.NewOffice) &&
      userHasDiffAuth == false;

    // redirect to edit
    this.showEditBtn =
      this.activatedRoute.snapshot.data["preview"] && !userHasDiffAuth;
  }

  private applyTransactions() {
    if (this.bulletineOffencesForm?.offencesGrid) {
      let offancesTransactions =
        this.bulletineOffencesForm.offencesGrid.transactions.getAggregatedChanges(
          true
        );

      this.fullForm.offancesTransactions.setValue(offancesTransactions);
    } else {
      this.fullForm.offancesTransactions.setValue([]);
    }

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
    } else {
      this.fullForm.decisionsTransactions.setValue([]);
    }
  }
}
