import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { ConfirmDialogComponent } from "../../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonConstants } from "../../../../@core/constants/common.constants";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { FbbcGridService } from "../data/fbbc-grid.service";
import { FbbcStatusTypeEnum } from "../data/fbbc-status-type.constants";
import { FbbcGridModel } from "../models/fbbc-grid.model";

@Component({
  selector: "cais-fbbc-fordestruction-overview",
  templateUrl: "./fbbc-fordestruction-overview.component.html",
  styleUrls: ["./fbbc-fordestruction-overview.component.scss"],
})
export class FbbcForDestructionOverviewComponent extends RemoteGridWithStatePersistance<
  FbbcGridModel,
  FbbcGridService
> {
  constructor(service: FbbcGridService,
    injector: Injector,
    private dialogService: NbDialogService) {
      super("fbbcs-search", service, injector);
      this.service.updateUrlStatus(FbbcStatusTypeEnum.ForDestruction);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  public openDestructionConfirmationDialog(fbbcId: string) {
    this.dialogService
      .open(ConfirmDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe((result) => {
        if (result) {
          this.service
            .changeStatus(fbbcId, FbbcStatusTypeEnum.Deleted)
            .subscribe(
              (res) => {
                this.toastr.showToast("success", "Успешно унищожено сведение");

                this.grid.deleteRow(fbbcId);
                this.grid.data = this.grid.data.filter(
                  (d) => d.id != fbbcId
                );
              },
              (error) => {
                var errorText = error.status + " " + error.statusText;
                this.toastr.showBodyToast(
                  "danger",
                  "Възникна грешка по време на унищожаване на сведение",
                  errorText
                );
              }
            );
        }
      });
  }
}