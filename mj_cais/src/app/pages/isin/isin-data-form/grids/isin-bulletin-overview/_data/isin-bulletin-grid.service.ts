import { Injectable, Injector } from "@angular/core";
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
      `${this.url}/${id}/select-bulletin/${bulletinId}`,
      []
    );
  }
}