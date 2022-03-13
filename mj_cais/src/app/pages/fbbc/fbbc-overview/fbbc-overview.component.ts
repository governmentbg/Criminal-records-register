import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { FbbcGridService } from "./data/fbbc-grid.service";
import { FbbcGridModel } from "./models/fbbc-grid.model";

@Component({
  selector: "cais-fbbc-overview",
  templateUrl: "./fbbc-overview.component.html",
  styleUrls: ["./fbbc-overview.component.scss"],
})
export class FbbcOverviewComponent extends RemoteGridWithStatePersistance<
  FbbcGridModel,
  FbbcGridService
> {
  constructor(
    private dialogService: NbDialogService,
    service: FbbcGridService,
    injector: Injector
  ) {
    super("fbbcs-search", service, injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
