import { HttpParams } from "@angular/common/http";
import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinGridModel } from "../_models/bulletin-grid.model";

const currentEndpoint = "bulletins";

@Injectable({ providedIn: "root" })
export class BulletinGridService extends CaisCrudService<
  BulletinGridModel,
  string
> {
  constructor(injector: Injector) {
    super(BulletinGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statusId?: string) {
    if (statusId) {
      this.updateUrl(`${currentEndpoint}?statusId=${statusId}`);
    } else {
      this.updateUrl(`${currentEndpoint}`);
    }
  }

  public changeStatus(aId: string, statusId: string): Observable<any> {
    return this.http.put(
      this.baseUrl + `/api/bulletins/${aId}/change-status/${statusId}`,
      {}
    );
  }

  public deleteBulletin(aId: string): Observable<any> {
    return this.http.put(this.baseUrl + `/api/bulletins/${aId}/delete`, {});
  }
}
