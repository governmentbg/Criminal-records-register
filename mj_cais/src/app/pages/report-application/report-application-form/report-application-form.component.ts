import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { Observable } from "rxjs";
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
  public reportApplicationStatus: string;
  public ReportApplicationStatusConstants = ReportApplicationStatusConstants;
  public PersonContextEnum = PersonContextEnum;

  constructor(
    service: ReportApplicationService,
    public injector: Injector,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
    this.setDisplayTitle("искане за справка за съдимост");
  }

  ngOnInit(): void {
    debugger;
    this.fullForm = new ReportApplicationForm();
    this.fullForm.group.patchValue(this.dbData.element);

    let selectedForeignKeys =
      this.fullForm.personData.nationalities.selectedForeignKeys.value;
    let mustAddDefaultCountry =
      !this.isEdit() &&
      (selectedForeignKeys == null || selectedForeignKeys.length == 0);

    if (mustAddDefaultCountry) {
      this.fullForm.personData.nationalities.selectedForeignKeys.patchValue([
        CommonConstants.bgCountryId,
      ]);
      this.fullForm.personData.nationalities.isChanged.patchValue(true);
    } else if (!this.isEdit()) {
      this.fullForm.personData.nationalities.isChanged.patchValue(true);
    }
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    debugger;

    return this.fullForm.group;
  }

  createInputObject(object: ReportApplicationModel) {
    debugger;

    return object;
  }

  public submitFunction = () => {
    //this.isFinalEdit = false;
    this.validateAndSave(this.fullForm);
  };

  public finalEdit() {
    //this.isFinalEdit = true;
    this.validateAndSave(this.fullForm);
  }

  protected saveAndNavigate() {
    let model = this.formObject;
    let submitAction: Observable<ReportApplicationModel>;
    // if (this.isFinalEdit) {
    //   submitAction = this.service.updateFinal(this.formObject.id, model);
    // } else
    if (this.isEdit()) {
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
        this.onServiceError(errorResponse);
      },
    });
  }
}
