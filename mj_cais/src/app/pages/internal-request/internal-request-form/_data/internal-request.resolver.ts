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
import { InternalRequestModel } from "../_models/internal-request.model";
import { PersonBulletinsGridExtendedModel } from "../_models/person-bulletin-grid-extended-model";
import { PersonBulletinsGridModel } from "../_models/person-bulletin-grid-model";
import { InternalRequestService } from "./internal-request.service";

@Injectable({
  providedIn: "root",
})
export class InternalRequestResolver implements Resolve<any> {
  constructor(
    private service: InternalRequestService,
    private nomenclatureService: NomenclatureService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let id = route.params["ID"];
    let isEdit = route.data["edit"];
    let bulletinId = route.queryParams["bulletinId"];
    let personId = route.queryParams["personId"];
    let element = isEdit ? this.service.find(id) : of(null);

    let result: InternalRequestResolverData = {
      element: element,
      requestTypes: this.nomenclatureService.getInternalRequestTypes(),
      csAuthorities: this.nomenclatureService.getCsAuthorities(),
      personBulletins: this.service.getSelectedBulltins(id),
      bulletinInfo: this.service.getBulletinWithPidData(bulletinId),
      bulletinsInfoByPerson:
        this.service.getBulletinWithPidDataByPersonId(personId),
    };
    return forkJoin(result);
  }
}

export class InternalRequestResolverData extends BaseResolverData<InternalRequestModel> {
  public requestTypes: Observable<BaseNomenclatureModel[]>;
  public csAuthorities: Observable<BaseNomenclatureModel[]>;
  public personBulletins: Observable<PersonBulletinsGridModel[]>;
  public bulletinInfo: Observable<PersonBulletinsGridExtendedModel>;
  public bulletinsInfoByPerson: Observable<PersonBulletinsGridExtendedModel>;
}
