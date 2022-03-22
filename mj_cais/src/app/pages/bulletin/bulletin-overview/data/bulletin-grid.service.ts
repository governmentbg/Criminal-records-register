import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinGridModel } from "../models/bulletin-grid.model";

@Injectable({ providedIn: "root" })
export class BulletinGridService extends CaisCrudService<
  BulletinGridModel,
  string
> {
  constructor(injector: Injector) {
    super(BulletinGridModel, injector, "bulletins");
  }

  public changeStatus(aId: string, statusId: string): Observable<any> {
    return this.http.put(
      this.baseUrl + `/api/bulletins/${aId}/change-status/${statusId}`,
      {}
    );
  }
}
