import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { FbbcGridService } from "../_data/fbbc-grid.service";
import { FbbcStatusTypeEnum } from "../_data/fbbc-status-type.constants";
import { FbbcGridModel } from "../_models/fbbc-grid.model";

@Component({
  selector: "cais-fbbc-overview",
  templateUrl: "./fbbc-active-overview.component.html",
  styleUrls: ["./fbbc-active-overview.component.scss"],
})
export class FbbcActiveOverviewComponent extends RemoteGridWithStatePersistance<
  FbbcGridModel,
  FbbcGridService
> {
  constructor(
    private dialogService: NbDialogService,
    service: FbbcGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("fbbcs-search", service, injector);
    this.service.updateUrlStatus(FbbcStatusTypeEnum.Active);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
