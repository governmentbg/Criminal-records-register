import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EApplicationReportGridModel } from "../_models/eapplication-report-grid.model";

@Injectable({
  providedIn: "root",
})
export class EApplicationReportGridService extends CaisCrudService<
  EApplicationReportGridModel,
  string
> {
  constructor(injector: Injector) {
    super(
      EApplicationReportGridModel,
      injector,
      "e-applicaiton-reports/reports"
    );
  }
}
