import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinGridModel } from "../../../bulletin/bulletin-overview/_models/bulletin-grid.model";
import { FbbcGridModel } from "../../../fbbc/fbbc-overview/_models/fbbc-grid.model";
import { EcrisMessageModel } from "../_models/ecris-message.model";
import { EcrisMsgNameModel } from "../_models/ecris-msg-name.model";
import { EcrisMsgNationalityModel } from "../_models/ecris-msg-nationality.model";
import { GraoPersonModel } from "../ecris-identification-form/grids/grao-person-overview/_models/grao-person.model";

@Injectable({ providedIn: "root" })
export class EcrisMessageService extends CaisCrudService<
  EcrisMessageModel,
  string
> {
  constructor(injector: Injector) {
    super(EcrisMessageModel, injector, "ecris-messages");
  }

  public getEcrisBulletins(id: string): Observable<BulletinGridModel[]> {
    return this.http.get<BulletinGridModel[]>(`${this.url}/${id}/bulletins`);
  }

  public getEcrisDocument(id: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.url}/${id}/document`);
  }

  public getEcrisFbbcs(id: string): Observable<FbbcGridModel[]> {
    return this.http.get<FbbcGridModel[]>(`${this.url}/${id}/fbbcs`);
  }

  public get(id: string): Observable<EcrisMessageModel> {
    return this.http.get<EcrisMessageModel>(`${this.url}/${id}`);
  }

  public getNationalities(id: string): Observable<EcrisMsgNationalityModel[]> {
    return this.http.get<EcrisMsgNationalityModel[]>(
      `${this.url}/${id}/nationalities`
    );
  }

  public getEcrisMsgNames(id: string): Observable<EcrisMsgNameModel[]> {
    return this.http.get<EcrisMsgNameModel[]>(`${this.url}/${id}/names`);
  }

  public getGraoPeople(id: string): Observable<GraoPersonModel[]> {
    return this.http.get<GraoPersonModel[]>(`${this.url}/${id}/grao-people`);
  }

  public identify(aId: string, egn: string): Observable<any> {
    let url = `${this.url}/${aId}/identify/${egn}`;
    return this.http.put(url, {});
  }

  public cancelIdentification(aId: string, egn: string): Observable<any> {
    let url = `${this.url}/${aId}/cancel-identification/${egn}`;
    return this.http.put(url, {});
  }
}
