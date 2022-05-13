import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonBulletinGridModel } from "../_models/person-bulletin-grid.model";

const currentEndpoint = "people/bulletins";

@Injectable({
  providedIn: "root",
})
export class PersonBulletinGridService extends CaisCrudService<
  PersonBulletinGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonBulletinGridModel, injector, currentEndpoint);
  }

  public setPersonId(personId: string) {
    this.updateUrl(`${currentEndpoint}?personId=${personId}`);
  }
}
