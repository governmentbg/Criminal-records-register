import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../@core/services/common/loader.service";
import { InternalRequestStatusType } from "../internal-request-box-over-view/tabs/_models/internal-request-status.type";
import { InternalRequestForJudgeGridService } from "./_data/internal-request-for-judge-grid.service";
import { InternalRequestForJudgeGridModel } from "./_models/internal-request-for-judge-grid.model";

@Component({
  selector: "cais-internal-request-for-judge-overview",
  templateUrl: "./internal-request-for-judge-overview.component.html",
  styleUrls: ["./internal-request-for-judge-overview.component.scss"],
})
export class InternalRequestForJudgeOverviewComponent extends RemoteGridWithStatePersistance<
  InternalRequestForJudgeGridModel,
  InternalRequestForJudgeGridService
> {
  constructor(
    service: InternalRequestForJudgeGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    private loaderService: LoaderService
  ) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(InternalRequestStatusType.Inbox);
  }

  public hideStatus: boolean = true;

  ngOnInit() {
    super.ngOnInit();
  }

  onShowAllChange(isChacked: boolean) {
    if (isChacked) {
      this.service.updateUrlStatus(InternalRequestStatusType.InboxAll);
    } else {
      this.service.updateUrlStatus(InternalRequestStatusType.Inbox);
    }
    this.hideStatus = !isChacked;
    this.ngOnInit();
  }
}
