import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ConfigurationService } from "@tl/tl-common";

@Injectable({ providedIn: "root" })
export class ApplicationEWebRequestsService {
  baseUrl: string;
  url: string;
  endpoint = "e-web-requests";


  constructor(private http: HttpClient, private configurationService: ConfigurationService) {
   this.baseUrl = this.configurationService.getServiceUrl();
   this.url = this.baseUrl + '/api/' + this.endpoint
  }

  public downloadHtml(id: string) {
    let url = `${this.url}/downloadHtml/${id}`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }
}
