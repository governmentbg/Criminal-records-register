import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { BulletinGridService } from "../_data/bulletin-grid.service";
import { BulletinGridModel } from "../_models/bulletin-grid.model";
import { BulletinStatusTypeEnum } from "../_models/bulletin-status-type.constants";

@Component({
  selector: "cais-bulletin-newoffice-overview",
  templateUrl: "./bulletin-newoffice-overview.component.html",
  styleUrls: ["./bulletin-newoffice-overview.component.scss"],
})
export class BulletinNewOfficeOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinGridService
> {
  constructor(service: BulletinGridService, injector: Injector, public dateFormatService: DateFormatService) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(BulletinStatusTypeEnum.NewOffice);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
