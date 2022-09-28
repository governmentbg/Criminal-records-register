import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { ReportApplicationService } from "../../_data/report-application.service";
import { ReportAppStatusHistoryModel } from "./_models/report-app-status-history.model";

@Component({
  selector: "cais-report-app-history-overview",
  templateUrl: "./report-app-history-overview.component.html",
  styleUrls: ["./report-app-history-overview.component.scss"],
})
export class ReportAppHistoryOverviewComponent implements OnInit {
  public historyData: ReportAppStatusHistoryModel[];
  public isLoading: boolean = false;

  constructor(
    public dateFormatService: DateFormatService,
    private service: ReportApplicationService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.params["ID"];
    this.isLoading = true;
    if (this.historyData) {
      this.isLoading = false;
      return;
    }
    this.service.getStatusHistoryData(id).subscribe((res) => {
      this.historyData = res;
      this.isLoading = false;
    });
  }
}