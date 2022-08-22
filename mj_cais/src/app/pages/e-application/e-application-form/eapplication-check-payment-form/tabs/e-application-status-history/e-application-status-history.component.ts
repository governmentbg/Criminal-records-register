import { Component, Input, OnInit } from "@angular/core";
import { DateFormatService } from "../../../../../../@core/services/common/date-format.service";
import { EApplicationStatusHistoryModel } from "./_models/e-application-status-history.model";

@Component({
  selector: "cais-e-application-status-history",
  templateUrl: "./e-application-status-history.component.html",
  styleUrls: ["./e-application-status-history.component.scss"],
})
export class EApplicationStatusHistoryComponent implements OnInit {
  @Input() historyData: EApplicationStatusHistoryModel[];

  constructor(public dateFormatService: DateFormatService) {}

  ngOnInit(): void {}
}
