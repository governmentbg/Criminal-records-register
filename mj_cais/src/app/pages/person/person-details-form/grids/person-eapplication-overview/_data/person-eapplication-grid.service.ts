import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonEApplicationGridModel } from "../_models/person-eapplication-grid.model";

const currentEndpoint = "people/e-applications";

@Injectable({
  providedIn: "root",
})
export class PersonEApplicationGridService extends CaisCrudService<
  PersonEApplicationGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonEApplicationGridModel, injector, currentEndpoint);
  }

  public setPersonId(personId: string) {
    this.updateUrl(`${currentEndpoint}?personId=${personId}`);
  }
}
