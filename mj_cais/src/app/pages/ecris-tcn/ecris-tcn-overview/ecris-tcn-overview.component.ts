import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { EcrisTcnTypeStatusConstants } from "./models/ecris-tcn-type-status.constants";
import { EcrisTcnGridService } from "./data/ecris-tcn-grid.service";
import { EcrisTcnGridModel } from "./models/ecris-tcn-grid.model";
import { ConfirmDialogComponent } from "../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonConstants } from "../../../@core/constants/common.constants";

@Component({
  selector: "cais-ecris-tcn-overview",
  templateUrl: "./ecris-tcn-overview.component.html",
  styleUrls: ["./ecris-tcn-overview.component.scss"],
})
export class EcrisTcnOverviewComponent extends RemoteGridWithStatePersistance<
  EcrisTcnGridModel,
  EcrisTcnGridService
> {
  public hideStatus: boolean = true;

  constructor(
    private dialogService: NbDialogService,
    service: EcrisTcnGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("ecris-tcn-search", service, injector);
    this.service.updateUrlStatus(EcrisTcnTypeStatusConstants.New);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  onShowAllEcrisTcnsChange(isChacked: boolean) {
    if (isChacked) {
      this.service.updateUrlStatus();
    } else {
      this.service.updateUrlStatus(EcrisTcnTypeStatusConstants.New);
    }
    this.hideStatus = !isChacked;
    this.ngOnInit();
  }

  changeToApproved = (id: string) => {
    debugger;
    this.dialogService
      .open(ConfirmDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe((result) => {
        if (result) {
          this.service
            .changeStatus(id, EcrisTcnTypeStatusConstants.Approved)
            .subscribe(
              (res) => this.deleteRowHandler(id),
              (error) => this.errorHandler(error)
            );
        }
      });
  };

  changeToCancelled = (id: string) => {
    this.dialogService
      .open(ConfirmDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe((result) => {
        if (result) {
          this.service
            .changeStatus(id, EcrisTcnTypeStatusConstants.Cancelled)
            .subscribe(
              (res) => this.deleteRowHandler(id),
              (error) => this.errorHandler(error)
            );
        }
      });
  };
}
