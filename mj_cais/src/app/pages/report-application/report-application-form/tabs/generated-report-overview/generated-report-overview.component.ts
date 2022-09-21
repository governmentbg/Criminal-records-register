import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { NgxSpinnerService } from "ngx-spinner";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { ReportApplicationService } from "../../_data/report-application.service";
import { GeneratedReportModel } from "./_models/generated-report-grid.model";
import * as fileSaver from "file-saver";
import { CustomToastrService } from "../../../../../@core/services/common/custom-toastr.service";
import { ReportApplicationStatusConstants } from "../../../report-application-overview/_models/report-applicarion-status.constants";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { BaseNomenclatureModel } from "../../../../../@core/models/nomenclature/base-nomenclature.model";
import { IgxDialogComponent } from "@infragistics/igniteui-angular";
import { PersonForm } from "../../../../../@core/components/forms/person-form/_models/person.form";
import { PidTypeEnum } from "../../../../../@core/components/forms/person-form/_models/pid-type-enum";
import { ApplicationCertificateService } from "../../../../application/application-form/tabs/application-certificate-result/_data/application-certificate.service";

@Component({
  selector: "cais-generated-report-overview",
  templateUrl: "./generated-report-overview.component.html",
  styleUrls: ["./generated-report-overview.component.scss"],
})
export class GeneratedReportOverviewComponent implements OnInit {
  public isForPreview: boolean;
  public reports: GeneratedReportModel[];
  public ReportApplicationStatusConstants = ReportApplicationStatusConstants;
  public cancelReportFormGroup: FormGroup;
  public signersformGroup: FormGroup;

  @ViewChild("cancelReportDialog", { read: IgxDialogComponent })
  public cancelReportDialog: IgxDialogComponent;

  @ViewChild("generateReportDialog", { read: IgxDialogComponent })
  public generateReportDialog: IgxDialogComponent;

  @Input() users: BaseNomenclatureModel[];
  @Input() person: PersonForm;
  @ViewChild("reportDialog", { read: IgxDialogComponent })
  public reportDialog: IgxDialogComponent;
  public report: string;

  constructor(
    public dateFormatService: DateFormatService,
    private service: ReportApplicationService,
    private loaderService: NgxSpinnerService,
    private activatedRoute: ActivatedRoute,
    private toastr: CustomToastrService,
    private loader: NgxSpinnerService,
    private formBuilder: FormBuilder,
    private appCertificateService: ApplicationCertificateService
  ) {}

  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.params["ID"];
    this.isForPreview = this.activatedRoute.snapshot.data["preview"];
    this.loaderService.show();
    if (this.reports) {
      this.loaderService.hide();
      return;
    }
    this.service.getReportsData(id).subscribe((res) => {
      this.reports = res;
      this.loaderService.hide();
    });

    this.cancelReportFormGroup = this.formBuilder.group({
      description: [{ value: "", disabled: false }, Validators.required],
      firstSignerId: [{ value: "", disabled: false }],
      secondSignerId: [{ value: "", disabled: false }],
      reportId: [{ value: "", disabled: false }, Validators.required],
    });

