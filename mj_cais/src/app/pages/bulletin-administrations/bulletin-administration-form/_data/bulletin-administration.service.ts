import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinAdministrationModel } from "../_models/bulletin-administration.model";

@Injectable({
  providedIn: "root",
})
export class BulletinAdministrationService extends CaisCrudService<
  BulletinAdministrationModel,
  string
> {
  constructor(injector: Injector) {
    super(BulletinAdministrationModel, injector, "bulletins-administration");
  }

  public get(id: string): Observable<BulletinAdministrationModel> {
    return this.http.get<BulletinAdministrationModel>(`${this.url}/${id}`);
  }
}
