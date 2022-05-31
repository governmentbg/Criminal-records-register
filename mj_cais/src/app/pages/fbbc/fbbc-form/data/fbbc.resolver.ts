import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from "@angular/router";
import { forkJoin, Observable, of } from "rxjs";
import { BaseResolverData } from "../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../@core/services/rest/nomenclature.service";
import { EcrisMessageGridModel } from "../../../ecris/ecris-message-overivew/_models/ecris-message-grid.model";
import { FbbcDocumentModel } from "../models/fbbc-document.model";
import { FbbcModel } from "../models/fbbc.model";
import { FbbcService } from "./fbbc.service";

@Injectable({ providedIn: "root" })
export class FbbcResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: FbbcService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let fbbcId = route.params["ID"];
    let isEdit = route.data["edit"];
    let personId = route.queryParams["personId"];
    let element: any = of(null);

    if (isEdit) {
      element = this.service.find(fbbcId);
    } else if (personId) {
      element = this.service.getWithPersonData(personId);
    } else {
      element = of(null);
    }

    let result: FbbcResolverData = {
      element: element,
      docTypeId: this.nomenclatureService.getFbbcDocTypes(),
      countries: this.nomenclatureService.getCountries(),
      cities: this.nomenclatureService.getAllCities(),
      sanctTypes: this.nomenclatureService.getFbbcSanctTypes(),
      documents: this.service.getDocuments(fbbcId),
      ecrisMessages: this.service.getEcrisMessages(fbbcId),
    };
    return forkJoin(result);
  }
}

export class FbbcResolverData extends BaseResolverData<FbbcModel> {
  public docTypeId: Observable<BaseNomenclatureModel[]>;
  public countries: Observable<BaseNomenclatureModel[]>;
  public cities: Observable<BaseNomenclatureModel[]>;
  public sanctTypes: Observable<BaseNomenclatureModel[]>;
  public documents: Observable<FbbcDocumentModel[]>;
  public ecrisMessages: Observable<EcrisMessageGridModel[]>;
}
