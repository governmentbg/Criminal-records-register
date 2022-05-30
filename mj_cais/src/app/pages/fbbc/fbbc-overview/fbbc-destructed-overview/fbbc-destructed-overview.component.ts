import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { FbbcGridService } from "../_data/fbbc-grid.service";
import { FbbcStatusTypeEnum } from "../_data/fbbc-status-type.constants";
import { FbbcGridModel } from "../_models/fbbc-grid.model";

@Component({
  selector: "cais-fbbc-destructed-overview",
  templateUrl: "./fbbc-destructed-overview.component.html",
  styleUrls: ["./fbbc-destructed-overview.component.scss"],
})
export class FbbcDestructedOverviewComponent extends RemoteGridWithStatePersistance<
  FbbcGridModel,
  FbbcGridService
> {
  constructor(
    service: FbbcGridService,
    injector: Injector,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService
  ) {
    super("fbbcs-search", service, injector);
    this.service.updateUrlStatus(FbbcStatusTypeEnum.Deleted);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
