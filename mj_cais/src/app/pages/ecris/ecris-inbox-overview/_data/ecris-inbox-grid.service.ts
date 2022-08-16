import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisInboxGridModel } from "../_models/ecris-inbox-grid.model";

const currentEndpoint = "ecris-inboxes";

@Injectable({
  providedIn: "root",
})
export class EcrisInboxGridService extends CaisCrudService<
  EcrisInboxGridModel,
  string
> {
  constructor(injector: Injector) {
    super(EcrisInboxGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statusId?: string) {
    if (statusId) {
      this.updateUrl(`${currentEndpoint}?statusId=${statusId}`);
      return;
    }

    this.updateUrl(`${currentEndpoint}`);
  }

  public download(id: string, traits: boolean) {
    let url = `${this.baseUrl}/api/${currentEndpoint}/${id}/xml/${traits}`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }
}
