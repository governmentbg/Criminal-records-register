import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { DateFormatService } from '../../../../../@core/services/common/date-format.service';
import { ReportApplicationService } from '../../_data/report-application.service';
import { ReportAppStatusHistoryModel } from './_models/report-app-status-history.model';

@Component({
  selector: 'cais-report-app-history-overview',
  templateUrl: './report-app-history-overview.component.html',
  styleUrls: ['./report-app-history-overview.component.scss']
})
export class ReportAppHistoryOverviewComponent implements OnInit {
  public historyData: ReportAppStatusHistoryModel[];

  constructor(
    public dateFormatService: DateFormatService,
    private service: ReportApplicationService,
    private loaderService: NgxSpinnerService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.params["ID"];
    this.loaderService.show();
    if(this.historyData){
      this.loaderService.hide();
      return;
    }
    this.service
      .getStatusHistoryData(id)
      .subscribe((res) => {
        this.historyData = res;
        this.loaderService.hide();
      });
  }
}