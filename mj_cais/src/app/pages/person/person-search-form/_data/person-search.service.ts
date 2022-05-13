import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { PersonSearchModel } from "../_models/person-search.model";

@Injectable({
  providedIn: "root",
})
export class PersonSearchService extends CaisCrudService<
  PersonSearchModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonSearchModel, injector, "people/search");
  }
}
