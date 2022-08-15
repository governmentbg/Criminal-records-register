import { Injectable, Injector } from "@angular/core";
import { map, Observable, of } from "rxjs";
import {
  PersonAliasCodeConstants,
  PersonAliasNameConstants,
} from "../../../../@core/constants/person-alias-type.constants";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinDecisionModel } from "../tabs/bulletin-decision-form/_models/bulletin-decision.model";
import { BulletinDocumentModel } from "../tabs/bulletin-documents-form/_models/bulletin-document.model";
import { BulletinOffenceModel } from "../tabs/bulletin-offences-form/_models/bulletin-offence.model";
import { BulletinSanctionModel } from "../tabs/bulletin-sanctions-form/_models/bulletin-sanction.model";
import { BulletinModel } from "../_models/bulletin.model";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { BulletinTypeConstants } from "../_models/bulletin-type-constants";
import { BulletinStatusHistoryModel } from "../tabs/bulletin-status-history-overview/_models/bulletin-status-history.model";
import { PersonAliasModel } from "../../../../@core/models/common/person-alias.model";

@Injectable({ providedIn: "root" })
export class BulletinService extends CaisCrudService<BulletinModel, string> {
  constructor(injector: Injector) {
    super(BulletinModel, injector, "bulletins");
  }

  public getWithPersonData(personId: string): Observable<BulletinModel> {
    return this.http.get<BulletinModel>(
      `${this.url}/create?personId=${personId}`
    );
  }

  public getOffences(id: string): Observable<BulletinOffenceModel[]> {
    return this.http.get<BulletinOffenceModel[]>(`${this.url}/${id}/offences`);
  }

  public getSanctions(id: string): Observable<BulletinSanctionModel[]> {
    return this.http.get<BulletinSanctionModel[]>(
      `${this.url}/${id}/sanctions`
    );
  }

  public getDecisions(id: string): Observable<BulletinDecisionModel[]> {
    return this.http.get<BulletinDecisionModel[]>(
      `${this.url}/${id}/decisions`
    );
  }

  public changeStatus(aId: string, statusId: string): Observable<any> {
    return this.http.put(`${this.url}/${aId}/change-status/${statusId}`, {});
  }

  public getDocuments(id: string): Observable<BulletinDocumentModel[]> {
    return this.http.get<BulletinDocumentModel[]>(
      `${this.url}/${id}/documents`
    );
  }

  public saveDocument(
    bulletinId: string,
    model: BulletinDocumentModel
  ): Observable<any> {
    return this.http.post<BulletinDocumentModel>(
      `${this.url}/${bulletinId}/documents`,
      model
    );
  }

  public downloadDocument(bulletinId: string, documentId: string) {
    let url = `${this.url}/${bulletinId}/documents-download/` + documentId;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public downloadHistoryObject(id: string) {
    let url = `${this.url}/${id}/status-history-content/`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public getBulletinTypes(): Observable<BaseNomenclatureModel[]> {
    return of(BulletinTypeConstants.allData);
  }

  public getBulletinStatusHistoryData(
    id: string
  ): Observable<BulletinStatusHistoryModel[]> {
    return this.http.get<BulletinStatusHistoryModel[]>(
      `${this.url}/${id}/status-history`
    );
  }

  public getPersonAlias(id: string): Observable<PersonAliasModel[]> {
    return this.http
      .get<PersonAliasModel[]>(`${this.url}/${id}/person-alias`)
      .pipe(
        map((items: PersonAliasModel[]) => {
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
