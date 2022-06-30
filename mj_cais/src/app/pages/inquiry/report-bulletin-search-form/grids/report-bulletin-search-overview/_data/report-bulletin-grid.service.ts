import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { ReportBulletinGridModel } from "../_models/report-bulletin-grid.model";

@Injectable({
  providedIn: "root",
})
export class ReportBulletinGridService extends CaisCrudService<
  ReportBulletinGridModel,
  string
> {
  constructor(injector: Injector) {
    super(ReportBulletinGridModel, injector, "inquiry");
  }
}
