import { Component, OnInit, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { IgxDialogComponent } from "@infragistics/igniteui-angular";
import { NbDialogService } from "@nebular/theme";
import { NgxSpinnerService } from "ngx-spinner";
import { CustomToastrService } from "../../../@core/services/common/custom-toastr.service";
import { EgnUtils } from "../../../@core/utils/egn.utils";
import { LnchUtils } from "../../../@core/utils/lnch.utils";
import { ReportApplicationService } from "../report-application-form/_data/report-application.service";
import { RegixRequestService } from "./_data/regix-request.service";

@Component({
  selector: "cais-regix-request-form",
  templateUrl: "./regix-request-form.component.html",
  styleUrls: ["./regix-request-form.component.scss"],
})
export class RegixRequestFormComponent implements OnInit {
  constructor(
    private dialogService: NbDialogService,
    private service: RegixRequestService,
    private router: Router,
    private toastr: CustomToastrService,
    private reportAppService: ReportApplicationService,
    private loaderService: NgxSpinnerService
  ) {}

  @ViewChild("searchByIdentifierDialog", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  @ViewChild("searchByIdentifierErrorDialog", { read: IgxDialogComponent })
  public dialogError: IgxDialogComponent;

  @ViewChild("cancelAppReportDialog", { read: IgxDialogComponent })
  public cancelAppReportDialog: IgxDialogComponent;

  private titelSearchByEgn = "Търсене по ЕГН";
  private titelSearchByLnch = "Търсене по ЛНЧ";
  private isEgn = true;
  private reportId: string;
  public titelSearchBy;
  public description;
  public searchValue: string = null;
  public errorTitle;
  public errorMsg;

  ngOnInit(): void {}

  public searchByEGN() {
    this.isEgn = true;
    this.titelSearchBy = this.titelSearchByEgn;
    this.searchValue = null;
    this.dialog.open();
  }

  public searchByLNCH() {
    this.isEgn = false;
    this.searchValue = null;
    this.titelSearchBy = this.titelSearchByLnch;
    this.dialog.open();
  }

  public searchForForeigner() {
    this.router.navigate(["pages/report-applications/create"]);
  }

  onCancel() {
    this.dialog.close();
  }

  onSubmit() {
    var isValidEgn = this.isEgn
      ? EgnUtils.isValid(this.searchValue)
      : true//LnchUtils.isValid(this.searchValue);
    if (!isValidEgn) {
      let title = this.isEgn ? "ЕГН" : "ЛНЧ";
      this.toastr.showToast("danger", `Невалидно ${title}!`);
      return;
    }

    this.loaderService.show();
    let action = this.isEgn
      ? this.service.searchByEgn(this.searchValue)
      : this.service.searchByLnch(this.searchValue);

    action.subscribe(
      (result: any) => {
        if (result.id == null || result.id == undefined) {
          this.parseError(result.error);
          return;
        }

        if (result.errorMsg) {
          this.reportId = result.id;
          this.loaderService.hide();
          this.errorTitle = "Възникна грешка";
          this.errorMsg = result.errorMsg;
          this.dialogError.open();
          return;
        }

        this.dialog.close();
        this.loaderService.hide();
        this.router.navigate([
          "pages",
          "report-applications",
          "edit",
          result.id,
        ]);
      },
      (error) => {
        this.parseError(error);
      }
    );
  }

  changeStatusToCanceled() {
    this.cancelAppReportDialog.open();
  }

  onSubmitCancelReportApplication() {
    if (this.description) {
      let descObj = {};
      descObj["description"] = this.description;
      this.reportAppService
        .cancel(this.reportId, descObj)
        .subscribe((result) => {
          this.cancelAppReportDialog.close();
          this.dialogError.close();
          this.dialog.close();
          let message = "Успешно анулирано искане";
          this.toastr.showToast("success", message);
        });
    }
  }

  navigateToReportApplicationCreate() {
    this.router.navigate([
      "pages",
      "report-applications",
      "edit",
      this.reportId,
    ]);
    this.dialogError.close();
  }

  dismissCancel() {
    this.cancelAppReportDialog.close();
  }

  private parseError(error) {
    this.loaderService.hide();
    var parser = new DOMParser();
    var htmlDoc = parser.parseFromString(error.error, "text/html");
    let errMsgElement = htmlDoc.getElementById("err-message");
    let errMsg = (errMsgElement.firstChild as any).data;
    this.errorTitle = (errMsg as string).split(":")[0];
    this.errorMsg = (errMsg as string).split(":")[1];
    this.dialogError.open();
  }
}
