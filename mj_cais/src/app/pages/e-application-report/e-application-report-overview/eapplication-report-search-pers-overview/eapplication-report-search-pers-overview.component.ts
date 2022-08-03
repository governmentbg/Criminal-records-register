import { Component, Injector, OnInit } from "@angular/core";
import { DatePrecisionConstants } from "../../../../@core/components/forms/inputs/date-precision/_models/date-precision.constants";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { EApplicationReportSearchPersGridService } from "../_data/eapplication-report-search-pers-grid.service";
import { EApplicationReportSearchPersGridModel } from "../_models/eapplication-report-search-pers-grid.model";

@Component({
  selector: "cais-eapplication-report-for-identificators-overview",
  templateUrl: "./eapplication-report-search-pers-overview.component.html",
  styleUrls: ["./eapplication-report-search-pers-overview.component.scss"],
})
export class EapplicationReportSearchPersOverviewComponent extends RemoteGridWithStatePersistance<
  EApplicationReportSearchPersGridModel,
  EApplicationReportSearchPersGridService
> {
  constructor(
    service: EApplicationReportSearchPersGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("e-application-report-search-pers-search", service, injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  formatPrecision(value: any) {
    if (value == DatePrecisionConstants.fullDate.id) {
      return DatePrecisionConstants.fullDate.name;
    } else if (value == DatePrecisionConstants.yearAndMonth.id) {
      return DatePrecisionConstants.yearAndMonth.name;
    } else if (value == DatePrecisionConstants.year.id) {
      return DatePrecisionConstants.year.name;
    }
  }
}
