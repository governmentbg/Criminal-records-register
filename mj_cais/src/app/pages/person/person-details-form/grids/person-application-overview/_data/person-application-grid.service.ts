import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonApplicationGridModel } from "../_models/person-bulletin-grid.model";

@Injectable({
  providedIn: "root",
})
export class PersonApplicationGridService extends CaisCrudService<
  PersonApplicationGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonApplicationGridModel, injector, "people/applications");
  }
}
