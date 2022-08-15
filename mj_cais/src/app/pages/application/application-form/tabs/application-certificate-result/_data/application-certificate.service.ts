import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { ApplicationCertificateCanceldModel } from "../../application-certificate-canceled/_data/application-certificate-cancled.model";
import { ApplicationCertificateDocumentModel } from "../_models/application-certificate-document.model";
import { ApplicationCertificateResultModel } from "../_models/application-certificate-result.model";
import { BulletinCheckGridModel } from "../_models/bulletin-check-grid.model";

@Injectable({
  providedIn: "root",
})
export class ApplicationCertificateService extends CaisCrudService<
  ApplicationCertificateResultModel,
  string
> {
  constructor(injector: Injector) {
    super(ApplicationCertificateResultModel, injector, "certificates");
  }

  public saveSignerData(
    id: string,
    model: ApplicationCertificateResultModel
  ): Observable<any> {
    return this.http.put<ApplicationCertificateResultModel>(
      `${this.url}/${id}/save-signer-data`,
      model
    );
  }
  public saveSignerDataByJudge(
    id: string,
    model: ApplicationCertificateResultModel
  ): Observable<any> {
    return this.http.put<ApplicationCertificateResultModel>(
      `${this.url}/${id}/save-signer-data-by-judge`,
      model
    );
  }

  public downloadSertificate(id: string) {
    let url = `${this.url}/${id}/certificate-content`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public downloadSertificateContent(id: string, applicationCode: string) {
    let url = `${this.url}/${id}/certificate-content-only/${applicationCode}`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public uploadSignedCertificate(
    certId: string,
    model: ApplicationCertificateDocumentModel
  ): Observable<any> {
    return this.http.post<ApplicationCertificateDocumentModel>(
      `${this.url}/${certId}/uploadSignedCertificate`,
      model
    );
  }

  public updateStatus(certId: string) {
    return this.http.get<any>(`${this.url}/updateCertificateStatus/${certId}`);
  }

  public getCertificateByAppId(
    appId: string
  ): Observable<ApplicationCertificateResultModel> {
    return this.http.get<ApplicationCertificateResultModel>(
      `${this.url}/by-application/${appId}`
    );
  }

  public setStatusToDelivered(appId: string) : Observable<any> {
    return this.http.get<any>(
      `${this.url}/set-status-to-delivered/${appId}`
    );
  }

  public getCanceledCertificateByAppId(appId: string) : Observable<ApplicationCertificateCanceldModel> {
    return this.http.get<ApplicationCertificateCanceldModel>(
      `${this.url}/by-application-canceled/${appId}`
    );
  }

  public getBulletinsCheck(
    id: string,
    onlyApproved: boolean
  ): Observable<BulletinCheckGridModel[]> {
    return this.http.get<BulletinCheckGridModel[]>(
      `${this.url}/${id}/bulletins-check/${onlyApproved}`
    );
  }

  public sendBulletinsForSelection(id: string): Observable<any> {
    return this.http.put<ApplicationCertificateResultModel>(
      `${this.url}/${id}/bulletins-selection`,
      {}
    );
  }

  public sendBulletinsForRehabilitation(
    id: string,
    newRequestId: string,
    ids: string[]
  ): Observable<any> {
    return this.http.put<ApplicationCertificateResultModel>(
      `${this.url}/${id}/bulletins-rehabilitation/${newRequestId}`,
      ids
    );
  }
}
