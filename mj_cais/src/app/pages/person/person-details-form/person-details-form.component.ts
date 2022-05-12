import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { elementAt } from "rxjs-compat/operator/elementAt";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";
import { PersonModel } from "../../../@core/components/forms/person-form/_models/person.model";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { PersonDetailsResolverData } from "./_data/person-details.resolver";
import { PersonDetailsService } from "./_data/person-details.service";

@Component({
  selector: "cais-person-details-form",
  templateUrl: "./person-details-form.component.html",
  styleUrls: ["./person-details-form.component.scss"],
})
export class PersonDetailsFormComponent
  extends CrudForm<PersonModel, null, PersonDetailsResolverData, PersonDetailsService>
  implements OnInit
{
  constructor(service: PersonDetailsService, public injector: Injector) {
    super(service, injector);
  }

  public personId: string;
  public model: PersonModel;
  public PersonContextEnum = PersonContextEnum;

  ngOnInit(): void {
    debugger;
    this.model = this.dbData.element as any;
    this.personId = this.model.id;
  }

  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: PersonModel) {
    return null;
  }
}
