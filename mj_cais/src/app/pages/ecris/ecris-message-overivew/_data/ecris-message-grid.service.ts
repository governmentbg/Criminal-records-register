import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisMessageGridModel } from "../_models/ecris-message-grid.model";

const currentEndpoint = "ecris-messages";

@Injectable({ providedIn: "root" })
export class EcrisMessageGridService extends CaisCrudService<
  EcrisMessageGridModel,
  string
> {
  constructor(injector: Injector) {
    super(EcrisMessageGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statusId: string) {
    this.updateUrl(`${currentEndpoint}?statusId=${statusId}`);
  }
}
