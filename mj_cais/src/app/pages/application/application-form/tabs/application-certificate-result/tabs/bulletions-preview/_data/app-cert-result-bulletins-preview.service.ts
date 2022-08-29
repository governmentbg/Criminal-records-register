import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ConfigurationService } from "@tl/tl-common";
import { Observable } from "rxjs";
import { ApplicationCertificateResultBulletionPreviewModel } from "../../application-certificate-result-bulletion-preview/_model/application-certificate-result-bulletion-preview.model";

@Injectable({ providedIn: "root" })
export class AppCertResultBulletinsPreviewService {
  baseUrl: string;
  url: string;
  endpoint = "bulletins";


  constructor(private http: HttpClient, private configurationService: ConfigurationService) {
   this.baseUrl = this.configurationService.getServiceUrl();
   this.url = this.baseUrl + '/api/' + this.endpoint
  }

  public getConvitionsOnly(id: string) : Observable<ApplicationCertificateResultBulletionPreviewModel[]> {
    let url = `${this.url}/getConvitionsOnly/${id}`;
    return this.http.get<ApplicationCertificateResultBulletionPreviewModel[]>(url);
  }
}
