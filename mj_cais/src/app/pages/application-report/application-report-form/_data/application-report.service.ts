import { Injectable, Injector } from "@angular/core";
import { map, Observable } from "rxjs";
import {
  PersonAliasCodeConstants,
  PersonAliasNameConstants,
} from "../../../../@core/constants/person-alias-type.constants";
import { PersonAliasModel } from "../../../../@core/models/common/person-alias.model";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { ApplicationReportModel } from "../_models/application-report.model";

@Injectable({
  providedIn: "root",
})
export class ApplicationReportService extends CaisCrudService<
  ApplicationReportModel,
  string
> {
  constructor(injector: Injector) {
    super(ApplicationReportModel, injector, "reports");
  }

  public getWithPersonData(
    personId: string
  ): Observable<ApplicationReportModel> {
    return this.http.get<ApplicationReportModel>(
      `${this.url}/create?personId=${personId}`
    );
  }

  // public generateReport(id: string): Observable<any> {
  //   return this.http.get<any>(`${this.url}/${id}/report-content`, {});
  // }

  public downloadSertificate(id: string) {
    let url = `${this.url}/${id}/report-content`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public downloadSertificateContent(id: string) {
    let url = `${this.url}/${id}/report-content-only`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }


  // public getApplicationStatusHistoryData(
  //   id: string
  // ): Observable<ApplicationStatusHistoryModel[]> {
  //   return this.http.get<ApplicationStatusHistoryModel[]>(
  //     `${this.url}/${id}/application-history`
  //   );
  // }

  // public updateFinal(id: string, t: ApplicationModel): Observable<ApplicationModel> {
  //   return this.http.put<ApplicationModel>(this.url + `/final-edit/${id}`, t, {});
  // }

  // public downloadDocument(applicationId: string, documentId: string) {
  //   let url = `${this.url}/${applicationId}/documents-download/` + documentId;
  //   return this.http.get(url, { responseType: "blob", observe: "response" });
  // }

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
