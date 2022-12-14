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
import { ReportBulletinService } from "./report-bulletin.service";

@Injectable({
  providedIn: "root",
})
export class ReportBulletinResolver implements Resolve<any> {
  constructor(
    private service: ReportBulletinService,
    private nomenclatureService: NomenclatureService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let element = of(null);

    let result: ReportBulletinResolverData = {
      element: element,
      bulletinTypes: this.service.getBulletinTypes(),
      caseTypes: this.nomenclatureService.getCaseTypes(),
      decisionTypes: this.nomenclatureService.getDecisionTypes(),
      decidingAuthorities:
        this.nomenclatureService.getDecidingAuthoritiesForBulletins(),
      statuses: this.nomenclatureService.getBulletinStatuses(),
      sanctionCategories: this.nomenclatureService.getSanctionCategories(),
      authorities: this.nomenclatureService.getCsAuthorities(),
    };

    return forkJoin(result);
  }
}

export class ReportBulletinResolverData extends BaseResolverData<any> {
  public bulletinTypes: Observable<BaseNomenclatureModel[]>;
  public caseTypes: Observable<BaseNomenclatureModel[]>;
  public decisionTypes: Observable<BaseNomenclatureModel[]>;
  public decidingAuthorities: Observable<BaseNomenclatureModel[]>;
  public statuses: Observable<BaseNomenclatureModel[]>;
  public sanctionCategories: Observable<BaseNomenclatureModel[]>;
  public authorities: Observable<BaseNomenclatureModel[]>;
}
