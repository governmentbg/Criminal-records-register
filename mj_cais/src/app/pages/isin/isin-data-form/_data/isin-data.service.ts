import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import { IsinDataPreviewModel } from "../_models/isin-data-preview.model";
import { IsinDataModel } from "../_models/isin-data.model";

@Injectable({
  providedIn: "root",
})
export class IsinDataService  {
  constructor(private http: HttpClient) {}

  public get(id: string): Observable<IsinDataModel> {
    return this.http.get<IsinDataModel>(
      environment.apiUrl + `/isin-data/${id}`
    );
  }

  public getForPreview(id: string): Observable<IsinDataPreviewModel> {
    return this.http.get<IsinDataPreviewModel>(
      environment.apiUrl + `/isin-data/preview/${id}`
    );
  }
}