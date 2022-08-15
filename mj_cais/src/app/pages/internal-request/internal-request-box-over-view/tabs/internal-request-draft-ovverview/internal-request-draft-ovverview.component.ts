import { Component, Injector } from "@angular/core";
import { NgxSpinnerService } from "ngx-spinner";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { InternalRequestService } from "../../../internal-request-form/_data/internal-request.service";
import { InternalRequestStatusCodeConstants } from "../../../internal-request-form/_models/internal-request-status-code.constants";
import { InternalRequestMailBoxGridService } from "../_data/internal-request-mail-box-grid.service";
import { InternalRequestMailBoxGridModel } from "../_models/internal-request-mail-box-grid.model";
import { InternalRequestStatusType } from "../_models/internal-request-status.type";

@Component({
  selector: "cais-internal-request-draft-ovverview",
  templateUrl: "./internal-request-draft-ovverview.component.html",
  styleUrls: ["./internal-request-draft-ovverview.component.scss"],
})
export class InternalRequestDraftOvverviewComponent extends RemoteGridWithStatePersistance<
  InternalRequestMailBoxGridModel,
  InternalRequestMailBoxGridService
> {
  constructor(
    service: InternalRequestMailBoxGridService,
    public internalRequestFormService: InternalRequestService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    private loaderService: NgxSpinnerService
  ) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(InternalRequestStatusType.Draft, true);
  }

  public hideStatus: boolean = true;

  ngOnInit() {
    super.ngOnInit();
  }

  public onSend(id) {
    this.loaderService.show();
    this.changeStatus(id, InternalRequestStatusCodeConstants.Sent);
  }

  public onDelete(id) {
    this.loaderService.show();
    this.internalRequestFormService.delete(id).subscribe(
      (res) => {
        this.loaderService.hide();

        this.toastr.showToast("success", "Успешно изтрита заявка");
        this.ngOnInit();
      },
      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast(
          "danger",
          "Грешка при изпращане на заявка:",
          errorText
        );
      }
    );
  }

  private changeStatus(id: string, status: string) {
    this.internalRequestFormService.changeStatus(id, status).subscribe(
      (res) => {
        this.loaderService.hide();

        this.toastr.showToast("success", "Успешно изпратена заявка");
        this.ngOnInit();
      },
      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast(
          "danger",
          "Грешка при изпращане на заявка:",
          errorText
        );
      }
    );
  }
}
