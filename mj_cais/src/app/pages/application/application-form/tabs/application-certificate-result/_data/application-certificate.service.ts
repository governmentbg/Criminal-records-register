import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
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

  public downloadSertificate(id: string) {
    let url = `${this.url}/${id}/certificate-content`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public getCertificateByAppId(appId: string) {
    return this.http.get<ApplicationCertificateResultModel>(
      `${this.url}/by-application/${appId}`
    );
  }

  public getBulletinsCheck(id: string): Observable<BulletinCheckGridModel[]> {
    return this.http.get<BulletinCheckGridModel[]>(
      `${this.url}/${id}/bulletins-check`
    );
  }

  public sendBulletinsForSelection(
    id:string,
    ids: string[],
  ): Observable<any> {
    return this.http.put<ApplicationCertificateResultModel>(
      `${this.url}/${id}/bulletins-selcetion`,
      ids
    );
  }
}
