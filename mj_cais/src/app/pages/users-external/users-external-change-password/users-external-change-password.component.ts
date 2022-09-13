import { Component, Injector, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CrudForm } from '../../../@core/directives/crud-form.directive';
import { UsersExternalService } from '../users-external-form/_data/users-external.service';
import { UsersExternalForm } from '../users-external-form/_models/users-external.form';
import { UsersExternalModel } from '../users-external-form/_models/users-external.model';
import { UsersExternalChangePasswordData, UsersExternalChangePasswordResolver } from './_data/users-external-password.resolver';
import { UsersExternalPasswordService } from './_data/users-external.service';
import { UsersExternalPasswordForm } from './_models/users-external-password.form';

@Component({
  selector: 'cais-users-external-change-password',
  templateUrl: './users-external-change-password.component.html',
  styleUrls: ['./users-external-change-password.component.scss']
})
export class UsersExternalChangePasswordComponent 
extends CrudForm<
  UsersExternalModel,
  UsersExternalPasswordForm,
  UsersExternalChangePasswordData,
  UsersExternalPasswordService
>
implements OnInit
{  
  constructor(
    service: UsersExternalPasswordService,
    public injector: Injector
  ) {
    super(service, injector);
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }
  createInputObject(object: UsersExternalModel) {
    return new UsersExternalModel(object);
  }

  ngOnInit(): void {
    this.fullForm = new UsersExternalPasswordForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };
  
  protected override reloadCurrentRoute() {
    this.router
      .navigateByUrl("/pages/users-external");
  }

  protected override errorHandler(errorResponse): void {
    if (errorResponse.status == "401") {
      this.router.navigateByUrl("pages");
      return;
    }

    let title = this.dangerMessage;
    let errorText = errorResponse.status + " " + errorResponse.statusText;

    if (errorResponse.error && errorResponse.error.customMessage) {
      title = errorResponse.error.customMessage;
      errorText = "";
    } else if (errorResponse.error && Array.isArray(errorResponse.error)){
      errorText = "";
      for(let i = 0; i< errorResponse.error.length; i++){
        switch(errorResponse.error[i].code){
          case "PasswordTooShort":
          {
            errorText += "Паролата е прекалено къса; ";
            break;
          }
          case "PasswordRequiresNonAlphanumeric":
          {
            errorText += "Паролата изисква специален символ; ";
            break;
          }
          case "PasswordRequiresDigit":
          {
            errorText += "Паролата изисква цифра; ";
            break;
          }
          case "PasswordRequiresUpper":
          {
            errorText += "Паролата изисква главна буква; ";
            break;
          }
          case "PasswordRequiresLower":
          {
            errorText += "Паролата изисква малка буква; ";
            break;
          }
        }
      }
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
  }
}