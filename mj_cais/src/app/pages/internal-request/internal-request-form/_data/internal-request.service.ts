import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { InternalRequestModel } from "../_models/internal-request.model";

@Injectable({
  providedIn: "root",
})
export class InternalRequestService extends CaisCrudService<
  InternalRequestModel,
  string
> {
  constructor(injector: Injector) {
    super(InternalRequestModel, injector, "internal-requests");
  }

  public changeStatus(aId: string, statusId: string): Observable<any> {
    return this.http.put(`${this.url}/${aId}/change-status/${statusId}`, {});
  }

  public getRequestsCount() {
    return this.http.get<any>(`${this.url}/requests-count`);
  }
}
