import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RoleNameEnum } from "../../../../@core/constants/role-name.enum";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { ApplicationGridService } from "../_data/application-grid.service";
import { ApplicationGridModel } from "../_models/application-overview/application-grid.model";
import { ApplicationTypeStatusConstants } from "../_models/application-type-status.constants";

@Component({
  selector: "cais-application-new-overview",
  templateUrl: "./application-new-overview.component.html",
  styleUrls: ["./application-new-overview.component.scss"],
})
export class ApplicationNewOverviewComponent extends RemoteGridWithStatePersistance<
  ApplicationGridModel,
  ApplicationGridService
> {
  public hideStatus: boolean = true;
  public roleNameNormal: string = RoleNameEnum.Normal;

  constructor(
    private dialogService: NbDialogService,
    service: ApplicationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("application-search", service, injector);
    this.service.updateUrlStatus(ApplicationTypeStatusConstants.NewId);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  onShowAllApplicationsChange(isChacked: boolean) {
    if (isChacked) {
      //removed filter entirely
      this.service.updateUrlStatus();
    } else {
      this.service.updateUrlStatus(ApplicationTypeStatusConstants.NewId);
    }
    this.hideStatus = !isChacked;
    this.ngOnInit();
  }
}
