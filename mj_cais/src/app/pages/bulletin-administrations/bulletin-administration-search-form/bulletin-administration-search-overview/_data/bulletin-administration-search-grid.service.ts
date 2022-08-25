import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { BaseNomenclatureModel } from "../../../../../@core/models/nomenclature/base-nomenclature.model";
import { CaisCrudService } from "../../../../../@core/services/rest/cais-crud.service";
import { BulletinAdministrationGridModel } from "../_models/bulletin-administration-grid.model";

const currentEndpoint = "bulletins-administration";

@Injectable({
  providedIn: "root",
})
export class BulletinAdministrationSearchGridService extends CaisCrudService<
  BulletinAdministrationGridModel,
  string
> {
  constructor(injector: Injector) {
    super(BulletinAdministrationGridModel, injector, currentEndpoint);
  }

  unlockBulletin(id: string, model: any): Observable<any> {
    return this.http.put<any>(
      `${this.baseUrl}/api/${currentEndpoint}/${id}/`,
      model,
      {}
    );
  }

  public getBulletinStatusHistory(
    id: string
  ): Observable<BaseNomenclatureModel[]> {
    return this.http.get<BaseNomenclatureModel[]>(
      `${this.baseUrl}/api/${currentEndpoint}/${id}/bulletin-statuses-history`
    );
  }
}
