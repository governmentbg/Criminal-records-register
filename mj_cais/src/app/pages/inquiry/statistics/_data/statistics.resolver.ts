import { Injectable } from "@angular/core";
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../@core/services/rest/nomenclature.service";
import { StatisticsSearchModel } from "../_model/statistics-search.model";
import { StatisticsService } from "./statistics.service";

@Injectable({
  providedIn: "root",
})
export class StatisticsResolver implements Resolve<any> {
  constructor(
    private service: StatisticsService,
    private nomenclatureService: NomenclatureService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let element = of(null);

    let result: StatisticsResolverData = {
      element: element,
      authorities: this.nomenclatureService.getCsAuthorities(),
    };

    return forkJoin(result);
  }
}

export class StatisticsResolverData extends BaseResolverData<StatisticsSearchModel> {
  public authorities: Observable<BaseNomenclatureModel[]>;
}
