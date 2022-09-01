import { Component, Injector, OnInit } from "@angular/core";
import { ConnectedPositioningStrategy, HorizontalAlignment, NoOpScrollStrategy, VerticalAlignment } from "@infragistics/igniteui-angular";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { IsinDataGridService } from "../_data/isin-data-grid.service";
import { IsinDataGridModel } from "../_model/isin-data-grid.model";
import { IsinDataStatusTypeEnum } from "../_model/isin-data-status-type.enum";

@Component({
  selector: "cais-isin-identified-overview",
  templateUrl: "./isin-identified-overview.component.html",
  styleUrls: ["./isin-identified-overview.component.scss"],
})
export class IsinIdentifiedOverviewComponent extends RemoteGridWithStatePersistance<
  IsinDataGridModel,
  IsinDataGridService
> {

  public overlaySettings = {
    positionStrategy: new ConnectedPositioningStrategy({
        horizontalDirection: HorizontalAlignment.Left,
        horizontalStartPoint: HorizontalAlignment.Right,
        verticalStartPoint: VerticalAlignment.Bottom
    }),
    scrollStrategy: new NoOpScrollStrategy()
};


  constructor(
    service: IsinDataGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("isin-data-search", service, injector);
    this.service.updateUrlStatus(IsinDataStatusTypeEnum.Identified);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
