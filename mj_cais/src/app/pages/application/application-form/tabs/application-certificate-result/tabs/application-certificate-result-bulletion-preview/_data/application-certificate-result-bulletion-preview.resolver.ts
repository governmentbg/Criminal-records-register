import { Injectable } from "@angular/core";
import {
  Resolve,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from "@angular/router";
import { Observable, of, forkJoin } from "rxjs";
import { BaseResolverData } from "../../../../../../../../@core/models/common/base-resolver.data";
import { BaseNomenclatureModel } from "../../../../../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../../../../../@core/services/rest/nomenclature.service";
import { ApplicationCertificateResultBulletionPreviewModel } from "../_model/application-certificate-result-bulletion-preview.model";
import { ApplicationCertificateResultBulletionPreviewService } from "./application-certificate-result-bulletion-preview.service";

@Injectable({
  providedIn: "root",
})
export class ApplicationCertificateResultBulletionPreviewResolver
  implements Resolve<any>
{
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: ApplicationCertificateResultBulletionPreviewService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let bulletineId = route.params["ID"];
    let isEdit = route.data["edit"];
    let personId = route.queryParams["personId"];
    let element: any = of(null);
    // if (isEdit) {
    //   element = this.service.find(bulletineId);
    // } else if (personId) {
    //   //element = this.service.getWithPersonData(personId);
    // } else {
      element = of(null);
    // }

    let result: ApplicationCertificateResultBulletionPreviewResolverData = {
      element: element,
      decisionTypes: this.nomenclatureService.getDecisionTypes(), //
      decidingAuthorities:
        this.nomenclatureService.getDecidingAuthoritiesForBulletins(), //
      caseTypes: this.nomenclatureService.getCaseTypes(), //
    };
    return forkJoin(result);
  }
}

export class ApplicationCertificateResultBulletionPreviewResolverData extends BaseResolverData<ApplicationCertificateResultBulletionPreviewModel> {
  public decisionTypes: Observable<BaseNomenclatureModel[]>;
  public decidingAuthorities: Observable<BaseNomenclatureModel[]>;
  public caseTypes: Observable<BaseNomenclatureModel[]>;
}
