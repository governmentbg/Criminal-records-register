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
import { EcrisMessageModel } from "../models/ecris-message.model";
import { EcrisMessageService } from "./ecris-message.service";

@Injectable({
  providedIn: "root",
})
export class EcrisMessageResolver implements Resolve<any> {
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

    let result: EcrisMessageResolverData = {
      element: element,
      ecrisAuthorities: this.nomenclatureService.getEcrisAuthorities(),
    };
    return forkJoin(result);
  }
}

export class EcrisMessageResolverData extends BaseResolverData<EcrisMessageModel> {
  public ecrisAuthorities: Observable<BaseNomenclatureModel[]>;
}
