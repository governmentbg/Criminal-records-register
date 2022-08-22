import { EApplicationModel } from "./../_models/eapplication.model";
import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { map, Observable } from "rxjs";
import { PersonAliasModel } from "../../../../@core/models/common/person-alias.model";
import {
  PersonAliasCodeConstants,
  PersonAliasNameConstants,
} from "../../../../@core/constants/person-alias-type.constants";
import { ApplicationDocumentModel } from "../../../application/application-form/_models/application-document.model";
import { EApplicationStatusHistoryModel } from "../eapplication-check-payment-form/tabs/e-application-status-history/_models/e-application-status-history.model";

@Injectable({ providedIn: "root" })
export class EApplicationService extends CaisCrudService<
  EApplicationModel,
  string
> {
  constructor(injector: Injector) {
    super(EApplicationModel, injector, "e-applications");
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

  public getDocuments(id: string): Observable<ApplicationDocumentModel[]> {
    return this.http.get<ApplicationDocumentModel[]>(
      `${this.url}/${id}/documents`
    );
  }

  public getEApplicationStatusHistoryData(
    id: string
  ): Observable<EApplicationStatusHistoryModel[]> {
    return this.http.get<EApplicationStatusHistoryModel[]>(
      `${this.url}/${id}/e-application-history`
    );
  }
}
