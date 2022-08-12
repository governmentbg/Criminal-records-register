import { Component, OnInit } from "@angular/core";
import { NgxSpinner, NgxSpinnerService } from "ngx-spinner";
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

  public titleDraft = "Съдадени заявки";
  public titleInbox = "Получени и необработени заявки";
  public titleOutbox = "Изпратени и обработени от получателя";
  public title = this.titleDraft;

  constructor(
    public service: InternalRequestService,
    private loaderService: NgxSpinnerService,
    public toastr: CustomToastrService
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
    this.outboxCount -= valueEmitted
  }

  onChangeTab(event) {
    this.showDraftTab = false;
    this.showInboxTab = false;
    this.showOutboxTab = false;

    if (event.tabTitle == this.draftTabTitle) {
      this.showDraftTab = true;
      this.title = this.titleDraft;
    }

    if (event.tabTitle == this.inboxTabTitle) {
      this.showInboxTab = true;
      this.title = this.titleInbox;
    }

    if (event.tabTitle == this.outboxTabTitle) {
      this.showOutboxTab = true;
      this.title = this.titleOutbox;
    }
  }
}
