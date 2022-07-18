import { Component, OnInit } from "@angular/core";

@Component({
  selector: "cais-bulletin-events-overview",
  templateUrl: "./bulletin-events-overview.component.html",
  styleUrls: ["./bulletin-events-overview.component.scss"],
})
export class BulletinEventsOverviewComponent implements OnInit {
  constructor() {}

  public articleTabTitle =
    "Уведомяване за настъпили обстоятелства по чл. 30, чл.22";
  public documentTabTitle =
    "Уведомяване за променен съдебен статус на осъдено лице";

  public showDocumentTab: boolean = false;

  ngOnInit(): void {
  }

  onChangeTab(event) {
    let tabTitle = event.tabTitle;

    if (!this.showDocumentTab) {
      this.showDocumentTab = tabTitle == this.documentTabTitle;
    }
  }
}
