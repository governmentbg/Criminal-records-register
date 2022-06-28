import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { forkJoin, Observable, of } from 'rxjs';
import { BaseResolverData } from '../../../../@core/models/common/base-resolver.data';
import { BaseNomenclatureModel } from '../../../../@core/models/nomenclature/base-nomenclature.model';
import { NomenclatureService } from '../../../../@core/services/rest/nomenclature.service';
import { ApplicationReportModel } from '../_models/application-report.model';
import { ApplicationReportService } from './application-report.service';

@Injectable({
  providedIn: 'root'
})
export class ApplicationReportResolver implements Resolve<any> {
  constructor(
    private nomenclatureService: NomenclatureService,
    private service: ApplicationReportService,
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

    let result: ApplicationReportResolverData = {
      element: element,
      purposeIds: this.nomenclatureService.getPurposes(),
      paymentMethodIds: this.nomenclatureService.getPaymentMethods(),
      srvcResRcptMethIds: this.nomenclatureService.getSrvcResRcptMethods(),
      //documents: this.service.getDocuments(applicationId),
      personAlias: this.service.getPersonAlias(applicationId),
      personAliasTypes: this.nomenclatureService.getPersonAliasTypes(),
      countries: this.nomenclatureService.getCountries(),
      genderTypes: this.nomenclatureService.getGenderTypes(),
      // applicationStatusHistoryData:
      //   this.service.getApplicationStatusHistoryData(applicationId),
      users: this.nomenclatureService.getUsers(),
      //report: this.certificateService.getCertificateByAppId(applicationId),
    };
    return forkJoin(result);
  }
}

export class ApplicationReportResolverData extends BaseResolverData<ApplicationReportModel> {
    public purposeIds: Observable<BaseNomenclatureModel[]>;
    public paymentMethodIds: Observable<BaseNomenclatureModel[]>;
    public srvcResRcptMethIds: Observable<BaseNomenclatureModel[]>;
    //public documents: Observable<ApplicationDocumentModel[]>;
    public users: Observable<BaseNomenclatureModel[]>;
    //public certificate: Observable<ApplicationCertificateResultModel>;
}
