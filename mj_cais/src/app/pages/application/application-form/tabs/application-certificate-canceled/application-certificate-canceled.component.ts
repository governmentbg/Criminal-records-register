import { Component, Input, OnInit } from '@angular/core';
import { ColumnPinningPosition, IPinningConfig } from '@infragistics/igniteui-angular';
import { DateFormatService } from '../../../../../@core/services/common/date-format.service';
import { ApplicationCertificateCanceldModel } from './_data/application-certificate-cancled.model';

@Component({
  selector: 'cais-application-certificate-canceled',
  templateUrl: './application-certificate-canceled.component.html',
  styleUrls: ['./application-certificate-canceled.component.scss']
})
export class ApplicationCertificateCanceledComponent implements OnInit {

  @Input() applicationCertificateCanceled: ApplicationCertificateCanceldModel[];
  public pinningConfig: IPinningConfig = { columns: ColumnPinningPosition.End };
  
  constructor(public dateFormatService: DateFormatService) { }

  ngOnInit(): void {
  }

}