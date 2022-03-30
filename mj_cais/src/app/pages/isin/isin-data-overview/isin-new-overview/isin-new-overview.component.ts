import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { IsinDataGridService } from "../_data/isin-data-grid.service";
import { IsinDataGridModel } from "../_model/isin-data-grid.model";
import { IsinDataStatusTypeEnum } from "../_model/isin-data-status-type.enum";

@Component({
  selector: "cais-isin-new-overview",
  templateUrl: "./isin-new-overview.component.html",
  styleUrls: ["./isin-new-overview.component.scss"],
})
export class IsinNewOverviewComponent extends RemoteGridWithStatePersistance<
  IsinDataGridModel,
  IsinDataGridService
> {
  constructor(
    service: IsinDataGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("isin-data-search", service, injector);
    this.service.updateUrlStatus(IsinDataStatusTypeEnum.New);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
