import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { PersonGridModel } from "../_models/person.grid.model";

@Injectable({
  providedIn: "root",
})
export class PersonGridService extends CaisCrudService<
  PersonGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonGridModel, injector, "people");
  }
}
