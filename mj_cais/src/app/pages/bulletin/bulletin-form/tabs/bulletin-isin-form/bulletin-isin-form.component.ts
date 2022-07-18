import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { IsinDataGridService } from "../../../../isin/isin-data-overview/_data/isin-data-grid.service";
import { IsinDataGridModel } from "../../../../isin/isin-data-overview/_model/isin-data-grid.model";

@Component({
  selector: "cais-bulletin-isin-form",
  templateUrl: "./bulletin-isin-form.component.html",
  styleUrls: ["./bulletin-isin-form.component.scss"],
})
export class BulletinIsinFormComponent extends RemoteGridWithStatePersistance<
  IsinDataGridModel,
  IsinDataGridService
> {
  constructor(
    service: IsinDataGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    public loaderService: LoaderService
  ) {
    super("isin-data-search", service, injector);
    let bulletinId = this.activatedRoute.snapshot.params["ID"];
    this.service.updateUrlBulletin(bulletinId);
  }

  ngOnInit() {
    this.loaderService.showSpinner(this.service);
    super.ngOnInit();
  }
}
