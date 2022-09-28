import { Component, Injector } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { ConfirmDialogComponent } from "../../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { BulletinGridService } from "../_data/bulletin-grid.service";
import { BulletinGridModel } from "../_models/bulletin-grid.model";
import { BulletinStatusTypeEnum } from "../_models/bulletin-status-type.enum";

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
      .open(ConfirmDialogComponent, {
        context: {
          color: "danger",
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {
        if (result) {
          this.service
            .deleteBulletin(bulletinId)
            .subscribe({
              next: (response) => {
                this.deleteRowHandler(bulletinId);
              },
              error: (errorResponse) => {
                this.errorHandler(errorResponse);
              },
            });
        }
      });
  }
}
