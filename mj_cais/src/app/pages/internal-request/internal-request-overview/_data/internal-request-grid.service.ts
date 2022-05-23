import { HttpParams } from "@angular/common/http";
import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { InternalRequestGridModel } from "../_models/internal-request-grid.model";

const currentEndpoint = "internal-requests";

@Injectable({
  providedIn: "root",
})
export class InternalRequestGridService extends CaisCrudService<
  InternalRequestGridModel,
  string
> {
  constructor(injector: Injector) {
    super(InternalRequestGridModel, injector, currentEndpoint);
  }

  public addOrderBy(params?: HttpParams): HttpParams {
    if (!params) {
      params = new HttpParams();
    }

    if (!params.has('$orderby')) {
      params = params.append('$orderby', 'requestDate desc');
    }

    return params;
  }
  
  public updateStatusUrl(statusId?: string, bulletinId?: string) {
    let url = `${currentEndpoint}`;

    if (bulletinId && statusId) {
      url += `?bulletinId=${bulletinId}&statusId=${statusId}`;
      this.updateUrl(url);
      return;
    }

    if (bulletinId) {
      url += `?bulletinId=${bulletinId}`;
      this.updateUrl(url);
      return;
    }

    if (statusId) {
      url += `?statusId=${statusId}`;
      this.updateUrl(url);
      return;
    }

    this.updateUrl(url);
  }
}
