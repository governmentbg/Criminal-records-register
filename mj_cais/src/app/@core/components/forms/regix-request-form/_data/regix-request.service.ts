import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigurationService } from '@tl/tl-common';

@Injectable({
  providedIn: 'root'
})
export class RegixRequestService  {
  baseUrl: string;
  url: string;
  endpoint = "applications";
  
  constructor(private http: HttpClient, private configurationService: ConfigurationService) {
    this.baseUrl = this.configurationService.getServiceUrl();
   this.url = this.baseUrl + '/api/' + this.endpoint
  }

  public searchByIdentifier(id: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.url}/searchByIdentifier/${id}`);
  }

  public searchByIdentifierLNCH(id: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.url}/searchByIdentifierLNCH/${id}`);
  }
}
