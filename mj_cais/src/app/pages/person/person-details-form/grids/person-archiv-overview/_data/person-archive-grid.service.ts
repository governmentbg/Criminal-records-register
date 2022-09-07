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

  public downloadContent(id: string) {
    let url = `${this.baseUrl}/api/certificates/archive-content/${id}`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }
}
