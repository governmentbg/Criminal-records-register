import { Injectable, Injector } from "@angular/core";
import { Observable, of } from "rxjs";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinTypeConstants } from "../../../bulletin/bulletin-form/_models/bulletin-type-constants";
import { ReportBulletinSearchModel } from "../_models/report-bulletin-search.model";

@Injectable({
  providedIn: "root",
})
export class ReportBulletinService extends CaisCrudService<
  ReportBulletinSearchModel,
  string
> {
  constructor(injector: Injector) {
    super(ReportBulletinSearchModel, injector, "inquiry");
  }

  public getBulletinTypes(): Observable<BaseNomenclatureModel[]> {
    return of(BulletinTypeConstants.allData);
  }
}
