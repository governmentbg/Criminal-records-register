import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { EApplicationReportGridService } from "../_data/eapplication-report-grid.service";
import { EApplicationReportGridModel } from "../_models/eapplication-report-grid.model";

@Component({
  selector: "cais-eapplication-report-overview",
  templateUrl: "./eapplication-report-overview.component.html",
  styleUrls: ["./eapplication-report-overview.component.scss"],
})
export class EapplicationReportOverviewComponent extends RemoteGridWithStatePersistance<
  EApplicationReportGridModel,
  EApplicationReportGridService
> {
  constructor(
    service: EApplicationReportGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("e-application-report-reports-search", service, injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
