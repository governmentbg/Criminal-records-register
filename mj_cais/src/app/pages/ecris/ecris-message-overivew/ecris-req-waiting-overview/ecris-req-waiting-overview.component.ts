import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { EcrisMessageGridService } from "../_data/ecris-message-grid.service";
import { EcrisMessageGridModel } from "../_models/ecris-message-grid.model";
import { EcrisMessageStatusConstants } from "../_models/ecris-message-status.constants";

@Component({
  selector: "cais-ecris-req-waiting-overview",
  templateUrl: "./ecris-req-waiting-overview.component.html",
  styleUrls: ["./ecris-req-waiting-overview.component.scss"],
})
export class EcrisReqWaitingOverviewComponent extends RemoteGridWithStatePersistance<
  EcrisMessageGridModel,
  EcrisMessageGridService
> {
  constructor(
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService,
    service: EcrisMessageGridService,
    injector: Injector
  ) {
    super("ecris-req-waiting-search", service, injector);
    this.service.updateUrlStatus(
      EcrisMessageStatusConstants.ReqWaitingForCSAuthority
    );
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
