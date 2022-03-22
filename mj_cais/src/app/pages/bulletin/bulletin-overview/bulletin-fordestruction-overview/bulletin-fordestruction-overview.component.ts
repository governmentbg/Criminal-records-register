import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { ConfirmDialogComponent } from "../../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonConstants } from "../../../../@core/constants/common.constants";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { BulletinGridService } from "../data/bulletin-grid.service";
import { BulletinGridModel } from "../models/bulletin-grid.model";
import { BulletinStatusTypeEnum } from "../models/bulletin-status-type.constants";

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
    private dialogService: NbDialogService
  ) {
    super("bulletins-search", service, injector);
    this.service.updateUrl(
      `bulletins?statusId=${BulletinStatusTypeEnum.ForDestruction}`
    );
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
              (res) => {
                this.toastr.showToast("success", "Успешно унищожен бюлетин");

                this.grid.deleteRow(bulletinId);
                this.grid.data = this.grid.data.filter(
                  (d) => d.id != bulletinId
                );
              },
              (error) => {
                var errorText = error.status + " " + error.statusText;
                this.toastr.showBodyToast(
                  "danger",
                  "Възникна грешка по време на унищожаване на бюлетин",
                  errorText
                );
              }
            );
        }
      });
  }
}
