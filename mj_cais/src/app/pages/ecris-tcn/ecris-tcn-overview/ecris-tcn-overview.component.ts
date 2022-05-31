import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { EcrisTcnTypeStatusConstants } from "./models/ecris-tcn-type-status.constants";
import { EcrisTcnGridService } from "./data/ecris-tcn-grid.service";
import { EcrisTcnGridModel } from "./models/ecris-tcn-grid.model";

@Component({
  selector: "cais-ecris-tcn-overview",
  templateUrl: "./ecris-tcn-overview.component.html",
  styleUrls: ["./ecris-tcn-overview.component.scss"],
})
export class EcrisTcnOverviewComponent extends RemoteGridWithStatePersistance<
  EcrisTcnGridModel,
  EcrisTcnGridService
> {
  public hideStatus: boolean = true;

  constructor(
    private dialogService: NbDialogService,
    service: EcrisTcnGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("ecris-tcn-search", service, injector);
    this.service.updateUrlStatus(EcrisTcnTypeStatusConstants.New);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  onShowAllApplicationsChange(isChacked: boolean) {
    if (isChacked) {
      this.service.updateUrlStatus();
    } else {
      this.service.updateUrlStatus(EcrisTcnTypeStatusConstants.New);
    }
    this.hideStatus = !isChacked;
    this.ngOnInit();
  }
}
