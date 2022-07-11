import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonFbbcGridModel } from "../_models/person-bulletin-grid-model";

const currentEndpoint = "people/fbbcs";

@Injectable({
  providedIn: "root",
})
export class PersonFbbcGridService extends CaisCrudService<
  PersonFbbcGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonFbbcGridModel, injector, currentEndpoint);
  }

  public setPersonId(personId: string) {
    this.updateUrl(`${currentEndpoint}?personId=${personId}`);
  }
}
