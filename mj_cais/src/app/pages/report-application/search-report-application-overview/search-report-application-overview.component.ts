import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { SearchReportApplicationGridService } from "./_data/search-report-application-grid.service";
import { SearchReportApplicationGridModel } from "./_models/search-report-application-grid.model";

@Component({
  selector: "cais-search-report-application-overview",
  templateUrl: "./search-report-application-overview.component.html",
  styleUrls: ["./search-report-application-overview.component.scss"],
})
export class SearchReportApplicationOverviewComponent extends RemoteGridWithStatePersistance<
  SearchReportApplicationGridModel,
  SearchReportApplicationGridService
> {
  constructor(
    service: SearchReportApplicationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("search-inquiry-search", service, injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
