import { Injectable, Injector } from "@angular/core";
import { map, Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import { PersonAliasCodeConstants, PersonAliasNameConstants } from "../../../../@core/constants/person-alias-type.constants";
import { PersonAliasModel } from "../../../../@core/models/common/person-alias.model";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ApplicationStatusHistoryModel } from "../tabs/application-status-history/_models/application-status-history.model";
import { ApplicationDocumentModel } from "../_models/application-document.model";
import { ApplicationModel } from "../_models/application.model";

@Injectable({ providedIn: "root" })
export class ApplicationService extends CaisCrudService<
  ApplicationModel,
  string
> {
  constructor(injector: Injector) {
    super(ApplicationModel, injector, "applications");
  }

  public getDocuments(id: string): Observable<ApplicationDocumentModel[]> {
    return this.http.get<ApplicationDocumentModel[]>(
      `${this.url}/${id}/documents`
    );
  }

  public getWithPersonData(personId: string): Observable<ApplicationModel> {
    return this.http.get<ApplicationModel>(
      `${this.url}/create?personId=${personId}`
    );
  }

  public saveDocument(
    appId: string,
    model: ApplicationDocumentModel
  ): Observable<any> {
    return this.http.post<ApplicationDocumentModel>(
      `${this.url}/${appId}/documents`,
      model
    );
  }

  public getApplicationStatusHistoryData(
    id: string
  ): Observable<ApplicationStatusHistoryModel[]> {
    return this.http.get<ApplicationStatusHistoryModel[]>(
      `${this.url}/${id}/application-history`
    );
  }

  public cancelApplication(
    id: string
  ): Observable<any[]> {
    return this.http.get<any>(
      `${this.url}/cancelApplication/${id}`
    );
  }

  public changeStatusToCheckPayment(
    id: string
  ): Observable<any[]> {
    return this.http.get<any>(
      `${this.url}/changeStatusToCheckPayment/${id}`
    );
  }

  public updateFinal(id: string, t: ApplicationModel): Observable<ApplicationModel> {
    return this.http.put<ApplicationModel>(this.url + `/final-edit/${id}`, t, {});
  }

  public downloadDocument(applicationId: string, documentId: string) {
    let url = `${this.url}/${applicationId}/documents-download/` + documentId;
    return this.http.get(url, { responseType: "blob", observe: "response" });
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
