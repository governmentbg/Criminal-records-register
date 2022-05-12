import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { BulletinGridService } from "../_data/bulletin-grid.service";
import { BulletinGridModel } from "../_models/bulletin-grid.model";
import { BulletinStatusTypeEnum } from "../_models/bulletin-status-type.constants";

@Component({
  selector: "cais-bulletin-neweiss-overview",
  templateUrl: "./bulletin-neweiss-overview.component.html",
  styleUrls: ["./bulletin-neweiss-overview.component.scss"],
})
export class BulletinNewEissOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinGridService
> {
  constructor(service: BulletinGridService, injector: Injector,public dateFormatService: DateFormatService) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(BulletinStatusTypeEnum.NewEISS);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
