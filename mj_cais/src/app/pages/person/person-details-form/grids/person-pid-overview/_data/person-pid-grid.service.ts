import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonPidGridModel } from "../_models/person-pid-grid.model";

const currentEndpoint = "people/pids";

@Injectable({
  providedIn: "root",
})
export class PersonPidGridService extends CaisCrudService<
  PersonPidGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonPidGridModel, injector, currentEndpoint);
  }

  public setPersonId(personId: string) {
    this.updateUrl(`${currentEndpoint}?personId=${personId}`);
  }
}
