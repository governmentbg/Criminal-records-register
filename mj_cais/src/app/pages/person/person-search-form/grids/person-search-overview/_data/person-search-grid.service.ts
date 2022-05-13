import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonSearchGridModel } from "../_models/person-search.grid";

@Injectable({
  providedIn: "root",
})
export class PersonSearchGridService extends CaisCrudService<
  PersonSearchGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonSearchGridModel, injector, "people");
  }
}
