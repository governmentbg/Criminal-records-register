import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../../../../@core/services/rest/cais-crud.service";
import { ApplicationCertificateResultModel } from "../../../../../../application/application-form/tabs/application-certificate-result/_models/application-certificate-result.model";
import { EApplicationCertificateResultModel } from "./e-application-certificate-result.model";

@Injectable({
  providedIn: "root",
})
export class WCertificateService extends CaisCrudService<
  EApplicationCertificateResultModel,
  string
> {
  constructor(injector: Injector) {
    super(EApplicationCertificateResultModel, injector, "w-certificates");
  }

  // public downloadSertificate(id: string) {
  //   let url = `${this.url}/${id}/certificate-content`;
  //   return this.http.get(url, { responseType: "blob", observe: "response" });
  // }

  public getWCertificateByAppId(
    appId: string
  ): Observable<ApplicationCertificateResultModel> {
    return this.http.get<ApplicationCertificateResultModel>(
      `${this.url}/by-application/${appId}`
    );
  }

  public getWCertificateContentByAppId(
    appId: string
  ): Observable<ApplicationCertificateResultModel> {
    return this.http.get<ApplicationCertificateResultModel>(
      `${this.url}/content-by-application/${appId}`
    );
  }
}
