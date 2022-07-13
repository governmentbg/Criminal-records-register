import { EcrisMessageForm } from './../../../../_models/ecris-message.form';
import { EcrisMessageService } from './../../../../_data/ecris-message.service';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { IgxGridComponent } from '@infragistics/igniteui-angular';

@Component({
  selector: 'cais-ecris-msg-names-overview',
  templateUrl: './ecris-msg-names-overview.component.html',
  styleUrls: ['./ecris-msg-names-overview.component.scss']
})
export class EcrisMsgNamesOverviewComponent implements OnInit {

  @Input() ecrisMsgForm: EcrisMessageForm;
  @Input() names: any;
  @Input() dbData: any;

  @ViewChild("namesGrid", {
    read: IgxGridComponent,
  })
  public namesGrid: IgxGridComponent;

  constructor(public ecrisMessageService: EcrisMessageService) { }

  ngOnInit(): void {
  }

}
