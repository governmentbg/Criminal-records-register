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

}

