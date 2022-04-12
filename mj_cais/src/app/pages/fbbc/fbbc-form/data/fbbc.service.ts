import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { FbbcDocumentModel } from "../models/fbbc-document.model";
import { FbbcModel } from "../models/fbbc.model";

@Injectable({ providedIn: "root" })
export class FbbcService extends CaisCrudService<FbbcModel, string> {
  constructor(injector: Injector) {
    super(FbbcModel, injector, "fbbcs");
  }

  public getDocuments(id: string): Observable<FbbcDocumentModel[]> {
    return this.http.get<FbbcDocumentModel[]>(
      environment.apiUrl + `/fbbcs/${id}/documents`
    );
  }

  public saveDocument(
    fbbcId: string,
    model: FbbcDocumentModel
  ): Observable<any> {
    return this.http.post<FbbcDocumentModel>(
      environment.apiUrl + `/fbbcs/${fbbcId}/documents`,
      model
    );
  }

  public downloadDocument(fbbcId: string, documentId: string) {
    let url =
      environment.apiUrl + `/fbbcs/${fbbcId}/documents-download/` + documentId;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public changeStatus(aId: string, statusId: string): Observable<any> {
    return this.http.put(
      environment.apiUrl + `/fbbcs/${aId}/change-status/${statusId}`,
      {}
    );
  }
}
