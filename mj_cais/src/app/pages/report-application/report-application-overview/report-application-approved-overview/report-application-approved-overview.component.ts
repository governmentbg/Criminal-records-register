import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { ReportApplicationGridService } from "../_data/report-application-grid.service";
import { ReportApplicationStatusConstants } from "../_models/report-applicarion-status.constants";
import { ReportApplicationGridModel } from "../_models/report-application-grid.model";

@Component({
  selector: "cais-report-application-approved-overview",
  templateUrl: "./report-application-approved-overview.component.html",
  styleUrls: ["./report-application-approved-overview.component.scss"],
})
export class ReportApplicationApprovedOverviewComponent extends RemoteGridWithStatePersistance<
  ReportApplicationGridModel,
  ReportApplicationGridService
> {
  public hideStatus: boolean = true;

  constructor(
    service: ReportApplicationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("report-application-approved", service, injector);
    this.service.updateUrlStatus(ReportApplicationStatusConstants.Approved);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
