import { Injectable } from "@angular/core";
import {
  Router,
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../@core/services/rest/nomenclature.service";
import { DailyStatisticsSearchModel } from "../_models/daily-statistics-search.model";
import { DailyStatisticsService } from "./daily-statistics.service";

@Injectable({
  providedIn: "root",
})
export class DailyStatisticsResolver implements Resolve<any> {
  constructor(
    private service: DailyStatisticsService,
    private nomenclatureService: NomenclatureService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let element = of(null);

    let result: DailyStatisticsResolverData = {
      element: element,
      statisticsTypes: this.nomenclatureService.getDailyStatisticsTypes(),
      aplicationStatuses: this.nomenclatureService.getApplicationStatues(),
      reportStatuses: this.nomenclatureService.getReportStatues(),
      buletinStatuses: this.nomenclatureService.getBulletinStatuses(),
    };

    return forkJoin(result);
  }
}

export class DailyStatisticsResolverData extends BaseResolverData<any> {
  public statisticsTypes: Observable<BaseNomenclatureModel[]>;
  public aplicationStatuses: Observable<BaseNomenclatureModel[]>;
  public reportStatuses: Observable<BaseNomenclatureModel[]>;
  public buletinStatuses: Observable<BaseNomenclatureModel[]>;
}
