import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { BulletinEventsGridModel } from "../../_models/bulletin-events-grid.model";
import { BulletinEventsStatusTypeEnum } from "../../_models/bulletin-events-status-type.enum";
import { BulletinEventsArticleGridService } from "./_data/bulletin-events-article-grid.service";

@Component({
  selector: "cais-bulletin-events-article-overview",
  templateUrl: "./bulletin-events-article-overview.component.html",
  styleUrls: ["./bulletin-events-article-overview.component.scss"],
})
export class BulletinEventsArticleOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinEventsGridModel,
  BulletinEventsArticleGridService
> {
  constructor(
    service: BulletinEventsArticleGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("bulletins-events-article--search", service, injector);
    this.service.updateEventStatusUrl(BulletinEventsStatusTypeEnum.New);
  }

  public hideStatus: boolean = true;

  ngOnInit() {
    super.ngOnInit();
  }

  onShowAllBulletinEventsChange(isChacked: boolean) {
    if (isChacked) {
      this.service.updateEventStatusUrl();
    } else {
      this.service.updateEventStatusUrl(BulletinEventsStatusTypeEnum.New);
    }

    this.hideStatus = !isChacked;
    this.ngOnInit();
  }
}
