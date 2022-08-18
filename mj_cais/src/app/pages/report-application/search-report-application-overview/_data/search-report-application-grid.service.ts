import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { SearchReportApplicationGridModel } from "../_models/search-report-application-grid.model";

@Injectable({ providedIn: "root" })
export class SearchReportApplicationGridService extends CaisCrudService<
  SearchReportApplicationGridModel,
  string
> {
  constructor(injector: Injector) {
    super(SearchReportApplicationGridModel, injector, "report-application/search");
  }
}
