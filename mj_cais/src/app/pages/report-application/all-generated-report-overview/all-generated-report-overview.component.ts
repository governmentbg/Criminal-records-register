import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { AllGeneratedReportGridService } from "./_data/all-generated-report-grid.service";
import { AllGeneratedReportGridModel } from "./_models/all-generated-report-grid.model";

@Component({
  selector: "cais-all-generated-report-overview",
  templateUrl: "./all-generated-report-overview.component.html",
  styleUrls: ["./all-generated-report-overview.component.scss"],
})
export class AllGeneratedReportOverviewComponent extends RemoteGridWithStatePersistance<
  AllGeneratedReportGridModel,
  AllGeneratedReportGridService
> {
  public hideStatus: boolean = true;

  constructor(
    service: AllGeneratedReportGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("all-generated-reports", service, injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
