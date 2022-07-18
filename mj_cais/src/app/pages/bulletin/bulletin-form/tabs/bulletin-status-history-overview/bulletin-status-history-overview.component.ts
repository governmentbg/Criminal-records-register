import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NgxSpinnerService } from "ngx-spinner";
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
    private activatedRoute: ActivatedRoute
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
}
