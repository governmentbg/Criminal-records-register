import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { FbbcGridService } from "../data/fbbc-grid.service";
import { FbbcStatusTypeEnum } from "../data/fbbc-status-type.constants";
import { FbbcGridModel } from "../models/fbbc-grid.model";

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
    injector: Injector
  ) {
    super("fbbcs-search", service, injector);
    this.service.updateUrlStatus(FbbcStatusTypeEnum.Active);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
