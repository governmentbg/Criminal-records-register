import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { BulletinAdministrationGridService } from "./_data/bulletin-administration-grid.service";
import { BulletinAdministrationGridModel } from "./_models/bulletin-administration-grid.model";

@Component({
  selector: "cais-bulletin-administration-overview",
  templateUrl: "./bulletin-administration-overview.component.html",
  styleUrls: ["./bulletin-administration-overview.component.scss"],
})
export class BulletinAdministrationOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinAdministrationGridModel,
  BulletinAdministrationGridService
> {
  constructor(
    service: BulletinAdministrationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("bulletins-administration-search", service, injector);
  }

  public hideStatus: boolean = true;

  ngOnInit() {
    super.ngOnInit();
  }
}
