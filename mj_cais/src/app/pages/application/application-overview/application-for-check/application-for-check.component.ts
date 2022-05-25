import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { ApplicationGridService } from "../_data/application-grid.service";
import { ApplicationGridModel } from "../_models/application-overview/application-grid.model";
import { ApplicationTypeStatusConstants } from "../_models/application-type-status.constants";

@Component({
  selector: "cais-application-for-check",
  templateUrl: "./application-for-check.component.html",
  styleUrls: ["./application-for-check.component.scss"],
})
export class ApplicationForCheckComponent extends RemoteGridWithStatePersistance<
  ApplicationGridModel,
  ApplicationGridService
> {
  constructor(
    private dialogService: NbDialogService,
    service: ApplicationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("application-for-check-search", service, injector);
    this.service.updateUrlStatus(ApplicationTypeStatusConstants.BulletinsCheck);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
