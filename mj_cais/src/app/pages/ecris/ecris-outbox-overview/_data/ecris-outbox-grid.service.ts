import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../@core/services/rest/cais-crud.service";
import { EcrisOutboxGridModel } from "../_models/ecris-outbox-grid.model";

const currentEndpoint = "ecris-outboxes";

@Injectable({
  providedIn: "root",
})
export class EcrisOutboxGridService extends CaisCrudService<
  EcrisOutboxGridModel,
  string
> {
  constructor(injector: Injector) {
    super(EcrisOutboxGridModel, injector, currentEndpoint);
  }

  public updateUrlStatus(statusId?: string) {
    if (statusId) {
      this.updateUrl(`${currentEndpoint}?statusId=${statusId}`);
      return;
    }

    this.updateUrl(`${currentEndpoint}`);
  }

  public download(id: string) {
    let url = `${this.baseUrl}/api/${currentEndpoint}/${id}/xml`;
    return this.http.get(url, { responseType: "blob", observe: "response" });
  }

  public resend(ecrisMsgId: string){
    return this.http.put(
      `${this.baseUrl}/api/${currentEndpoint}/resend/${ecrisMsgId}`,
      {}
    );
  }
}
