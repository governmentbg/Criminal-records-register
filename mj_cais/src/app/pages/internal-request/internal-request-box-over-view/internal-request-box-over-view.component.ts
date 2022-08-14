import { Component, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NbTabComponent, NbTabsetComponent } from "@nebular/theme";
import { NgxSpinnerService } from "ngx-spinner";
import { CustomToastrService } from "../../../@core/services/common/custom-toastr.service";
import { InternalRequestService } from "../internal-request-form/_data/internal-request.service";

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

  public inboxCount: number = 0;
  public outboxCount: number = 0;

  public titleDraft = "Създадени заявки";
  public titleInbox = "Получени и необработени заявки";
  public titleOutbox = "Изпратени и обработени от получателя";
  public title = this.titleDraft;
  private isInitComponent: boolean = true;

  @ViewChild("nbtabset") tabset: NbTabsetComponent;

  constructor(
    public service: InternalRequestService,
    private loaderService: NgxSpinnerService,
    public toastr: CustomToastrService,
    public router: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loaderService.show();

    this.service.getRequestsCount().subscribe(
      (res) => {
        this.loaderService.hide();
        this.inboxCount = res.inboxCount;
        this.outboxCount = res.outboxCount;
      },
      (error) => {
        this.loaderService.hide();

        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast("danger", "Възникна грешка:", errorText);
      }
    );
  }

  public onOutBoxReadMessageClicked(valueEmitted: number) {
    this.outboxCount -= valueEmitted;
  }
  
  onChangeTab(event) {
    // reset
    this.showDraftTab = false;
    this.showInboxTab = false;
    this.showOutboxTab = false;

    // when back btn is clicked or save and navigate
    let activeTab = this.router.snapshot.queryParams.activeTab;
    if (activeTab && this.isInitComponent) {
      this.isInitComponent = false;
      this.selectTab(activeTab);
      return;
    }

    if (event.tabTitle == this.inboxTabTitle) {
      this.showInboxTab = true;
      this.title = this.titleInbox;
    } else if (event.tabTitle == this.outboxTabTitle) {
      this.showOutboxTab = true;
      this.title = this.titleOutbox;
    } else {
      this.showDraftTab = true;
      this.title = this.titleDraft;
    }
  }

  selectTab(activeTab) {
    if (this.tabset && this.tabset.tabs) {
      let index = 0;
      if (activeTab == "inbox") {
        index = 1;
        this.showInboxTab = true;
        this.title = this.titleInbox;
      } else if (activeTab == "outbox") {
        index = 2;
        this.showOutboxTab = true;
        this.title = this.titleOutbox;
      } else {
        this.showDraftTab = true;
        this.title = this.titleDraft;
      }

      var tabs = (this.tabset.tabs as any)._results;
      var hasElement = tabs && tabs.length > index;
      if (hasElement) {
        var element = tabs[index] as NbTabComponent;
        if (element.activeValue) return;
        this.tabset.selectTab(element);
      }
    }
  }
}
