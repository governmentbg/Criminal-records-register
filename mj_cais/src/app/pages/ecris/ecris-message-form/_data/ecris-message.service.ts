import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { BulletinGridModel } from "../../../bulletin/bulletin-overview/_models/bulletin-grid.model";
import { FbbcGridModel } from "../../../fbbc/fbbc-overview/_models/fbbc-grid.model";
import { EcrisMessageModel } from "../_models/ecris-message.model";

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

  public getEcrisFbbcs(id: string): Observable<FbbcGridModel[]> {
    return this.http.get<FbbcGridModel[]>(`${this.url}/${id}/fbbcs`);
  }
}
