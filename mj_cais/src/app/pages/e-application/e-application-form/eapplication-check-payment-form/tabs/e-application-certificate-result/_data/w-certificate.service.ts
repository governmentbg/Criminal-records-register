import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../../../../@core/services/rest/cais-crud.service";
import { ApplicationCertificateResultModel } from "../../../../../../application/application-form/tabs/application-certificate-result/_models/application-certificate-result.model";
import { EApplicationStatusHistoryModel } from "../../e-application-status-history/_models/e-application-status-history.model";
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

  public getWCertificateContentByAppId(appId: string) {
    let url = `${this.url}/content-by-application/${appId}`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public getEApplicationStatusHistoryData(
    id: string
  ): Observable<EApplicationStatusHistoryModel[]> {
    return this.http.get<EApplicationStatusHistoryModel[]>(
      `${this.url}/${id}/e-application-history`
    );
  }
}
