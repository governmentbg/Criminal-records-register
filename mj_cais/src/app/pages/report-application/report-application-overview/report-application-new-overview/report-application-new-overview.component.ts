import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { ReportApplicationGridService } from "../_data/report-application-grid.service";
import { ReportApplicationStatusConstants } from "../_models/report-applicarion-status.constants";
import { ReportApplicationGridModel } from "../_models/report-application-grid.model";

@Component({
  selector: "cais-report-application-new-overview",
  templateUrl: "./report-application-new-overview.component.html",
  styleUrls: ["./report-application-new-overview.component.scss"],
})
export class ReportApplicationNewOverviewComponent extends RemoteGridWithStatePersistance<
  ReportApplicationGridModel,
  ReportApplicationGridService
> {
  public hideStatus: boolean = true;

  constructor(
    service: ReportApplicationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
  ) {
    super("report-application-new", service, injector);
    this.service.updateUrlStatus(ReportApplicationStatusConstants.New);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  onShowAllReportApplicationsChange(isChacked: boolean) {
    if (isChacked) {
      //removed filter entirely
      this.service.updateUrlStatus();
    } else {
      this.service.updateUrlStatus(ReportApplicationStatusConstants.New);
    }
    this.hideStatus = !isChacked;
    this.ngOnInit();
  }
}
