import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { InternalRequestMailBoxGridService } from "../_data/internal-request-mail-box-grid.service";
import { InternalRequestMailBoxGridModel } from "../_models/internal-request-mail-box-grid.model";
import { InternalRequestStatusType } from "../_models/internal-request-status.type";

@Component({
  selector: "cais-internal-request-inbox-overview",
  templateUrl: "./internal-request-inbox-overview.component.html",
  styleUrls: ["./internal-request-inbox-overview.component.scss"],
})
export class InternalRequestInboxOverviewComponent extends RemoteGridWithStatePersistance<
  InternalRequestMailBoxGridModel,
  InternalRequestMailBoxGridService
> {
  constructor(
    service: InternalRequestMailBoxGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    private loaderService: LoaderService
  ) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(InternalRequestStatusType.Inbox, false);
  }

  public hideStatus: boolean = true;

  ngOnInit() {
    super.ngOnInit();
  }

  onShowAllChange(isChacked: boolean) {
    if (isChacked) {
      this.service.updateUrlStatus(InternalRequestStatusType.InboxAll, false);
    } else {
      this.service.updateUrlStatus(InternalRequestStatusType.Inbox, false);
    }
    this.hideStatus = !isChacked;
    this.ngOnInit();
  }

  // refresh() {
  //   this.loaderService.showSpinner(this.service);
  //   super.ngOnInit();
  // }
}
