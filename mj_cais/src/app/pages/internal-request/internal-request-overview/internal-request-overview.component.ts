import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { InternalRequestGridService } from "./_data/internal-request-grid.service";
import { InternalRequestGridModel } from "./_models/internal-request-grid.model";

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

  ngOnInit() {
    let bulletinId = this.activatedRoute.snapshot.params["ID"];
    if (bulletinId) {
      this.service.updateUrl(`internal-requests?bulletinId=${bulletinId}`);
    }else{
      this.service.updateUrl(`internal-requests`);
    }
    super.ngOnInit();
  }
}
