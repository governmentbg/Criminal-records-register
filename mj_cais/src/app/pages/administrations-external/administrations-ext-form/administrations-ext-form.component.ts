import { Component, Injector, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CrudForm } from '../../../@core/directives/crud-form.directive';
import { AdministrationsExtResolverData } from './_data/administrations-ext.resolver';
import { AdministrationsExtService } from './_data/administrations-ext.service';
import { AdministrationsExtForm } from './_models/administrations-ext.form';
import { AdministrationsExtModel } from './_models/administrations-ext.model';

@Component({
  selector: 'cais-administrations-ext-form',
  templateUrl: './administrations-ext-form.component.html',
  styleUrls: ['./administrations-ext-form.component.scss']
})
export class AdministrationsExtFormmComponent 
extends CrudForm<
  AdministrationsExtModel,
  AdministrationsExtForm,
  AdministrationsExtResolverData,
  AdministrationsExtService
>
implements OnInit
{  
  constructor(
    service: AdministrationsExtService,
    public injector: Injector
  ) {
    super(service, injector);
    this.setDisplayTitle("външна администрация");
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: AdministrationsExtModel) {
    return new AdministrationsExtModel(object);
  }

  ngOnInit(): void {
    this.fullForm = new AdministrationsExtForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };

}

