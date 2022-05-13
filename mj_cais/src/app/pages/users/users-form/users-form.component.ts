import { Component, Injector, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
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
    public injector: Injector
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
    this.fullForm = new UserForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }
  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };

}

