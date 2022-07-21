import { Component, Injector, Input } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
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
    public dateFormatService: DateFormatService,
    public loaderService: LoaderService
  ) {
    super("bulletins-events-document-search", service, injector);
  }

  @Input() bulletinId: string;

  public BulletinEventsStatusTypeEnum = BulletinEventsStatusTypeEnum;
  public hideStatus: boolean = true;

  ngOnInit() {
    this.service.updateEventStatusUrl(BulletinEventsStatusTypeEnum.New, this.bulletinId);
    this.loaderService.showSpinner(this.service);
    super.ngOnInit();
  }

  onShowAllBulletinEventsChange(isChacked: boolean) {
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
