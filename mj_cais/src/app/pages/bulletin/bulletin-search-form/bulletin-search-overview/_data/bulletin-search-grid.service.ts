import { Injectable, Injector } from "@angular/core";
import { map } from "rxjs";
import { CaisCrudService } from "../../../../../@core/services/rest/cais-crud.service";
import { BulletinSearchGridModel } from "../_models/bulletin-search-grid.model";

const currentEndpoint = "bulletins/search";

@Injectable({
  providedIn: "root",
})
export class BulletinSearchGridService extends CaisCrudService<
  BulletinSearchGridModel,
  string
> {
  constructor(injector: Injector) {
    super(BulletinSearchGridModel, injector, currentEndpoint);
  }

  public excelExportBulletins(queryParams) {
    let urlResult = `${this.baseUrl}/api/bulletins/export?`;
    if (queryParams) {
      urlResult += queryParams;
    }

    return this.http.get<BulletinSearchGridModel[]>(urlResult);
  }
}
