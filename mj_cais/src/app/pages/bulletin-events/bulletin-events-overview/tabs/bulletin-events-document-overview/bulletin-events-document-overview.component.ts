import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { BulletinEventsGridModel } from "../../_models/bulletin-events-grid.model";
import { BulletinEventsStatusTypeEnum } from "../../_models/bulletin-events-status-type.enum";
import { BulletinEventsDocumentGridService } from "./_data/bulletin-events-document-grid.service";

@Component({
  selector: "cais-bulletin-events-document-overview",
  templateUrl: "./bulletin-events-document-overview.component.html",
  styleUrls: ["./bulletin-events-document-overview.component.scss"],
})
export class BulletinEventsDocumentOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinEventsGridModel,
  BulletinEventsDocumentGridService
> {
  constructor(
    service: BulletinEventsDocumentGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("bulletins-events-document-search", service, injector);
    this.service.updateEventStatusUrl(BulletinEventsStatusTypeEnum.New);
  }

  public BulletinEventsStatusTypeEnum = BulletinEventsStatusTypeEnum;
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
