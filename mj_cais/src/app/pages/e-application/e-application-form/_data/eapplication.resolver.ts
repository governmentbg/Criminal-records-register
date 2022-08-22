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
import { ApplicationService } from "../../../application/application-form/_data/application.service";
import { ApplicationDocumentModel } from "../../../application/application-form/_models/application-document.model";
import { WCertificateService } from "../eapplication-check-payment-form/tabs/e-application-certificate-result/_data/w-certificate.service";
import { EApplicationModel } from "../_models/eapplication.model";
import { EApplicationService } from "./eapplication.service";

@Injectable({ providedIn: "root" })
export class EApplicationResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: EApplicationService,
    private wCertificateService: WCertificateService,
    private appService: ApplicationService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    let eapplicationId = route.params["ID"];
    let isEdit = route.data["edit"];
    let element = isEdit ? this.service.find(eapplicationId) : of(null);

    let result: EApplicationResolverData = {
      element: element,
      purposeIds: this.nomenclatureService.getPurposes(),
      personAliasTypes: this.nomenclatureService.getPersonAliasTypes(),
      countries: this.nomenclatureService.getCountries(),
      genderTypes: this.nomenclatureService.getGenderTypes(),
      paymentMethodIds: this.nomenclatureService.getPaymentMethods(),
      srvcResRcptMethIds: this.nomenclatureService.getSrvcResRcptMethods(),
      documents: this.service.getDocuments(eapplicationId),
      personAlias: this.service.getPersonAlias(eapplicationId),
      certificate:
        this.wCertificateService.getWCertificateByAppId(eapplicationId),
      applicationStatusHistoryData:
        this.service.getEApplicationStatusHistoryData(eapplicationId),
      eWebRequest:
        this.appService.getApplicationEWebRequestsData(eapplicationId),
    };
    return forkJoin(result);
  }
}

export class EApplicationResolverData extends BaseResolverData<EApplicationModel> {
  public purposeIds: Observable<BaseNomenclatureModel[]>;
  public paymentMethodIds: Observable<BaseNomenclatureModel[]>;
  public srvcResRcptMethIds: Observable<BaseNomenclatureModel[]>;
  public documents: Observable<ApplicationDocumentModel[]>;
}
