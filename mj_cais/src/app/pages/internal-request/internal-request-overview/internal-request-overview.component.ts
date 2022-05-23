import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { InternalRequestGridService } from "./_data/internal-request-grid.service";
import { InternalRequestGridModel } from "./_models/internal-request-grid.model";
import { IRStatusTypeEnum } from "./_models/internal-request-status-type.constants";

@Component({
  selector: "cais-internal-request-overview",
  templateUrl: "./internal-request-overview.component.html",
  styleUrls: ["./internal-request-overview.component.scss"],
})
export class InternalRequestOverviewComponent extends RemoteGridWithStatePersistance<
  InternalRequestGridModel,
  InternalRequestGridService
> {
  constructor(
    service: InternalRequestGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("internal-requests-search", service, injector);
  }

  public showEditButton = false;
  public IRStatusTypeEnum = IRStatusTypeEnum;
  public hideStatus: boolean = true;
  public reqTypeText: string = 'Нови';

  ngOnInit() {
    let bulletinId = this.activatedRoute.snapshot.params["ID"];
    this.showEditButton = bulletinId ? false : true;
    this.service.updateStatusUrl(IRStatusTypeEnum.New, bulletinId);
    super.ngOnInit();
  }

  onShowAllRequests(isChacked: boolean) {
    this.hideStatus = !isChacked;
    this.reqTypeText = isChacked ? 'Всички' : 'Нови';

    let bulletinId = this.activatedRoute.snapshot.params["ID"];
    let status = isChacked ? null : IRStatusTypeEnum.New;

    this.showEditButton = bulletinId ? false : true;
    this.service.updateStatusUrl(status, bulletinId);

    super.ngOnInit();
  }
}
