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
import { ReportApplicationModel } from "../_models/report-application.model";
import { ReportApplicationService } from "./report-application.service";

@Injectable({
  providedIn: "root",
})
export class ReportApplicationResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: ReportApplicationService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let reportAppId = route.params["ID"];
    let isEdit = route.data["edit"];
    let personId = route.queryParams["personId"];
    let element: any = of(null);

    if (isEdit) {
      element = this.service.find(reportAppId);
    } else if (personId) {
      // todo: load from search person form
      //element = this.service.getWithPersonData(personId);
    } else {
      element = of(null);
    }

    let result: ReportApplicationResolverData = {
      element: element,
      purposeIds: this.nomenclatureService.getPurposes(),
      users: this.nomenclatureService.getUsers(), // todo? remote
      countries: this.nomenclatureService.getCountries(),
    };
    return forkJoin(result);
  }
}

export class ReportApplicationResolverData extends BaseResolverData<ReportApplicationModel> {
  public purposeIds: Observable<BaseNomenclatureModel[]>;
  public users: Observable<BaseNomenclatureModel[]>;
  public countries: Observable<BaseNomenclatureModel[]>;
}
