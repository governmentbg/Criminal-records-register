import { Injectable } from "@angular/core";
import {
  Router,
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from "@angular/router";
import { forkJoin, Observable, ObservableInput, of } from "rxjs";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../@core/services/nomenclature.service";
import { BulletinModel } from "./bulletin.model";
import { BulletinService } from "./bulletin.service";

@Injectable({
  providedIn: "root",
})
export class BulletinResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: BulletinService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(route.params["ID"]) : of(null);

    let result: BulletinResolverData = {
      element: element,
      countries: this.nomenclatureService.getCountries(),
    };
    return forkJoin(result);
  }
}

export class BulletinResolverData
  implements Record<string, ObservableInput<any>>
{
  [x: string]: ObservableInput<any>;
  public element: Observable<BulletinModel>;
  public countries: Observable<BaseNomenclatureModel[]>;
}
