import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { DailyStatisticsSearchModel } from "../_models/daily-statistics-search.model";

const currentEndpoint = "statistics";

@Injectable({
  providedIn: "root",
})
export class DailyStatisticsService extends CaisCrudService<
  DailyStatisticsSearchModel,
  string
> {
  constructor(injector: Injector) {
    super(DailyStatisticsSearchModel, injector, currentEndpoint);
  }

  getReport(filterQuerry) {
    let url = `${this.url}/daily-statistics?${filterQuerry}`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }
  //public isLoading: false;
}
