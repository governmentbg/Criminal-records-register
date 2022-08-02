import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../@core/services/rest/cais-crud.service";
import { ApplicationCountModel } from "../_models/application-count.model";
import { BulletinCountModel } from "../_models/bulletin-count.model";
import { CentralAuthCountModel } from "../_models/central-auth-count.model";

@Injectable({
  providedIn: "root",
})
export class HomeService extends CaisCrudService<any, string> {
  constructor(injector: Injector) {
    super(null, injector, "home");
  }

  public getBulletinsCount(): Observable<BulletinCountModel> {
    return this.http.get<BulletinCountModel>(`${this.url}/bulletin-count`);
  }

  public getCentralAuthCount(): Observable<CentralAuthCountModel> {
    return this.http.get<CentralAuthCountModel>(`${this.url}/central-auth-count`);
  }

  public getApplicationsCount(): Observable<ApplicationCountModel> {
    return this.http.get<ApplicationCountModel>(
      `${this.url}/application-count`
    );
  }
}
