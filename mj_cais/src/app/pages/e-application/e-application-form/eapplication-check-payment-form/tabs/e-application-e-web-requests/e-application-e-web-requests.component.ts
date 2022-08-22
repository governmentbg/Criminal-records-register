import { Component, Input, OnInit } from '@angular/core';
import { ColumnPinningPosition, IPinningConfig } from '@infragistics/igniteui-angular';
import * as fileSaver from 'file-saver';
import { DateFormatService } from '../../../../../../@core/services/common/date-format.service';
import { ApplicationEWebRequestsService } from '../../../../../application/application-form/tabs/application-e-web-requests/_data/application-e-web-requests..service';
import { ApplicationEWebRequestsModel } from '../../../../../application/application-form/tabs/application-e-web-requests/_models/application-status-history.model';

@Component({
  selector: 'cais-e-application-e-web-requests',
  templateUrl: './e-application-e-web-requests.component.html',
  styleUrls: ['./e-application-e-web-requests.component.scss']
})
export class EApplicationEWebRequestsComponent implements OnInit {
  public pinningConfig: IPinningConfig = { columns: ColumnPinningPosition.End };
  
  @Input() historyData: ApplicationEWebRequestsModel[];

  constructor(
    public dateFormatService: DateFormatService,
    private applicationEWebRequestsService: ApplicationEWebRequestsService
  ) {}

  ngOnInit(): void {}


  public downloadHtml(objectId: any){
    this.applicationEWebRequestsService
    .downloadHtml(objectId)
    .subscribe((response) => {
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
     // this.toastr.showToast("success", "Успешно запазен документ");
    });
  }
}