import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { IgxDialogComponent } from "@infragistics/igniteui-angular";
import { NbDialogService } from "@nebular/theme";
import { NgxSpinnerService } from "ngx-spinner";
import { Observable } from "rxjs";
import { CancelDialogComponent } from "../../../@core/components/dialogs/cancel-dialog/cancel-dialog.component";
import { ConfirmDialogComponent } from "../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { ReportApplicationStatusConstants } from "../report-application-overview/_models/report-applicarion-status.constants";
import { ReportApplicationResolverData } from "./_data/report-application.resolver";
import { ReportApplicationService } from "./_data/report-application.service";
import { ReportApplicationForm } from "./_models/report-application.form";
import { ReportApplicationModel } from "./_models/report-application.model";

@Component({
  selector: "cais-report-application-form",
  templateUrl: "./report-application-form.component.html",
  styleUrls: ["./report-application-form.component.scss"],
})
export class ReportApplicationFormComponent
  extends CrudForm<
    ReportApplicationModel,
    ReportApplicationForm,
    ReportApplicationResolverData,
    ReportApplicationService
  >
  implements OnInit
{
  @ViewChild("finalEditDialog", { read: IgxDialogComponent })
  public finalEditDialog: IgxDialogComponent;

  public reportApplicationStatus: string;
  public ReportApplicationStatusConstants = ReportApplicationStatusConstants;
  public PersonContextEnum = PersonContextEnum;

  public historyTabTitle = "????????";
  public reportsTabTitle = "??????????????";
  public showHistoryTab: boolean = false;
  public showReportsTab: boolean = false;
  private isFinalEdit: boolean;

  constructor(
    service: ReportApplicationService,
    public injector: Injector,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
    this.setDisplayTitle("???????????? ???? ?????????????? ???? ????????????????");
  }

  ngOnInit(): void {
    this.fullForm = new ReportApplicationForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.reportApplicationStatus = this.fullForm.statusCode.value;
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

    this.isForPreview =
      this.isForPreview ||
      this.reportApplicationStatus ==
        ReportApplicationStatusConstants.Approved ||
      this.reportApplicationStatus ==
        ReportApplicationStatusConstants.Canceled ||
      this.reportApplicationStatus ==
        ReportApplicationStatusConstants.Delivered;

    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ReportApplicationModel) {
    return object;
  }

  public submitFunction = () => {
    this.isFinalEdit = false;
    this.validateAndSave(this.fullForm);
  };

  public finalEdit() {
    this.dialogService
      .open(ConfirmDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe((x) => {
        if (x) {
          this.isFinalEdit = true;
          this.validateAndSave(this.fullForm);
        }
      });
  }

  protected validateAndSave(form: any) {
    console.log(form.group);
    if (!form.group.valid) {
      form.group.markAllAsTouched();
      this.toastr.showToast("danger", "???????????? ?????? ??????????????????????!");
      this.scrollToValidationError();
    } else {
      this.formObject = form.group.getRawValue();
      this.saveAndNavigate();
    }
  }

  protected saveAndNavigate() {
    let model = this.formObject;
    let submitAction: Observable<ReportApplicationModel>;
    if (this.isFinalEdit) {
      submitAction = this.service.updateFinal(this.formObject.id, model);
    } else if (this.isEdit()) {
      submitAction = this.service.update(this.formObject.id, model);
    } else {
      submitAction = this.service.save(model);
    }

    if (this.isLoadingForm) {
      return;
    }
    super.isLoadingForm = true;

    submitAction.subscribe({
      next: (data) => {
        this.toastr.showToast("success", this.successMessage);
        setTimeout(() => {
          this.onSubmitSuccess(data);
          this.isLoadingForm = false;
        }, this.navigateTimeout);
      },
      error: (errorResponse) => {
        this.finalEditDialog.close();
        this.errorHandler(errorResponse);
        setTimeout(() => {
          this.onSubmitSuccess(null);
        }, this.navigateTimeout);
      },
    });
  }

  public onOpenDialogForCancelApplication() {
    this.dialogService
      .open(CancelDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe((x) => {
        if (x) {
          this.isLoadingForm = true;
          this.service.cancel(this.objectId, x).subscribe({
            next: (data) => {
              let message = "?????????????? ??????????????????";
              this.toastr.showToast("success", message);
              this.router.navigate(["pages/report-applications"]);
            },
            error: (errorResponse) => {
              this.errorHandler(errorResponse);
            },
          });
        }
      });
  }

  onChangeTab(event) {
    let tabTitle = event.tabTitle;
    if (!this.showHistoryTab) {
      this.showHistoryTab = tabTitle == this.historyTabTitle;
    }

    if (!this.showReportsTab) {
      this.showReportsTab = tabTitle == this.reportsTabTitle;
    }
  }
}
