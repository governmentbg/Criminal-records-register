import { Injectable, Injector } from "@angular/core";
import { environment } from "../../../../../../../environments/environment";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { IsinBulletinGridModel } from "../_models/isin-bulletin-grid.model";

@Injectable({
  providedIn: "root",
})
export class IsinBulletinGridService extends CaisCrudService<
  IsinBulletinGridModel,
  string
> {
  constructor(injector: Injector) {
    super(IsinBulletinGridModel, injector, "isin-data/bulletins");
  }

  selectBulletin(id: string, bulletinId: string) {
    return this.http.post<any>(
      environment.apiUrl + `/isin-data/${id}/select-bulletin/${bulletinId}`,
      []
    );
  }
}