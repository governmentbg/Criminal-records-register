import { Component, Injector } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { ConfirmDialogComponent } from "../../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonConstants } from "../../../../@core/constants/common.constants";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { BulletinGridService } from "../_data/bulletin-grid.service";
import { BulletinGridModel } from "../_models/bulletin-grid.model";
import { BulletinStatusTypeEnum } from "../_models/bulletin-status-type.enum";

@Component({
  selector: "cais-bulletin-neweiss-overview",
  templateUrl: "./bulletin-neweiss-overview.component.html",
  styleUrls: ["./bulletin-neweiss-overview.component.scss"],
})
export class BulletinNewEissOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinGridService
> {
  constructor(service: BulletinGridService,
     injector: Injector,
     public dateFormatService: DateFormatService,
     private dialogService: NbDialogService,
     private translate: TranslateService) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(BulletinStatusTypeEnum.NewEISS);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  public openUpdateConfirmationDialog(bulletinId: string) {
    let dialogRef = this.dialogService.open(
      ConfirmDialogComponent,
      CommonConstants.defaultDialogConfig
    );

    dialogRef.componentRef.instance.confirmMessage = this.translate.instant(
      "BULLETIN.CONFIRM-MESSAGE-WHEN-UPDATE"
    );
    dialogRef.componentRef.instance.showHeder = false;

    dialogRef.onClose.subscribe((result) => {
      if (result) {
        this.service
          .changeStatus(bulletinId, BulletinStatusTypeEnum.Active)
          .subscribe(
            (res) => {
              this.toastr.showToast(
                "success",
                this.translate.instant("BULLETIN.SUCCESS-UPDATE-STATUS")
              );
              this.router.navigate(["pages/bulletins/active"]);
            },
            (error) => {
              let title = this.dangerMessage;
              let errorText = error.status + " " + error.statusText;
              if (error.error && error.error.customMessage) {
                title = error.error.customMessage;
                errorText = "";
              }

              this.toastr.showBodyToast("danger", title, errorText);
            }
          );
      }
    });
  }
}