    this.signersformGroup = this.formBuilder.group({
      reportId: [{ value: "", disabled: false }, Validators.required],
      firstSignerId: [{ value: "", disabled: false }],
      secondSignerId: [{ value: "", disabled: false }],
    });
  }

  printReport(id: string) {
    this.service.printReport(id).subscribe((response: any) => {
      this.getPdfContent(response);
    }),
      (error) => this.errorHandler(error, "Грешка при изтегляне на файла:");
  }

  onGenerateReportDialogOpen(
    id: string,
    firstSignerId: string,
    secondSignerId: string
  ) {
    this.signersformGroup.controls.reportId.patchValue(id);
    this.signersformGroup.controls.firstSignerId.patchValue(firstSignerId);
    this.signersformGroup.controls.secondSignerId.patchValue(secondSignerId);

    this.generateReportDialog.open();
  }

  generateReport() {
    if (!this.signersformGroup.valid) {
      this.signersformGroup.markAllAsTouched();
      return;
    }

    this.loader.show();
    let reportFormValue = this.signersformGroup.getRawValue();
    this.service.generateReport(reportFormValue).subscribe({
      next: (data) => {
        this.reload();
        this.generateReportDialog.close();
        this.loader.hide();
      },
      error: (errorResponse) => {
        this.loader.hide();
        this.errorHandler(errorResponse, "Грешка при генериране на файла:");
      },
    });
  }

  deliver(id: string) {
    this.service.deliver(id).subscribe((response: any) => {
      this.toastr.showBodyToast("success", "Успешно доставена справка", "");
      this.reload();
    }),
      (error) =>
        this.errorHandler(
          error,
          "Грешка при промяна на статус на доставена справка:"
        );
  }

  onOpenCancelReportDialog(
    id: string,
    firstSignerId: string,
    secondSignerId: string
  ) {
    this.cancelReportDialog.open();
    this.cancelReportFormGroup.controls.reportId.patchValue(id);
    this.cancelReportFormGroup.controls.firstSignerId.patchValue(firstSignerId);
    this.cancelReportFormGroup.controls.secondSignerId.patchValue(
      secondSignerId
    );
  }

  cancelReport() {
    if (!this.cancelReportFormGroup.valid) {
      this.cancelReportFormGroup.markAllAsTouched();
      return;
    }

    this.loader.show();
    this.service
      .cancelReport(this.cancelReportFormGroup.getRawValue())
      .subscribe({
        next: (data) => {
          let message = "Успешно анулирана справка";
          this.toastr.showToast("success", message);
          this.cancelReportDialog.close();
          this.loader.hide();
          this.reload();
        },
        error: (errorResponse) => {
          this.cancelReportDialog.close();
          this.loader.hide();
          this.errorHandler(
            errorResponse,
            "Възникна грешки при анулиране на справка"
          );
        },
      });
  }

  showReport() {
    let pid = "";
    let pidType = "";

    if (this.person.egn.value) {
      pid = this.person.egn.value;
      pidType = PidTypeEnum.Egn;
    } else if (this.person.lnch.value) {
      pid = this.person.lnch.value;
      pidType = PidTypeEnum.Lnch;
    } else if (this.person.ln.value) {
      pid = this.person.ln.value;
      pidType = PidTypeEnum.Ln;
    } else if (this.person.idDocNumber.value) {
      pid = this.person.idDocNumber.value;
      pidType = PidTypeEnum.DocumentId;
    } else if (this.person.afisNumber.value) {
      pid = this.person.afisNumber.value;
      pidType = PidTypeEnum.AfisNumber;
    } else if (this.person.suid.value) {
      pid = this.person.suid.value;
      pidType = "SUID";
    }

    this.appCertificateService.htmlReport(pidType, pid).subscribe((res) => {
      this.report = res;
      this.reportDialog.open();
    });
  }

  onPrint() {
    const popupWin = window.open("", "_blank");
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title></title>
          <style>
          </style>
        </head>
        <body onload="window.print();window.close()">
            <div>${this.report}</div>
        </body>
      </html>`);
    popupWin.document.close();
  }

  private errorHandler(error, msg) {
    var errorText = error.status + " " + error.statusText;
    this.toastr.showBodyToast("danger", msg, errorText);
  }

  private getPdfContent(response) {
    let blob = new Blob([response.body]);
    window.URL.createObjectURL(blob);

    let header = response.headers.get("Content-Disposition");
    let filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;

    let fileName = "download";

    var matches = filenameRegex.exec(header);
    if (matches != null && matches[1]) {
      fileName = matches[1].replace(/['"]/g, "");
    }

    fileSaver.saveAs(blob, fileName);
  }

  private reload() {
    this.reports = null;
    this.ngOnInit();
  }
}
