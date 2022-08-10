import { Component, OnInit } from "@angular/core";

@Component({
  selector: "cais-internal-request-box-over-view",
  templateUrl: "./internal-request-box-over-view.component.html",
  styleUrls: ["./internal-request-box-over-view.component.scss"],
})
export class InternalRequestBoxOverViewComponent implements OnInit {
  public draftTabTitle = "Чернова";
  public inboxTabTitle = "Получени";
  public outboxTabTitle = "Изпратени";
  public showInboxTab: boolean = false;
  public showOutboxTab: boolean = false;
  public showDraftTab: boolean = true;

  constructor() {}

  ngOnInit(): void {}

  onChangeTab(event) {
    this.showDraftTab = event.tabTitle == this.draftTabTitle;
    this.showInboxTab = event.tabTitle == this.inboxTabTitle;
    this.showOutboxTab = event.tabTitle == this.outboxTabTitle;
  }
}
