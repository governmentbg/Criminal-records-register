import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ConfigurationService } from "@tl/tl-common";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class RegixRequestService {
  baseUrl: string;
  url: string;
  endpoint = "a-report-applications";
  constructor(
    private http: HttpClient,
    private configurationService: ConfigurationService
  ) {
    this.baseUrl = this.configurationService.getServiceUrl();
    this.url = this.baseUrl + "/api/" + this.endpoint;
  }

  public searchByEgn(identifier: string): Observable<any[]> {
    return this.http.post<any[]>(`${this.url}/search-by-egn/`, { identifier });
  }

  public searchByLnch(identifier: string): Observable<any[]> {
    return this.http.post<any[]>(`${this.url}/search-by-lnch/`, { identifier });
  }
}
