import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { FbbcGridModel } from "../_models/fbbc-grid.model";

const currentEndpoint = "fbbcs";
@Injectable({ providedIn: "root" })
export class FbbcGridService extends CaisCrudService<FbbcGridModel, string> {
  constructor(injector: Injector) {
    super(FbbcGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statusId: string) {
    this.updateUrl(`${currentEndpoint}?statusId=${statusId}`);
  }

  public changeStatus(aId: string, statusId: string): Observable<any> {
    return this.http.put(
      this.baseUrl + `/api/fbbcs/${aId}/change-status/${statusId}`,
      {}
    );
  }
}