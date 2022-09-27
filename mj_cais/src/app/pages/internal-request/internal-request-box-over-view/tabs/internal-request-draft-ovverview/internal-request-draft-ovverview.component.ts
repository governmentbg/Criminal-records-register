import { Component, Injector, Input } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { InternalRequestService } from "../../../internal-request-form/_data/internal-request.service";
import { InternalRequestStatusCodeConstants } from "../../../internal-request-form/_models/internal-request-status-code.constants";
import { InternalRequestMailBoxGridService } from "../_data/internal-request-mail-box-grid.service";
import { InternalRequestMailBoxGridModel } from "../_models/internal-request-mail-box-grid.model";
import { InternalRequestStatusType } from "../_models/internal-request-status.type";
import { NbDialogService } from "@nebular/theme";
import { ConfirmDialogComponent } from "../../../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";

@Component({
  selector: "cais-internal-request-draft-ovverview",
  templateUrl: "./internal-request-draft-ovverview.component.html",
  styleUrls: ["./internal-request-draft-ovverview.component.scss"],
})
export class InternalRequestDraftOvverviewComponent extends RemoteGridWithStatePersistance<
  InternalRequestMailBoxGridModel,
  InternalRequestMailBoxGridService
> {
  @Input() caisTitle: string;

  constructor(
    service: InternalRequestMailBoxGridService,
    public internalRequestFormService: InternalRequestService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    private dialogService: NbDialogService
  ) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(InternalRequestStatusType.Draft, true);
  }

  public hideStatus: boolean = true;
  public isLoading: boolean = false;

  ngOnInit() {
    super.ngOnInit();
  }

  public onSend(id) {
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          color: "success",
          showHeder: false,
          confirmMessage: "Изпращане на заявка"
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {
        if (result) {
          this.isLoading = true;
          this.changeStatus(id, InternalRequestStatusCodeConstants.Sent);
        }
      });
  }

  public onDelete(id) {
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          color: "danger",
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {
        if (result) {
          this.isLoading = true;
          this.internalRequestFormService.delete(id).subscribe({
            next: (response) => {
              this.isLoading = false;
              this.toastr.showToast("success", "Успешно изтрита заявка");
              this.ngOnInit();
            },
            error: (errorResponse) => {
              this.errorHandler(errorResponse);
            },
          });
        }
      });
  }

  private changeStatus(id: string, status: string) {
    this.isLoading = true;
    this.internalRequestFormService.changeStatus(id, status).subscribe({
      next: (response) => {
        this.isLoading = false;
        this.toastr.showToast("success", "Успешно изпратена заявка");
        this.ngOnInit();
      },
      error: (errorResponse) => {
        this.errorHandler(errorResponse);
      },
    });
  }
}
