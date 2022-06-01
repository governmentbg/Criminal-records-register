import { Component, Input, OnInit } from "@angular/core";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { ApplicationStatusHistoryModel } from "./_models/application-status-history.model";

@Component({
  selector: "cais-application-status-history",
  templateUrl: "./application-status-history.component.html",
  styleUrls: ["./application-status-history.component.scss"],
})
export class ApplicationStatusHistoryComponent implements OnInit {

  @Input() historyData: ApplicationStatusHistoryModel[];
  
  constructor(public dateFormatService: DateFormatService) { }

  ngOnInit(): void {
  }

}
