import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import * as fileSaver from "file-saver";
import { NgxSpinnerService } from "ngx-spinner";
import { CustomToastrService } from "../../../../../@core/services/common/custom-toastr.service";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { BulletinService } from "../../_data/bulletin.service";
import { BulletinStatusHistoryModel } from "./_models/bulletin-status-history.model";

@Component({
  selector: "cais-bulletin-status-history-overview",
  templateUrl: "./bulletin-status-history-overview.component.html",
  styleUrls: ["./bulletin-status-history-overview.component.scss"],
})
export class BulletinStatusHistoryOverviewComponent implements OnInit {
  public historyData: BulletinStatusHistoryModel[];

  constructor(
    public dateFormatService: DateFormatService,
    private bulletinService: BulletinService,
    private loaderService: NgxSpinnerService,
    private activatedRoute: ActivatedRoute,
    private toastr: CustomToastrService
  ) {}

  ngOnInit(): void {
    let bulletinId = this.activatedRoute.snapshot.params["ID"];
    this.loaderService.show();
    this.bulletinService
      .getBulletinStatusHistoryData(bulletinId)
      .subscribe((res) => {
        this.historyData = res;
        this.loaderService.hide();
      });
  }

  download(id: string) {
    this.loaderService.show();
    this.bulletinService.downloadHistoryObject(id).subscribe(
      (response: any) => {
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
        this.loaderService.hide();
      },
      (error) => {
        this.loaderService.hide();
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast(
          "danger",
          "Грешка при изтегляне на файла: ",
          errorText
        );
      }
    );
  }
}
