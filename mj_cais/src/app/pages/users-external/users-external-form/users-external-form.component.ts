import { Component, Injector, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CrudForm } from '../../../@core/directives/crud-form.directive';
import { UsersExternalResolver, UsersExternalResolverData } from './_data/users-external.resolver';
import { UsersExternalService } from './_data/users-external.service';
import { UsersExternalForm } from './_models/users-external.form';
import { UsersExternalModel } from './_models/users-external.model';

@Component({
  selector: 'cais-users-external-form',
  templateUrl: './users-external-form.component.html',
  styleUrls: ['./users-external-form.component.scss']
})
export class UsersExternalFormComponent 
extends CrudForm<
  UsersExternalModel,
  UsersExternalForm,
  UsersExternalResolverData,
  UsersExternalService
>
implements OnInit
{  
  constructor(
    service: UsersExternalService,
    public injector: Injector
  ) {
    super(service, injector);
    this.setDisplayTitle("външен потребител");
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }
  createInputObject(object: UsersExternalModel) {
    return new UsersExternalModel(object);
  }

  ngOnInit(): void {
    this.fullForm = new UsersExternalForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }
  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };

  protected override errorHandler(errorResponse): void {
    if (errorResponse.status == "401") {
      this.router.navigateByUrl("pages");
      return;
    }

    let title = this.dangerMessage;
    let errorText = errorResponse.status + " " + errorResponse.statusText;

    if (errorResponse.error && errorResponse.error.code && errorResponse.error.code === 'UserAlreadyExists') {
      title = "Грешка";
      errorText = "Потребител с подаденото ЕГН вече съществува в избраната администрация";
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

