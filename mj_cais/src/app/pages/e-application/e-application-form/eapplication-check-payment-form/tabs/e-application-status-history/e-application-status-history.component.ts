import { AfterViewInit, Component, Input, OnInit, ViewChild } from "@angular/core";
import { IgxGridComponent, SortingDirection } from "@infragistics/igniteui-angular";
import { DateFormatService } from "../../../../../../@core/services/common/date-format.service";
import { EApplicationStatusHistoryModel } from "./_models/e-application-status-history.model";

@Component({
  selector: "cais-e-application-status-history",
  templateUrl: "./e-application-status-history.component.html",
  styleUrls: ["./e-application-status-history.component.scss"],
})
export class EApplicationStatusHistoryComponent implements OnInit, AfterViewInit {
  @Input() historyData: EApplicationStatusHistoryModel[];
  @ViewChild('statusHistoryGrid', { read: IgxGridComponent, static: true })
  public statusHistoryGrid: IgxGridComponent;

  constructor(public dateFormatService: DateFormatService) {}
  
  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.statusHistoryGrid.sort([
      { fieldName: 'createdOn', dir: SortingDirection.Asc, ignoreCase: true },
    ])
  }

}
