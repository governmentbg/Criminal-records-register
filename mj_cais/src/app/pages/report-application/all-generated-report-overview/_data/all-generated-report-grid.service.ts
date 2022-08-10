import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { AllGeneratedReportGridModel } from "../_models/all-generated-report-grid.model";

const currentEndpoint = "a-report-applications/all-generated-reports";

@Injectable({
  providedIn: "root",
})
export class AllGeneratedReportGridService extends CaisCrudService<
  AllGeneratedReportGridModel,
  string
> {
  constructor(injector: Injector) {
    super(AllGeneratedReportGridModel, injector, currentEndpoint);
  }
}
