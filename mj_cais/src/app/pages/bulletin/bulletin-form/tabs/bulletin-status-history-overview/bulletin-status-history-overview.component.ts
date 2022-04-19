import { Component, Input, OnInit } from '@angular/core';
import { DateFormatService } from '../../../../../@core/services/common/date-format.service';
import { BulletinStatusHistoryModel } from './_models/bulletin-status-history.model';

@Component({
  selector: 'cais-bulletin-status-history-overview',
  templateUrl: './bulletin-status-history-overview.component.html',
  styleUrls: ['./bulletin-status-history-overview.component.scss']
})
export class BulletinStatusHistoryOverviewComponent implements OnInit {

  @Input() historyData: BulletinStatusHistoryModel[];
  
  constructor(public dateFormatService: DateFormatService) { }

  ngOnInit(): void {
  }

}
