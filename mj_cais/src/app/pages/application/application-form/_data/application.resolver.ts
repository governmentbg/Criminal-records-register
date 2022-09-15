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
import { ApplicationCertificateService } from "../tabs/application-certificate-result/_data/application-certificate.service";
import { ApplicationCertificateResultModel } from "../tabs/application-certificate-result/_models/application-certificate-result.model";
import { ApplicationDocumentModel } from "../_models/application-document.model";
import { ApplicationModel } from "../_models/application.model";
import { ApplicationService } from "./application.service";

@Injectable({ providedIn: "root" })
export class ApplicationResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: ApplicationService,
    private certificateService: ApplicationCertificateService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let applicationId = route.params["ID"];
    let isEdit = route.data["edit"];
    let personId = route.queryParams["personId"];
    let element: any = of(null);

    if (isEdit) {
      element = this.service.find(applicationId);
    } else if (personId) {
      element = this.service.getWithPersonData(personId);
    } else {
      element = of(null);
    }

    let result: ApplicationResolverData = {
      element: element,
      purposeIds: this.nomenclatureService.getPurposes(),
      paymentMethodIds: this.nomenclatureService.getPaymentMethods(),
      srvcResRcptMethIds: this.nomenclatureService.getSrvcResRcptMethods(),
      documents: this.service.getDocuments(applicationId),
      personAlias: this.service.getPersonAlias(applicationId),
      personAliasTypes: this.nomenclatureService.getPersonAliasTypes(),
      eWebRequest: this.service.getApplicationEWebRequestsData(applicationId),
      countries: this.nomenclatureService.getCountries(),
      genderTypes: this.nomenclatureService.getGenderTypes(),
      documentTypes: this.nomenclatureService.getDocumentTypes(),
      applicationStatusHistoryData:
        this.service.getApplicationStatusHistoryData(applicationId),
      users: this.nomenclatureService.getUsers(),
      certificate: this.certificateService.getCertificateByAppId(applicationId),
      applicationCertificateCanceled: this.certificateService.getCanceledCertificateByAppId(applicationId),
       decisionTypes: this.nomenclatureService.getDecisionTypes(), //
      decidingAuthorities:
        this.nomenclatureService.getDecidingAuthoritiesForBulletins(), //
      caseTypes: this.nomenclatureService.getCaseTypes(), //
      idDocumentCategoryTypes : this.nomenclatureService.getIdDocumentCategoryTypes()
    };
    return forkJoin(result);
  }
}

export class ApplicationResolverData extends BaseResolverData<ApplicationModel> {
    public purposeIds: Observable<BaseNomenclatureModel[]>;
    public paymentMethodIds: Observable<BaseNomenclatureModel[]>;
    public srvcResRcptMethIds: Observable<BaseNomenclatureModel[]>;
    public documents: Observable<ApplicationDocumentModel[]>;
    public users: Observable<BaseNomenclatureModel[]>;
    public certificate: Observable<ApplicationCertificateResultModel>;
    public decisionTypes: Observable<BaseNomenclatureModel[]>;
    public decidingAuthorities: Observable<BaseNomenclatureModel[]>;
    public caseTypes: Observable<BaseNomenclatureModel[]>;
    public idDocumentCategoryTypes: Observable<BaseNomenclatureModel[]>;
  
}
