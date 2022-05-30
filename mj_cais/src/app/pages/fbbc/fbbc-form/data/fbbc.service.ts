import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisMessageGridModel } from "../../../ecris/ecris-message-overivew/_models/ecris-message-grid.model";
import { FbbcDocumentModel } from "../models/fbbc-document.model";
import { FbbcModel } from "../models/fbbc.model";

@Injectable({ providedIn: "root" })
export class FbbcService extends CaisCrudService<FbbcModel, string> {
  constructor(injector: Injector) {
    super(FbbcModel, injector, "fbbcs");
  }

  public getWithPersonData(personId: string): Observable<FbbcModel> {
    return this.http.get<FbbcModel>(
      `${this.url}/create?personId=${personId}`
    );
  }
  
  public getEcrisMessages(id: string): Observable<EcrisMessageGridModel[]> {
    return this.http.get<EcrisMessageGridModel[]>(
      `${this.url}/${id}/ecris-messages`
    );
  }

  public getDocuments(id: string): Observable<FbbcDocumentModel[]> {
    return this.http.get<FbbcDocumentModel[]>(`${this.url}/${id}/documents`);
  }

  public saveDocument(
    fbbcId: string,
    model: FbbcDocumentModel
  ): Observable<any> {
    return this.http.post<FbbcDocumentModel>(
      `${this.url}/${fbbcId}/documents`,
      model
    );
  }

  public downloadDocument(fbbcId: string, documentId: string) {
    let url = `${this.url}/${fbbcId}/documents-download/` + documentId;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public changeStatus(aId: string, statusId: string): Observable<any> {
    let url = `${this.url}/${aId}/change-status/${statusId}`;
    return this.http.put(url, {});
  }
}
