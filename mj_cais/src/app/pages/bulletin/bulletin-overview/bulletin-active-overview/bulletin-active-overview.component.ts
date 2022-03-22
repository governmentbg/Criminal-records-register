import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { BulletinGridService } from "../data/bulletin-grid.service";
import { BulletinGridModel } from "../models/bulletin-grid.model";
import { BulletinStatusTypeEnum } from "../models/bulletin-status-type.constants";

@Component({
  selector: "cais-bulletin-active-overview",
  templateUrl: "./bulletin-active-overview.component.html",
  styleUrls: ["./bulletin-active-overview.component.scss"],
})
export class BulletinActiveOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinGridService
> {
  constructor(service: BulletinGridService, injector: Injector) {
    super("bulletins-search", service, injector);
    this.service.updateUrl(`bulletins?statusId=${BulletinStatusTypeEnum.Active}`);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
