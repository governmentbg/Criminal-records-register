import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
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

  public getBulletinPersonInfo(bulletinId: boolean): Observable<BulletinPersonInfoModel> {
    return this.http.get<BulletinPersonInfoModel>(
      `${this.url}/bulletin-person-info/${bulletinId}`
    );
  }
}
