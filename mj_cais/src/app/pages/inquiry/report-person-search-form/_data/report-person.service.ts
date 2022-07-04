import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ReportPersonSearchModel } from "../_models/report-person-search.model";

@Injectable({
  providedIn: "root",
})
export class ReportPersonService extends CaisCrudService<
  ReportPersonSearchModel,
  string
> {
  constructor(injector: Injector) {
    super(ReportPersonSearchModel, injector, "inquiry");
  }
}
