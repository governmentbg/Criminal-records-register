import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonArchiveGridModel } from "../_models/person-archive-grid.model";

const currentEndpoint = "people/archive";

@Injectable({
  providedIn: "root",
})
export class PersonArchiveGridService extends CaisCrudService<
  PersonArchiveGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonArchiveGridModel, injector, currentEndpoint);
  }

  public setPersonId(personId: string) {
    this.updateUrl(`${currentEndpoint}?personId=${personId}`);
  }
}
