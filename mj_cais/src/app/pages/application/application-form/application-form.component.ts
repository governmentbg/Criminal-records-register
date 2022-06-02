import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { Observable } from "rxjs";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
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

  constructor(
    service: ApplicationService,
    public injector: Injector,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
    this.backUrl = "pages/applications";
    this.setDisplayTitle("Свидетелство");
  }

  ngOnInit(): void {
    this.fullForm = new ApplicationForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
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
    this.validateAndSave(this.fullForm);
  };

  public finalEdit(){
    this.validateAndSave(this.fullForm);
  }
  protected saveAndNavigate() {
    
    let model = this.formObject;
    let submitAction: Observable<ApplicationModel>;
    if (this.isEdit()) {
      submitAction = this.service.updateFinal(this.formObject.id, model);
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

}
