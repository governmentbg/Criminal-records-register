import { HttpParams } from "@angular/common/http";
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

  public updateEventStatusUrl(statusId?: string, bulletinId?: string) {
    let url = `${currentEndpoint}?groupCode=${BulletinEventGroupTypeEnum.Article}`;
    if (statusId) {
      url += `&statusId=${statusId}`;
    }

    if (bulletinId) {
      url += `&bulletinId=${bulletinId}`;
    }

    this.updateUrl(url);
  }

  public changeStatus(aId: string, statusId: string): Observable<any> {
    return this.http.put(
      `${this.baseUrl}/api/bulletin-events/${aId}/change-status/${statusId}`,
      {}
    );
  }
}
