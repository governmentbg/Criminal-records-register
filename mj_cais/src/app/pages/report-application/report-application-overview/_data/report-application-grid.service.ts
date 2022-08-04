import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ReportApplicationGridModel } from "../_models/report-application-grid.model";

const currentEndpoint = "a-report-applications";
@Injectable({
  providedIn: "root",
})
export class ReportApplicationGridService extends CaisCrudService<
  ReportApplicationGridModel,
  string
> {
  constructor(injector: Injector) {
    super(ReportApplicationGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statusId?: string) {
    if (statusId) {
      this.updateUrl(`${currentEndpoint}?statusId=${statusId}`);
    } else {
      this.updateUrl(`${currentEndpoint}`);
    }
  }
}
