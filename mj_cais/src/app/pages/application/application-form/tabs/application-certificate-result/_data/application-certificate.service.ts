import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { ApplicationCertificateResultModel } from "../_models/application-certificate-result.model";

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
    model.version = 2;
    model.id = '5aacead1-8495-47be-8c24-7e26f2833343';
    return this.http.put<ApplicationCertificateResultModel>(
      `${this.url}/${id}/save-signer-data`,
      model
    );
  }

  public downloadSertificate(id: string) {
    let url = `${this.url}/${id}/certificate-content`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }
}
