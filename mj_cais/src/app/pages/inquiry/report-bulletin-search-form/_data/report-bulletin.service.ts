import { Injectable, Injector } from "@angular/core";
import { Observable, of } from "rxjs";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinTypeConstants } from "../../../bulletin/bulletin-form/_models/bulletin-type-constants";
import { ExportBulletinModel } from "../../_models/export-bulletin.model";

@Injectable({
  providedIn: "root",
})
export class ReportBulletinService extends CaisCrudService<
  ExportBulletinModel,
  string
> {
  constructor(injector: Injector) {
    super(ExportBulletinModel, injector, "inquiry");
  }

  public getBulletinTypes(): Observable<BaseNomenclatureModel[]> {
    return of(BulletinTypeConstants.allData);
  }
}
