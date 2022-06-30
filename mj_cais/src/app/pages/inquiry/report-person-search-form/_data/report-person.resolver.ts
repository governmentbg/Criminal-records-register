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
import { ReportPersonSearchModel } from "../_models/report-person-search.model";
import { ReportPersonService } from "./report-person.service";

@Injectable({
  providedIn: "root",
})
export class ReportPersonResolver implements Resolve<any> {
  constructor(
    private service: ReportPersonService,
    private nomenclatureService: NomenclatureService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let element = of(null);

    let result: ReportPersonResolverData = {
      element: element,
      genderTypes: this.nomenclatureService.getGenderTypes(),
    };

    return forkJoin(result);
  }
}

export class ReportPersonResolverData extends BaseResolverData<ReportPersonSearchModel> {
  public genderTypes: Observable<BaseNomenclatureModel[]>;
}
