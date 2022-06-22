import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EApplicationGridModel } from "../_models/e-application-grid.model";

const currentEndpoint = "e-applications";

@Injectable({
  providedIn: "root",
})
export class EApplicationGridService extends CaisCrudService<
  EApplicationGridModel,
  string
> {
  constructor(injector: Injector) {
    super(EApplicationGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statusId?: string) {
    if (statusId) {
      this.updateUrl(`${currentEndpoint}?statusId=${statusId}`);
    } else {
      this.updateUrl(`${currentEndpoint}`);
    }
  }
}
