import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { BulletinGridModel } from "./data/bulletin-grid.model";
import { BulletinService } from "./data/bulletin.service";

@Component({
  selector: "cais-bulletin-overview",
  templateUrl: "./bulletin-overview.component.html",
  styleUrls: ["./bulletin-overview.component.scss"],
})
export class BulletinOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinService
> {
  constructor(
    private dialogService: NbDialogService,
    service: BulletinService,
    injector: Injector
  ) {
    super("bulletins-search", service, injector);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
