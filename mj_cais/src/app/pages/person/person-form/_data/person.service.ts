import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { PersonModel } from "../_models/person.model";

@Injectable({
  providedIn: "root",
})
export class PersonService extends CaisCrudService<PersonModel, string> {
  constructor(injector: Injector) {
    super(PersonModel, injector, "people");
  }
}
