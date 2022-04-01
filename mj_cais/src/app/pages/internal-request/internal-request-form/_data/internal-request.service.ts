import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import { BulletinPersonInfoModel } from "../../../../@core/components/shared/bulletin-person-info/_models/bulletin-person-info.model";
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

  public getBulletinPersonInfo(id: string, isBulletinId: boolean): Observable<BulletinPersonInfoModel> {
    return this.http.get<BulletinPersonInfoModel>(
      environment.apiUrl + `/internal-requests/${id}/bulletin-person-info/${isBulletinId}`
    );
  }
}
