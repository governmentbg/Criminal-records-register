import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinAdministrationGridModel } from "../_models/bulletin-administration-grid.model";

@Injectable({
  providedIn: "root",
})
export class BulletinAdministrationGridService extends CaisCrudService<
  BulletinAdministrationGridModel,
  string
> {
  constructor(injector: Injector) {
    super(
      BulletinAdministrationGridModel,
      injector,
      "bulletins-administration"
    );
  }

  unlockBulletin(id: string, model: any): Observable<any> {
    return this.http.put<any>(this.url + "/" + id, model, {});
  }
}
