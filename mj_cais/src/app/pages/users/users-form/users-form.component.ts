import { Component, Injector, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { NgxPermissionsService } from 'ngx-permissions';
import { Observable } from 'rxjs';
import { RoleNameEnum } from '../../../@core/constants/role-name.enum';
import { CrudForm } from '../../../@core/directives/crud-form.directive';
import { BaseResolverData } from '../../../@core/models/common/base-resolver.data';
import { BaseNomenclatureModel } from '../../../@core/models/nomenclature/base-nomenclature.model';
import { UserResolver, UserResolverData } from './_data/user.resolver';
import { UserService } from './_data/user.service';
import { UserForm } from './_models/user.form';
import { UserModel } from './_models/user.model';

@Component({
  selector: 'cais-users-form',
  templateUrl: './users-form.component.html',
  styleUrls: ['./users-form.component.scss']
})
export class UsersFormComponent 
extends CrudForm<
  UserModel,
  UserForm,
  UserResolverData,
  UserService
>
implements OnInit
{  
  constructor(
    service: UserService,
    public injector: Injector,
    private permissionsService: NgxPermissionsService
  ) {
    super(service, injector);
    this.setDisplayTitle("потребител");
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }
  createInputObject(object: UserModel) {
    return new UserModel(object);
  }

  ngOnInit(): void {
    
    this.permissionsService.permissions$.subscribe((perm) => {
      var roles = Object.keys(perm);
      this.fullForm = new UserForm( roles.indexOf(RoleNameEnum.GlobalAdmin) > -1 );
      this.fullForm.group.patchValue(this.dbData.element);
      this.formFinishedLoading.emit();
    });
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

    if (errorResponse.error && errorResponse.error.code && errorResponse.error.code === 'EGNAlreadyExists') {
      title = "Грешка";
      errorText = "Потребител с подаденото ЕГН вече съществува";
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

