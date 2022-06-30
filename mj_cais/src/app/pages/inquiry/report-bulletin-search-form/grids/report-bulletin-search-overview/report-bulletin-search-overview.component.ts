import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { ReportBulletinGridService } from "./_data/report-bulletin-grid.service";
import { ReportBulletinGridModel } from "./_models/report-bulletin-grid.model";

@Component({
  selector: "cais-report-bulletin-search-overview",
  templateUrl: "./report-bulletin-search-overview.component.html",
  styleUrls: ["./report-bulletin-search-overview.component.scss"],
})
export class ReportBulletinSearchOverviewComponent extends RemoteGridWithStatePersistance<
  ReportBulletinGridModel,
  ReportBulletinGridService
> {
  constructor(
    service: ReportBulletinGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("report-bulletin-search", service, injector);
  }

  ngOnInit() {
 
  }

  public onSearch = (filterQuery: string) => {
    this.service.updateUrl(`inquiry/search-bulletins?${filterQuery}`);
    super.ngOnInit();
  };
}
