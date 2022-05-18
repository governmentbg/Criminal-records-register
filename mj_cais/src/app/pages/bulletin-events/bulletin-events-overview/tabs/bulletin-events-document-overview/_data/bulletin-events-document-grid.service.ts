import { Injectable, Injector } from "@angular/core";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { BulletinEventsGridModel } from "../../../_models/bulletin-events-grid.model";
import { BulletinEventGroupTypeEnum } from "../../../_models/bulletin-events-group-type.enum";

const currentEndpoint = "bulletin-events";

@Injectable({
  providedIn: "root",
})
export class BulletinEventsDocumentGridService extends CaisCrudService<
  BulletinEventsGridModel,
  string
> {
  constructor(injector: Injector) {
    super(BulletinEventsGridModel, injector, currentEndpoint);
  }

  public updateEventStatusUrl(statusId?: string) {
    let url = `${currentEndpoint}?groupCode=${BulletinEventGroupTypeEnum.Document}`;
    if (statusId) {
      url += `&statusId=${statusId}`;
    }

    this.updateUrl(url);
  }
}
