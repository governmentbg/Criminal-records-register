import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ReportApplicationModel } from "../_models/report-application.model";

@Injectable({
  providedIn: "root",
})
export class ReportApplicationService extends CaisCrudService<
  ReportApplicationModel,
  string
> {
  constructor(injector: Injector) {
    super(ReportApplicationModel, injector, "applications");
  }
}
