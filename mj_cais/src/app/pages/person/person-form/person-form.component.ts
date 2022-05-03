import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { PersonResolverData } from "./_data/person.resolver";
import { PersonService } from "./_data/person.service";
import { PersonForm } from "./_models/person.form";
import { PersonModel } from "./_models/person.model";

@Component({
  selector: "cais-person-form",
  templateUrl: "./person-form.component.html",
  styleUrls: ["./person-form.component.scss"],
})
export class PersonFormComponent
  extends CrudForm<PersonModel, PersonForm, PersonResolverData, PersonService>
  implements OnInit
{
  constructor(service: PersonService, public injector: Injector) {
    super(service, injector);
    this.setDisplayTitle("лице");
  }

  ngOnInit(): void {
    this.fullForm = new PersonForm();
    this.fullForm.group.patchValue(this.dbData.element);
  }

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: PersonModel) {
    return new PersonModel(object);
  }
}
