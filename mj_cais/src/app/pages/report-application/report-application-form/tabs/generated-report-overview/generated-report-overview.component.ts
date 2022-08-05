import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { DateFormatService } from '../../../../../@core/services/common/date-format.service';
import { ReportApplicationService } from '../../_data/report-application.service';
import { GeneratedReportModel } from './_models/generated-report-grid.model';

@Component({
  selector: 'cais-generated-report-overview',
  templateUrl: './generated-report-overview.component.html',
  styleUrls: ['./generated-report-overview.component.scss']
})
export class GeneratedReportOverviewComponent implements OnInit {
  public reports: GeneratedReportModel[];

  constructor(
    public dateFormatService: DateFormatService,
    private service: ReportApplicationService,
    private loaderService: NgxSpinnerService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.params["ID"];
    this.loaderService.show();
    if(this.reports){
      this.loaderService.hide();
      return;
    }
    this.service
      .getReportsData(id)
      .subscribe((res) => {
        this.reports = res;
        this.loaderService.hide();
      });
  }
}