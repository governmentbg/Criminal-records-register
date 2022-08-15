import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { ApplicationSearchGridService } from "../_data/application-search-grid.service";
import { ApplicationSearchGridModel } from "../_models/application-search-overview/application-search-grid.model";

@Component({
  selector: "cais-application-search-overview",
  templateUrl: "./application-search-overview.component.html",
  styleUrls: ["./application-search-overview.component.scss"],
})
export class ApplicationSearchOverviewComponent extends RemoteGridWithStatePersistance<
  ApplicationSearchGridModel,
  ApplicationSearchGridService
> {
  constructor(
    private dialogService: NbDialogService,
    service: ApplicationSearchGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("application-search-search", service, injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
