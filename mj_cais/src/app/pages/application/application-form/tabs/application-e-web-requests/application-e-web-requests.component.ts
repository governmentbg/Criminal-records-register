import { Component, Input, OnInit } from '@angular/core';
import { DateFormatService } from '../../../../../@core/services/common/date-format.service';
import { ApplicationEWebRequestsModel } from './_models/application-status-history.model';

@Component({
  selector: 'cais-application-e-web-requests',
  templateUrl: './application-e-web-requests.component.html',
  styleUrls: ['./application-e-web-requests.component.scss']
})
export class ApplicationEWebRequestsComponent implements OnInit {

  @Input() historyData: ApplicationEWebRequestsModel[];
  
  constructor(public dateFormatService: DateFormatService) { }

  ngOnInit(): void {
  }

}