import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { NbDialogService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { NgxSpinnerService } from "ngx-spinner";
import { ConfirmDialogComponent } from "../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { CustomToastrService } from "../../../@core/services/common/custom-toastr.service";
import { BulletinService } from "../bulletin-form/_data/bulletin.service";
import { BulletinStatusTypeEnum } from "../bulletin-overview/_models/bulletin-status-type.enum";

@Injectable({
  providedIn: "root",
})
export class BulletinHelperService {
  protected dangerMessage = "Грешка при запазване на данните: ";

  constructor(
    private dialogService: NbDialogService,
    private translate: TranslateService,
    private loaderService: NgxSpinnerService,
    private bulletinService: BulletinService,
    private toastr: CustomToastrService,
    private router: Router
  ) {}

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
        this.loaderService.show();
        this.bulletinService
          .changeStatus(bulletinId, BulletinStatusTypeEnum.Active)
          .subscribe(
            (res) => {
              this.loaderService.hide();

              this.toastr.showToast(
                "success",
                this.translate.instant("BULLETIN.SUCCESS-UPDATE-STATUS")
              );
              this.router.navigate(["pages/bulletins/active"]);
            },
            (error) => {
              this.loaderService.hide();
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
