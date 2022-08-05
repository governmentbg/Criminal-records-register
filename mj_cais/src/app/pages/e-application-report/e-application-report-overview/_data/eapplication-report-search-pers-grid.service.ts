import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EApplicationReportSearchPersGridModel } from "../_models/eapplication-report-search-pers-grid.model";

@Injectable({
  providedIn: "root",
})
export class EApplicationReportSearchPersGridService extends CaisCrudService<
  EApplicationReportSearchPersGridModel,
  string
> {
  constructor(injector: Injector) {
    super(
      EApplicationReportSearchPersGridModel,
      injector,
      "e-applicaiton-reports/search-pers"
    );
  }
}
