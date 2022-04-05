import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { IsinDataPreviewModel } from "../_models/isin-data-preview.model";
import { IsinDataModel } from "../_models/isin-data.model";

@Injectable({
  providedIn: "root",
})
export class IsinDataService extends CaisCrudService<
  IsinDataPreviewModel,
  string
> {
  constructor(injector: Injector) {
    super(IsinDataPreviewModel, injector, "isin-data");
  }

  public get(id: string): Observable<IsinDataModel> {
    return this.http.get<IsinDataModel>(`${this.url}/${id}`);
  }

  public getForPreview(id: string): Observable<IsinDataPreviewModel> {
    return this.http.get<IsinDataPreviewModel>(
      `${this.url}/preview/${id}`
    );
  }

  public markAsClosed(id: string) {
    return this.http.post<any>(`${this.url}/${id}/close/`, []);
  }
}