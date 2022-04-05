import { Component, Injector } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { EcrisMessageGridService } from "../_data/ecris-message-grid.service";
import { EcrisMessageGridModel } from "../_models/ecris-message-grid.model";
import { EcrisMessageStatusConstants } from "../_models/ecris-message-status.constants";

@Component({
  selector: "cais-ecris-identification-overview",
  templateUrl: "./ecris-identification-overview.component.html",
  styleUrls: ["./ecris-identification-overview.component.scss"],
})
export class EcrisIdentificationOverviewComponent extends RemoteGridWithStatePersistance<
  EcrisMessageGridModel,
  EcrisMessageGridService
> {
  constructor(
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService,
    service: EcrisMessageGridService,
    injector: Injector
  ) {
    super("ecris-identification-search", service, injector);
    this.service.updateUrlStatus(EcrisMessageStatusConstants.ForIdentification);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
