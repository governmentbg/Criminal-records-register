import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisTcnGridModel } from "../models/ecris-tcn-grid.model";

const currentEndpoint = "ecris-tcns";

@Injectable({ providedIn: "root" })
export class EcrisTcnGridService extends CaisCrudService<
  EcrisTcnGridModel,
  string
> {
  constructor(injector: Injector) {
    super(EcrisTcnGridModel, injector, currentEndpoint);
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
      this.baseUrl + `/api/ecris-tcns/${aId}/change-status/${statusId}`,
      {}
    );
  }
}
