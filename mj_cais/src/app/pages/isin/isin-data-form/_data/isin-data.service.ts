import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import { IsinDataModel } from "../_models/isin-data.model";

@Injectable({
  providedIn: "root",
})
export class IsinDataService {
  constructor(private http: HttpClient) {}

  public get(id: string): Observable<IsinDataModel> {
    return this.http.get<IsinDataModel>(
      environment.apiUrl + `/isin-data/${id}`
    );
  }
}