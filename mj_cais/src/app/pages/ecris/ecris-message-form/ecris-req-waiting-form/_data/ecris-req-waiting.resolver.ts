import { Injectable } from "@angular/core";
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../../@core/services/rest/nomenclature.service";
import { BulletinGridModel } from "../../../../bulletin/bulletin-overview/_models/bulletin-grid.model";
import { FbbcGridModel } from "../../../../fbbc/fbbc-overview/_models/fbbc-grid.model";
import { EcrisMessageModel } from "../../_models/ecris-message.model";
import { EcrisMessageService } from "../../_data/ecris-message.service";

@Injectable({
  providedIn: "root",
})
export class EcrisReqWaitingResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: EcrisMessageService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let id = route.params["ID"];
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(id) : of(null);

    let result: EcrisReqWaitingResolverData = {
      element: element,
      ecrisAuthorities: this.nomenclatureService.getEcrisAuthorities(),
      genderTypes: this.nomenclatureService.getGenderTypes(),
      countries: this.nomenclatureService.getCountries(),
      ecrisBulletins: this.service.getEcrisBulletins(id),
      ecrisFbbcs: this.service.getEcrisFbbcs(id),
    };
    return forkJoin(result);
  }
}

export class EcrisReqWaitingResolverData extends BaseResolverData<EcrisMessageModel> {
  public ecrisAuthorities: Observable<BaseNomenclatureModel[]>;
  public genderTypes: Observable<BaseNomenclatureModel[]>;
  public countries: Observable<BaseNomenclatureModel[]>;
  public ecrisBulletins: Observable<BulletinGridModel[]>;
  public ecrisFbbcs: Observable<FbbcGridModel[]>;
}
