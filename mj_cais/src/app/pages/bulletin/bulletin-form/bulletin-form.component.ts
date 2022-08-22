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
import { BulletinHelperService } from "../_data/bulletin-helper.service";

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

  //#region Tabset

  public offencesTabTitle = "Престъпления";
  public sanctionsTabTitle = "Наказания";
  public decisionTabTitle = "Доп. сведения";
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
  public PersonContextEnum = PersonContextEnum;

  constructor(
    service: BulletinService,
    public injector: Injector,
    private loaderService: NgxSpinnerService,
    private helperService: BulletinHelperService
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
      this.fullForm.person.nationalities.isChanged.patchValue(true);
    } else if (!this.isEdit()) {
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

    this.validateAndSave(this.fullForm);
  };

  //override submit function
  //todo
  onSubmitSuccess(data: any) {
    this.loaderService.hide();
    super.onSubmitSuccess(data);
  }

  //override
  protected validateAndSave(form: any) {
    debugger;
    console.log(form.group);
    if (!form.group.valid) {
      form.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");

      this.scrollToValidationError();
    } else {
      this.loaderService.show();
      this.formObject = form.group.value;

      this.saveAndNavigate();
    }
  }

  public onNoSanctionChange(event: any) {
    this.isNoSanctionCheck = event.target.checked;
  }

  public openUpdateConfirmationDialog() {
    this.helperService.openUpdateConfirmationDialog(this.fullForm.id.value);
  }

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

  onChangeTab(event) {
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

  onChangeEventsTab(event) {
    this.showDocEventTab =
      !this.showDocEventTab && event.tabTitle == this.docEventTabTitle;
  }

  print() {
    this.loaderService.show();
    this.service.print(this.objectId).subscribe(
      (response) => {
        this.loaderService.hide();
        this.downloadFile(response);
      },
      (error) => {
        this.loaderService.hide();
        this.onServiceError(error);
      }
    );
  }
}
