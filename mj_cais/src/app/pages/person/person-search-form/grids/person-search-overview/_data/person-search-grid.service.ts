import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { PersonSearchGridModel } from "../_models/person-search.grid";

@Injectable({
  providedIn: "root",
})
export class PersonSearchGridService extends CaisCrudService<
  PersonSearchGridModel,
  string
> {
  constructor(injector: Injector) {
    super(PersonSearchGridModel, injector, "people");
  }

  public connectPeople(model: any): Observable<any[]> {
    return this.http.post<any>(`${this.baseUrl}/api/people/connect`, model);
  }
}
