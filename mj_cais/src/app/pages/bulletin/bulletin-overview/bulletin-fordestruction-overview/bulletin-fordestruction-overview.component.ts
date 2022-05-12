import { Component, Injector } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { ConfirmDialogComponent } from "../../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonConstants } from "../../../../@core/constants/common.constants";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { BulletinGridService } from "../_data/bulletin-grid.service";
import { BulletinGridModel } from "../_models/bulletin-grid.model";
import { BulletinStatusTypeEnum } from "../_models/bulletin-status-type.constants";

@Component({
  selector: "cais-bulletin-fordestruction-overview",
  templateUrl: "./bulletin-fordestruction-overview.component.html",
  styleUrls: ["./bulletin-fordestruction-overview.component.scss"],
})
export class BulletinForDestructionOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinGridService
> {
  constructor(
    service: BulletinGridService,
    injector: Injector,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService
  ) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(BulletinStatusTypeEnum.ForDestruction);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  public openDestructionConfirmationDialog(bulletinId: string) {
    this.dialogService
      .open(ConfirmDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe((result) => {
        if (result) {
          this.service
            .changeStatus(bulletinId, BulletinStatusTypeEnum.Deleted)
            .subscribe(
              (res) => this.deleteRowHandler(bulletinId),
              (error) => this.errorHandler(error)
            );
        }
      });
  }
}
