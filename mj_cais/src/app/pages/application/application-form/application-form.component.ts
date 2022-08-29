import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { EActions } from "@tl/tl-common";
import * as fileSaver from "file-saver";
import { Observable } from "rxjs";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { ApplicationTypeStatusConstants } from "../application-overview/_models/application-type-status.constants";
import { CancelDialogComponent } from "./cancel-dialog/cancel-dialog.component";
import { ApplicationResolverData } from "./_data/application.resolver";
import { ApplicationService } from "./_data/application.service";
import { ApplicationForm } from "./_models/application.form";
import { ApplicationModel } from "./_models/application.model";

@Component({
  selector: "cais-application-form",
  templateUrl: "./application-form.component.html",
  styleUrls: ["./application-form.component.scss"],
})
export class ApplicationFormComponent
  extends CrudForm<
    ApplicationModel,
    ApplicationForm,
    ApplicationResolverData,
    ApplicationService
  >
  implements OnInit
{
  public PersonContextEnum = PersonContextEnum;
  public applicationStatus: string;
  public ApplicationTypeStatusConstants = ApplicationTypeStatusConstants;
  private isFinalEdit: boolean;
  public isCreate: boolean;
  isAppAproved: boolean = false;

  constructor(
    service: ApplicationService,
    public injector: Injector,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
    this.backUrl = "pages/applications";
    this.setDisplayTitle("свидетелство");
  }

  ngOnInit(): void {
    this.fullForm = new ApplicationForm();
    this.fullForm.group.patchValue(this.dbData.element);

    if (
      this.fullForm.statusCode.value ==
      ApplicationTypeStatusConstants.ApprovedApplication){
        this.isAppAproved = true;
      }

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

    this.applicationStatus = this.fullForm.statusCode.value;
    if (
      this.fullForm.statusCode.value ==
      ApplicationTypeStatusConstants.ApprovedApplication || this.fullForm.statusCode.value ==
      ApplicationTypeStatusConstants.Canceled
    ) {
      this.fullForm.group.disable();
    }
   
    this.formFinishedLoading.emit();
    if (
      this.fullForm.statusCode.value ==
      ApplicationTypeStatusConstants.CheckPayment
    ) {
      this.fullForm.paymentMethodId.enable();
    }

    this.isCreate = this.currentAction != EActions.EDIT;
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ApplicationModel) {
    return object;
  }

  submitFunction = () => {
    // this.fullForm.applicationTypeId.setValue('6'); //Взима се от контекста
    // this.fullForm.csAuthorityId.setValue('562');  //Взима се от контекста
    this.isFinalEdit = false;
    this.validateAndSave(this.fullForm);
  };

  protected validateAndSave(form: any) {
    console.log(form.group);
    if (!form.group.valid) {
      form.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");

      this.scrollToValidationError();
    } else {
      this.formObject = form.group.getRawValue();
      this.saveAndNavigate();
    }
  }

  protected saveAndNavigate() {
    let model = this.formObject;
    let submitAction: Observable<ApplicationModel>;
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
        let title = this.dangerMessage;
        let errorText = errorResponse.status + " " + errorResponse.statusText;

        if (errorResponse.error && errorResponse.error.customMessage) {
          title = errorResponse.error.customMessage;
          errorText = "";
        }

        this.toastr.showBodyToast("danger", title, errorText);

        // if has server side validation errors add them to the form control
        if (errorResponse.error.errors) {
          Object.keys(errorResponse.error.errors).forEach((prop) => {
            var propName = prop[0].toLocaleLowerCase() + prop.slice(1);
            const formControl = this.fullForm.group.get(propName);
            if (formControl) {
              // activate the error message
              formControl.setErrors({
                serverErrors: errorResponse.error.errors[prop],
              });
            }
          });
        }

        this.scrollToValidationError();
      },
    });
  }

  public printApplication() {
    this.service.printApplication(this.objectId).subscribe((response) => {
      let blob = new Blob([response.body]);
      window.URL.createObjectURL(blob);

      let header = response.headers.get("Content-Disposition");
      let filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;

      let fileName = "download";

      var matches = filenameRegex.exec(header);
      if (matches != null && matches[1]) {
        fileName = matches[1].replace(/['"]/g, "");
      }

      fileSaver.saveAs(blob, fileName);
      this.toastr.showToast("success", "Успешно разпечатан документ");
    });
  }

  public finalEdit() {
    this.isFinalEdit = true;
    this.validateAndSave(this.fullForm);
  }

  public changeStatusToCheckPayment() {
    this.service
      .changeStatusToCheckPayment(this.objectId)
      .subscribe((result) => {});
  }

  public cancelApplication() {
    this.dialogService
      .open(CancelDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe((x) => {
        if (x) {
          this.service
            .cancelApplication(this.objectId, x)
            .subscribe((result) => {
              let message = "Успешно анулирано";
              this.toastr.showToast("success", message);
              this.locationService.back();
            });
        }
      });
  }
}
