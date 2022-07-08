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
      nationalityTypes: this.nomenclatureService.getNationalityTypes(),
      authorities: this.nomenclatureService.getCsAuthorities(),
    };

    return forkJoin(result);
  }
}

export class ReportPersonResolverData extends BaseResolverData<any> {
  public genderTypes: Observable<BaseNomenclatureModel[]>;
  public nationalityTypes: Observable<BaseNomenclatureModel[]>;
  public authorities: Observable<BaseNomenclatureModel[]>;
}
