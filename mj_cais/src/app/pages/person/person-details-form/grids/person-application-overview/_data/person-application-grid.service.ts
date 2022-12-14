import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonApplicationGridModel } from "../_models/person-application-grid.model";

const currentEndpoint = "people/applications";

@Injectable({
  providedIn: "root",
})
export class PersonApplicationGridService extends CaisCrudService<
  PersonApplicationGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonApplicationGridModel, injector, currentEndpoint);
  }

  public setPersonId(personId: string) {
    this.updateUrl(`${currentEndpoint}?personId=${personId}`);
  }
}
