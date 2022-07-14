import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../../@core/services/rest/nomenclature.service";
import { EcrisMessageService } from "../../_data/ecris-message.service";
import { EcrisMessageModel } from "../../_models/ecris-message.model";

@Injectable({
  providedIn: "root",
})
export class EcrisIdentificationResolver implements Resolve<any> {
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

    let result: EcrisIdentificationResolverData = {
      element: element,
      ecrisAuthorities: this.nomenclatureService.getEcrisAuthorities(),
      genderTypes: this.nomenclatureService.getGenderTypes(),
      countries: this.nomenclatureService.getCountries(),
      ecrisMsgNames: this.service.getEcrisMsgNames(id),
      graoPeople: this.service.getGraoPeople(id),
    };
    return forkJoin(result);
  }
}

export class EcrisIdentificationResolverData extends BaseResolverData<EcrisMessageModel> {
  public ecrisAuthorities: Observable<BaseNomenclatureModel[]>;
  public genderTypes: Observable<BaseNomenclatureModel[]>;
  public countries: Observable<BaseNomenclatureModel[]>;
}
