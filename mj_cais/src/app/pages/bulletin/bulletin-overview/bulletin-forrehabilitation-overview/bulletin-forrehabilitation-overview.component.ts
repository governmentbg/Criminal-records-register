import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { BulletinGridService } from "../_data/bulletin-grid.service";
import { BulletinGridModel } from "../_models/bulletin-grid.model";
import { BulletinStatusTypeEnum } from "../_models/bulletin-status-type.enum";

@Component({
  selector: "cais-bulletin-forrehabilitation-overview",
  templateUrl: "./bulletin-forrehabilitation-overview.component.html",
  styleUrls: ["./bulletin-forrehabilitation-overview.component.scss"],
})
export class BulletinForRehabilitationOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinGridService
> {
  constructor(service: BulletinGridService, injector: Injector, public dateFormatService: DateFormatService) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(BulletinStatusTypeEnum.ForRehabilitation);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  public onCancelRehabilitation(bulletinId: string) {
    this.service
      .changeStatus(bulletinId, BulletinStatusTypeEnum.Active)
      .subscribe(
        (res) => this.deleteRowHandler(bulletinId),
        (error) => this.errorHandler(error)
      );
  }
}
