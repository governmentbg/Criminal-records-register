import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { IgxGridComponent } from "@infragistics/igniteui-angular";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { InternalRequestService } from "../../../internal-request-form/_data/internal-request.service";
import { InternalRequestMailBoxGridService } from "../_data/internal-request-mail-box-grid.service";
import { InternalRequestMailBoxGridModel } from "../_models/internal-request-mail-box-grid.model";
import { InternalRequestStatusType } from "../_models/internal-request-status.type";

@Component({
  selector: "cais-internal-request-outbox-overview",
  templateUrl: "./internal-request-outbox-overview.component.html",
  styleUrls: ["./internal-request-outbox-overview.component.scss"],
})
export class InternalRequestOutboxOverviewComponent extends RemoteGridWithStatePersistance<
  InternalRequestMailBoxGridModel,
  InternalRequestMailBoxGridService
> {
  constructor(
    service: InternalRequestMailBoxGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    private loaderService: LoaderService,
    public internalReqService: InternalRequestService
  ) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(InternalRequestStatusType.Outbox, true);
  }

  @Input() count: string;
  @ViewChild("grid", { read: IgxGridComponent })
  public grid: IgxGridComponent;
  public hideStatus: boolean = true;
  public selectedRows: string[];

  ngOnInit() {
    super.ngOnInit();
    this.selectedRows = [];
  }

  onShowAllChange(isChacked: boolean) {
    if (isChacked) {
      this.selectedRows = [];
      this.service.updateUrlStatus(InternalRequestStatusType.OutboxAll, true);
    } else {
      this.service.updateUrlStatus(InternalRequestStatusType.Outbox, true);
    }
    this.hideStatus = !isChacked;
    this.ngOnInit();
  }

  refresh() {
    this.loaderService.showSpinner(this.service);
    super.ngOnInit();
    this.selectedRows = [];
  }

  markAsRead() {
    debugger;
    let currentSelectedRows = this.grid.selectedRows;
    if (currentSelectedRows.length > 0) {
      this.loaderService.show();
      this.internalReqService.markAsRead(currentSelectedRows).subscribe(
        (res) => {
          this.loaderService.hide();
          this.toastr.showToast("success", "Успешно маркирани, като прочетени");
          this.refresh();
        },
        (error) => {
          this.loaderService.hide();
          var errorText = error.status + " " + error.statusText;
          this.toastr.showBodyToast(
            "danger",
            "Грешка при извършване на операцията:",
            errorText
          );
        }
      );
    }
  }
}
