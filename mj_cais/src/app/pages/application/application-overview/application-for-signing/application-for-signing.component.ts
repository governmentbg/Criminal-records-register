import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { ApplicationGridService } from "../_data/application-grid.service";
import { ApplicationGridModel } from "../_models/application-overview/application-grid.model";
import { ApplicationTypeStatusConstants } from "../_models/application-type-status.constants";

@Component({
  selector: "cais-application-for-signing",
  templateUrl: "./application-for-signing.component.html",
  styleUrls: ["./application-for-signing.component.scss"],
})
export class ApplicationForSigningComponent extends RemoteGridWithStatePersistance<
  ApplicationGridModel,
  ApplicationGridService
> {
  constructor(
    private dialogService: NbDialogService,
    service: ApplicationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("application-for-signing", service, injector);
    this.service.updateUrlStatusForCert(ApplicationTypeStatusConstants.ForSigning);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
