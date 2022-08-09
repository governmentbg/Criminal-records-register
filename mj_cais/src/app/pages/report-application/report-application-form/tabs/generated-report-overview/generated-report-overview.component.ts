import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { NgxSpinnerService } from "ngx-spinner";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { ReportApplicationService } from "../../_data/report-application.service";
import { GeneratedReportModel } from "./_models/generated-report-grid.model";
import * as fileSaver from "file-saver";
import { CustomToastrService } from "../../../../../@core/services/common/custom-toastr.service";

@Component({
  selector: "cais-generated-report-overview",
  templateUrl: "./generated-report-overview.component.html",
  styleUrls: ["./generated-report-overview.component.scss"],
})
export class GeneratedReportOverviewComponent implements OnInit {

  public isForPreview: boolean;
  public reports: GeneratedReportModel[];
  
  constructor(
    public dateFormatService: DateFormatService,
    private service: ReportApplicationService,
    private loaderService: NgxSpinnerService,
    private activatedRoute: ActivatedRoute,
    private toastr: CustomToastrService,
    private router: Router
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
  }

  printReport(id: string) {
    this.service.printReport(id).subscribe((response: any) => {
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
    }),
      (error) => this.errorHandler(error, "Грешка при изтегляне на файла:");
  }

  deliver(id: string) {
    this.service.deliver(id).subscribe(
      (res) => {
        this.toastr.showBodyToast("success", "Успешно доставена справка","");
        this.router.navigateByUrl("report-applications/edit/" + id);   
      },
      (error) =>
        this.errorHandler(
          error,
          "Грешка при промяна на статус на доставена справка:"
        )
    );
  }

  private errorHandler(error, msg) {
    var errorText = error.status + " " + error.statusText;
    this.toastr.showBodyToast("danger", msg, errorText);
  }
}
