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
import { BulletinSearchService } from "./bulletin-search.service";

@Injectable({
  providedIn: "root",
})
export class BulletinSearchResolver implements Resolve<any> {
  constructor(
    private service: BulletinSearchService,
    private nomenclatureService: NomenclatureService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let element = of(null);

    let result: BulletinSearchResolverData = {
      element: element,
      bulletinTypes: this.nomenclatureService.getBulletinTypes(),
      statuses: this.nomenclatureService.getBulletinStatuses(),
    };

    return forkJoin(result);
  }
}

export class BulletinSearchResolverData extends BaseResolverData<any> {
  public bulletinTypes: Observable<BaseNomenclatureModel[]>;
  public statuses: Observable<BaseNomenclatureModel[]>;
}
