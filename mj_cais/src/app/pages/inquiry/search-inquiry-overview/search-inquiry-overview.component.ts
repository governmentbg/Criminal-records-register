import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { SearchInquiryGridService } from "./_data/search-inquiry-grid.service";
import { SearchInquiryGridModel } from "./_models/search-inquiry-grid.model";

@Component({
  selector: "cais-search-inquiry-overview",
  templateUrl: "./search-inquiry-overview.component.html",
  styleUrls: ["./search-inquiry-overview.component.scss"],
})
export class SearchInquiryOverviewComponent extends RemoteGridWithStatePersistance<
  SearchInquiryGridModel,
  SearchInquiryGridService
> {
  constructor(
    service: SearchInquiryGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("search-inquiry-search", service, injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
