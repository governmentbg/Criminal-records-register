import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { BulletinGridService } from "../data/bulletin-grid.service";
import { BulletinGridModel } from "../models/bulletin-grid.model";
import { BulletinStatusTypeConstants } from "../models/bulletin-status-type.constants";

@Component({
  selector: "cais-bulletin-neweiss-overview",
  templateUrl: "./bulletin-neweiss-overview.component.html",
  styleUrls: ["./bulletin-neweiss-overview.component.scss"],
})
export class BulletinNewEissOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinGridService
> {
  constructor(service: BulletinGridService, injector: Injector) {
    super("bulletins-search", service, injector);
    this.service.updateUrl(
      `bulletins?statusId=${BulletinStatusTypeConstants.NewEISS}`
    );
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
