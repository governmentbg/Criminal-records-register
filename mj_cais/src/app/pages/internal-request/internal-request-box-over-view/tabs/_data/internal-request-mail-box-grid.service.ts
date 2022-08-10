import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../@core/services/rest/cais-crud.service";
import { InternalRequestMailBoxGridModel } from "../_models/internal-request-mail-box-grid.model";

const currentEndpoint = "internal-requests";

@Injectable({
  providedIn: "root",
})
export class InternalRequestMailBoxGridService extends CaisCrudService<
  InternalRequestMailBoxGridModel,
  string
> {
  constructor(injector: Injector) {
    super(InternalRequestMailBoxGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statuses: string, isForSender: boolean) {
    this.updateUrl(
      `${currentEndpoint}?statuses=${statuses}&isForSender=${isForSender}`
    );
  }
}
