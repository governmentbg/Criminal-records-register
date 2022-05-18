import { Injectable, Injector } from "@angular/core";
import { Observable } from "rxjs";
import { CaisCrudService } from "../../../../../../@core/services/rest/cais-crud.service";
import { BulletinEventsGridModel } from "../../../_models/bulletin-events-grid.model";
import { BulletinEventGroupTypeEnum } from "../../../_models/bulletin-events-group-type.enum";

const currentEndpoint = "bulletin-events";

@Injectable({
  providedIn: "root",
})
export class BulletinEventsArticleGridService extends CaisCrudService<
  BulletinEventsGridModel,
  string
> {
  constructor(injector: Injector) {
    super(BulletinEventsGridModel, injector, currentEndpoint);
  }

  public updateEventStatusUrl(statusId?: string) {
    let url = `${currentEndpoint}?groupCode=${BulletinEventGroupTypeEnum.Article}`;
    if (statusId) {
      url += `&statusId=${statusId}`;
    }

    this.updateUrl(url);
  }

  public changeStatus(aId: string, statusId: string): Observable<any> {
    debugger;
    return this.http.put(`${this.baseUrl}/api/bulletin-events/${aId}/change-status/${statusId}`, {});
  }
}
