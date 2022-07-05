import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../@core/services/rest/cais-crud.service";
import { StatisticsResultModel } from "../_models/statistics-result.model";
import { StatisticsSearchModel } from "../_models/statistics-search.model";

const currentEndpoint = "statistics";

@Injectable({
  providedIn: "root",
})
export class StatisticsService extends CaisCrudService<
  StatisticsSearchModel,
  string
> {
  constructor(injector: Injector) {
    super(StatisticsSearchModel, injector, currentEndpoint);
  }

  public isLoading: false;

  public getStatistics(queryParams) {
    let urlResult = `${this.baseUrl}/api/${currentEndpoint}/`;
    if (queryParams) {
      urlResult += queryParams;
    }

    return this.http.get<StatisticsResultModel[]>(urlResult);
  }
}
