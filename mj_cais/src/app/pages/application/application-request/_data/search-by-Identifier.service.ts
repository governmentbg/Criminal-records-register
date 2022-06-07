import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class SearchByIdentifierService {
  constructor(private http: HttpClient) {}

  public searchByIdentifier(id: string): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:7275/api/applications/searchByIdentifier/${id}`);
  }

  public searchByIdentifierLNCH(id: string): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:7275/api/applications/searchByIdentifierLNCH/${id}`);
  }
}
