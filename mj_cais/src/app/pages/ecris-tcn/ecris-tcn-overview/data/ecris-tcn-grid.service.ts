import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisTcnGridModel } from "../models/ecris-tcn-grid.model";

const currentEndpoint = "ecris-tcns"

@Injectable({ providedIn: "root" })
export class EcrisTcnGridService extends CaisCrudService<
  EcrisTcnGridModel,
  string
> {
  constructor(injector: Injector) {
    super(EcrisTcnGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statusId?: string) {
    this.updateUrl(`${currentEndpoint}?statusId=${statusId}`);
  }
}
