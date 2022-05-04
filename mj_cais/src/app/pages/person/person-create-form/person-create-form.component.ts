import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";
import { PersonForm } from "../../../@core/components/forms/person-form/_models/person.form";
import { PersonModel } from "../../../@core/components/forms/person-form/_models/person.model";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { PersonResolverData } from "./_data/person.resolver";
import { PersonService } from "./_data/person.service";

@Component({
  selector: "cais-person-create-form",
  templateUrl: "./person-create-form.component.html",
  styleUrls: ["./person-create-form.component.scss"],
})
export class PersonCreateFormComponent
  extends CrudForm<PersonModel, PersonForm, PersonResolverData, PersonService>
  implements OnInit
{
  private readonly PERSON_OVERVIEW_URL = "/pages/people";
  constructor(service: PersonService, public injector: Injector) {
    super(service, injector);
    this.setDisplayTitle("лице");
  }

  ngOnInit(): void {
    this.fullForm = new PersonForm(PersonContextEnum.Person);
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };

  //override submit success function
  onSubmitSuccess(data: any) {
    this.router.navigateByUrl(this.PERSON_OVERVIEW_URL);
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: PersonModel) {
    return new PersonModel(object);
  }
}
