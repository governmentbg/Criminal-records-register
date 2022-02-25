import { Component, Injector } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { BulletinGridModel } from "./models/bulletin-grid.model";
import { BulletinGridService } from "./data/bulletin-grid.service";

@Component({
  selector: "cais-bulletin-overview",
  templateUrl: "./bulletin-overview.component.html",
  styleUrls: ["./bulletin-overview.component.scss"],
})
export class BulletinOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinGridService
> {
  constructor(
    private dialogService: NbDialogService,
    service: BulletinGridService,
    injector: Injector
  ) {
    super("bulletins-search", service, injector);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
