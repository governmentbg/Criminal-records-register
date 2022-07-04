import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { ReportBulletinByPersonGridModel } from "../_models/report-bulletin-by-person.model";

@Injectable({
  providedIn: "root",
})
export class ReportBulletinByPersonGridService extends CaisCrudService<
  ReportBulletinByPersonGridModel,
  string
> {
  constructor(injector: Injector) {
    super(ReportBulletinByPersonGridModel, injector, "inquiry");
  }
}
