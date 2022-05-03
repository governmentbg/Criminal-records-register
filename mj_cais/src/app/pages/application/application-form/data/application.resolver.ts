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
import { ApplicationModel } from "../models/application.model";
import { ApplicationService } from "./application.service";

@Injectable({ providedIn: "root" })
export class ApplicationResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: ApplicationService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let applicationId = route.params["ID"];
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(applicationId) : of(null);

    let result: ApplicationResolverData = {
      element: element,
    };
    return forkJoin(result);
  }
}

export class ApplicationResolverData extends BaseResolverData<ApplicationModel> {}
