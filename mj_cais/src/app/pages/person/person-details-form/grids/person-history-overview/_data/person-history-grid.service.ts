import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonHistoryGridModel } from "../_models/person-history-grid.model";

const currentEndpoint = "people/person-history";

@Injectable({
  providedIn: "root",
})
export class PersonHistoryGridService extends CaisCrudService<
  PersonHistoryGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonHistoryGridModel, injector, currentEndpoint);
  }

  public setPersonId(personId: string) {
    this.updateUrl(`${currentEndpoint}?personId=${personId}`);
  }
}
