import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../../../../@core/services/rest/cais-crud.service";
import { GraoPersonModel } from "../_models/grao-person.model";

@Injectable({
  providedIn: "root",
})
export class GraoPersonGridService extends CaisCrudService<
  GraoPersonModel,
  string
> {
  constructor(injector: Injector) {
    super(GraoPersonModel, injector, "ecris-messages");
  }

  public getGraoPeople(id: string): Observable<any> {
    return this.http.get<GraoPersonModel[]>(`${this.url}/${id}/grao-people`);
  }
}
