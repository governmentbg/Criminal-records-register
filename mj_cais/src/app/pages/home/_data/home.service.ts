import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../@core/services/rest/cais-crud.service";
import { ApplicationCountModel } from "../_models/application-count.model";
import { BulletinCountModel } from "../_models/bulletin-count.model";
import { BulletinEventCountModel } from "../_models/bulletin-event-count.model";
import { EcrisCountModel } from "../_models/ecris-count.model";
import { IsinCountModel } from "../_models/isin-count.model";
import { ObjectCountModel } from "../_models/object-count.model";

@Injectable({
  providedIn: "root",
})
export class HomeService extends CaisCrudService<ObjectCountModel, string> {
  constructor(injector: Injector) {
    super(ObjectCountModel, injector, "home");
  }

  public getBulletinsCount(): Observable<BulletinCountModel> {
    return this.http.get<BulletinCountModel>(`${this.url}/bulletin-count`);
  }

  public getBulletinEventsCount(): Observable<BulletinEventCountModel> {
    return this.http.get<BulletinEventCountModel>(
      `${this.url}/bulletin-event-count`
    );
  }

  public getIsinCount(): Observable<IsinCountModel> {
    return this.http.get<IsinCountModel>(`${this.url}/isin-count`);
  }

  public getEcrisCount(): Observable<EcrisCountModel> {
    return this.http.get<EcrisCountModel>(`${this.url}/ecris-count`);
  }

  public getApplicationsCount(): Observable<ApplicationCountModel> {
    return this.http.get<ApplicationCountModel>(
      `${this.url}/application-count`
    );
  }
}
