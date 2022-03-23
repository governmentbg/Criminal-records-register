import { Injectable, Injector } from "@angular/core";
import { map, Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import {
  PersonAliasCodeConstants,
  PersonAliasNameConstants,
} from "../../../../@core/constants/person-alias-type.constants";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinDecisionModel } from "../tabs/bulletin-decision-form/_models/bulletin-decision.model";
import { BulletinDocumentModel } from "../tabs/bulletin-documents-form/_models/bulletin-document.model";
import { BulletinOffenceModel } from "../tabs/bulletin-offences-form/_models/bulletin-offence.model";
import { BulletinPersonAliasModel } from "../_models/bulletin-person-alias.model";
import { BulletinSanctionModel } from "../tabs/bulletin-sanctions-form/_models/bulletin-sanction.model";
import { BulletinModel } from "../_models/bulletin.model";

@Injectable({ providedIn: "root" })
export class BulletinService extends CaisCrudService<BulletinModel, string> {
  constructor(injector: Injector) {
    super(BulletinModel, injector, "bulletins");
  }

  public getOffences(id: string): Observable<BulletinOffenceModel[]> {
    return this.http.get<BulletinOffenceModel[]>(
      environment.apiUrl + `/bulletins/${id}/offences`
    );
  }

  public getSanctions(id: string): Observable<BulletinSanctionModel[]> {
    return this.http.get<BulletinSanctionModel[]>(
      environment.apiUrl + `/bulletins/${id}/sanctions`
    );
  }

  public getDecisions(id: string): Observable<BulletinDecisionModel[]> {
    return this.http.get<BulletinDecisionModel[]>(
      environment.apiUrl + `/bulletins/${id}/decisions`
    );
  }

  public getDocuments(id: string): Observable<BulletinDocumentModel[]> {
    return this.http.get<BulletinDocumentModel[]>(
      environment.apiUrl + `/bulletins/${id}/documents`
    );
  }

  public saveDocument(
    bulletinId: string,
    model: BulletinDocumentModel
  ): Observable<any> {
    return this.http.post<BulletinDocumentModel>(
      environment.apiUrl + `/bulletins/${bulletinId}/documents`,
      model
    );
  }

  public downloadDocument(bulletinId: string, documentId: string) {
    let url =
      environment.apiUrl +
      `/bulletins/${bulletinId}/documents-download/` +
      documentId;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public getPersonAlias(id: string): Observable<BulletinPersonAliasModel[]> {
    return this.http
      .get<BulletinPersonAliasModel[]>(
        environment.apiUrl + `/bulletins/${id}/person-alias`
      )
      .pipe(
        map((items: BulletinPersonAliasModel[]) => {
          return items.map((item) => {
            switch (item.typeCode) {
              case PersonAliasCodeConstants.Nickname:
                item.typeId = 1;
                item.typeName = PersonAliasNameConstants.Nickname;
                break;
              case PersonAliasCodeConstants.Previous:
                item.typeId = 2;
                item.typeName = PersonAliasNameConstants.Previous;
                break;
              case PersonAliasCodeConstants.Maiden:
                item.typeId = 3;
                item.typeName = PersonAliasNameConstants.Maiden;
                break;
            }

            return item;
          });
        })
      );
  }
}
