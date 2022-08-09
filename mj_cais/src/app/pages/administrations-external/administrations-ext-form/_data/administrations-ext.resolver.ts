import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { AdministrationsExtModel } from "../_models/administrations-ext.model";
import { AdministrationsExtService } from "./administrations-ext.service";

@Injectable({ providedIn: "root" })
export class AdministrationsExtResolver implements Resolve<any> {
  constructor(
    private service: AdministrationsExtService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let userId = route.params["ID"];
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(userId) : of(null);

    let result: AdministrationsExtResolverData = {
      element: element,
      roleTypes: of([
        new BaseNomenclatureModel({id: 'ECertificates', name: 'Свидетелства'}),
        new BaseNomenclatureModel({id: 'EReports', name: 'Справки'})
        ])
    };
    return forkJoin(result);
  }
}

export class AdministrationsExtResolverData extends BaseResolverData<AdministrationsExtModel> {
  public roleTypes: Observable<BaseNomenclatureModel[]>;
}
