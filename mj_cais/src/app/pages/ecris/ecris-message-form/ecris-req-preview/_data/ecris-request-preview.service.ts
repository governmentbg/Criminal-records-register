import { HttpClient } from "@angular/common/http";
import { Injectable, Injector } from "@angular/core";
import { ConfigurationService } from "@tl/tl-common";
import { Observable } from "rxjs";

const currentEndpoint = "ecris-messages";

@Injectable({ providedIn: "root" })
export class EcrisRequestPreviewService {
  baseUrl: string;
  url: string;
  endpoint = "ecris-messages";
  constructor(
    private http: HttpClient,
    private configurationService: ConfigurationService
  ) {
    this.baseUrl = this.configurationService.getServiceUrl();
    this.url = this.baseUrl + '/api/' + this.endpoint
  }

  public getEcrisRequest(id: string,type: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.url}/${id}/document/${type}`);
  }
}
