import { Component, OnInit, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { IgxDialogComponent } from "@infragistics/igniteui-angular";
import { NbDialogService } from "@nebular/theme";
import { CustomToastrService } from "../../../@core/services/common/custom-toastr.service";
import { EgnUtils } from "../../../@core/utils/egn.utils";
import { LnchUtils } from "../../../@core/utils/lnch.utils";
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
    private toastr: CustomToastrService
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
  private reportId;
  public titelSearchBy;
  public description;
  public searchValue: string = null;
  public errorTitle;
  public errorMsg;

  ngOnInit(): void {}

  public searchByEGN() {
    this.isEgn = true;
    this.titelSearchBy = this.titelSearchByEgn;
    this.dialog.open();
  }

  public searchByLNCH() {
    this.isEgn = false;
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
      : LnchUtils.isValid(this.searchValue);
    if (!isValidEgn) {
      let title = this.isEgn ? "ЕГН" : "ЛНЧ";
      this.toastr.showToast("danger", `Невалидно ${title}!`);
      return;
    }

    let action = this.isEgn
      ? this.service.searchByEgn(this.searchValue)
      : this.service.searchByLnch(this.searchValue);

    action.subscribe(
      (result: any) => {
        this.router.navigate([
          "pages",
          "report-applications",
          "edit",
          result.id,
        ]);
      },
      (error) => {
        var parser = new DOMParser();
        var htmlDoc = parser.parseFromString(error.error, "text/html");
        let errMsgElement = htmlDoc.getElementById("err-message");
        let errMsg = (errMsgElement.firstChild as any).data;
        this.errorTitle = (errMsg as string).split(":")[0];
        this.errorMsg = (errMsg as string).split(":")[1];
        this.dialogError.open();
      }
    );
  }
  
  changeStatusToCanceled() {
    this.cancelAppReportDialog.open();
  }

  onSubmitCancelReportApplication(){
    //todo:
    // if (this.description) {
    //   this.applicationService
    //     .cancelApplication(this.reportId, this.description)
    //     .subscribe((result) => {
    //       this.ref.close();
    //       let message = "Успешно анулирано";
    //       this.toastr.showToast("success", message);
    //     });
    // }
  }

  navigateToReportApplicationCreate() {
    this.router.navigate(["pages", "report-applications", "edit", null]); // todo?? id
    this.dialogError.close();
  }

  dismissCancel(){
    this.cancelAppReportDialog.close();
  }
}
