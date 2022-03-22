import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { EcrisIdentificationGridService } from "./data/ecris-identification-grid.service";
import { EcrisIdentificationGridModel } from "./models/ecris-identification-grid.model";

@Component({
  selector: "cais-ecris-identification-overview",
  templateUrl: "./ecris-identification-overview.component.html",
  styleUrls: ["./ecris-identification-overview.component.scss"],
})
export class EcrisIdentificationOverviewComponent extends RemoteGridWithStatePersistance<
  EcrisIdentificationGridModel,
  EcrisIdentificationGridService
> {
  constructor(
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService,
    service: EcrisIdentificationGridService,
    injector: Injector
  ) {
    super("ecris-identification-search", service, injector);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
