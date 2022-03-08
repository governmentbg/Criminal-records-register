import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinDecisionModel } from "../models/bulletin-decision.model";
import { BulletinDocumentModel } from "../models/bulletin-document.model";
import { BulletinOffenceModel } from "../models/bulletin-offence.model";
import { BulletinSanctionModel } from "../models/bulletin-sanction.model";
import { BulletinModel } from "../models/bulletin.model";

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
}
