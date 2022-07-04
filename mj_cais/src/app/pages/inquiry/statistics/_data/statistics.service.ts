import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { StatisticsResultModel } from "../_model/statistics-result.model";
import { StatisticsSearchModel } from "../_model/statistics-search.model";

@Injectable({
  providedIn: "root",
})
export class StatisticsService extends CaisCrudService<StatisticsSearchModel, string> {
  constructor(injector: Injector) {
    super(StatisticsSearchModel, injector, "inquiry/statistics");
  }

  public isLoading: false;
  
  public getStatistics(queryParams) {
    let urlResult = `${this.url}?`;
    if (queryParams) {
      urlResult += queryParams;
    }

    return this.http.get<StatisticsResultModel[]>(urlResult);
  }
}
