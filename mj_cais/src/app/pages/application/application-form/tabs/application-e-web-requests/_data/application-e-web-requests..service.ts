import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: "root" })
export class ApplicationEWebRequestsService {
  url: any = "http://localhost:7275/api/e-web-requests";


  constructor(private http: HttpClient) {}

  public downloadHtml(id: string) {
    let url = `${this.url}/downloadHtml/${id}`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }
}
