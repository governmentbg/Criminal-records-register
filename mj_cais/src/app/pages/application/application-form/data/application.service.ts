import { Injectable, Injector } from "@angular/core";
import { map, Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import { PersonAliasCodeConstants, PersonAliasNameConstants } from "../../../../@core/constants/person-alias-type.constants";
import { PersonAliasModel } from "../../../../@core/models/common/person-alias.model";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ApplicationDocumentModel } from "../models/application-document.model";
import { ApplicationModel } from "../models/application.model";

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

  public saveDocument(
    fbbcId: string,
    model: ApplicationDocumentModel
  ): Observable<any> {
    return this.http.post<ApplicationDocumentModel>(
      `${this.url}/${fbbcId}/documents`,
      model
    );
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
