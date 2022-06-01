import { Component, Injector, Input } from "@angular/core";
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
    super("bulletins-events-article-search", service, injector);
  }

  @Input() bulletinId: string;
  public BulletinEventsStatusTypeEnum = BulletinEventsStatusTypeEnum;
  public hideStatus: boolean = true;

  ngOnInit() {
    this.service.updateEventStatusUrl(BulletinEventsStatusTypeEnum.New, this.bulletinId);
    super.ngOnInit();
  }

  onShowAllBulletinEventsChange(isChacked: boolean) {
    debugger;
    if (isChacked) {
      this.service.updateEventStatusUrl(null, this.bulletinId);
    } else {
      this.service.updateEventStatusUrl(BulletinEventsStatusTypeEnum.New, this.bulletinId);
    }

    this.hideStatus = !isChacked;
    super.ngOnInit();
  }

 public changeStatus(id: string, status: BulletinEventsStatusTypeEnum) {
    this.service.changeStatus(id, status).subscribe(
      (res) => {
        let message =
          status == BulletinEventsStatusTypeEnum.Approved
            ? "Потвърдено"
            : "Отхвърлено";
        this.toastr.showToast("success", `Успешно ${message} обстоятелство`);
        this.ngOnInit();
      },
      (error) => {
        let title = this.dangerMessage;
        let errorText = error.status + " " + error.statusText;
        if (error.error && error.error.customMessage) {
          title = error.error.customMessage;
          errorText = "";
        }

        this.toastr.showBodyToast("danger", title, errorText);
      }
    );
  }
}
