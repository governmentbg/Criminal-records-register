import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { ConfirmTemplateDialogComponent } from "../../../../@core/components/dialogs/confirm-template-dialog/confirm-template-dialog.component";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { EApplicationGridService } from "../_data/eapplication-grid.service";
import { EApplicationGridModel } from "../_models/e-application-grid.model";
import { EApplicationTypeStatusEnum } from "../_models/e-application-type-status.enum";

@Component({
  selector: "cais-eapplication-check-payment-overview",
  templateUrl: "./eapplication-check-payment-overview.component.html",
  styleUrls: ["./eapplication-check-payment-overview.component.scss"],
})
export class EApplicationCheckPaymentOverviewComponent extends RemoteGridWithStatePersistance<
  EApplicationGridModel,
  EApplicationGridService
> {
  constructor(
    private dialogService: NbDialogService,
    service: EApplicationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("e-application-check-payment-search", service, injector);
    this.service.updateUrlStatus(EApplicationTypeStatusEnum.CheckPayment);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  public confirmPayment(id: any): void {
    let rowId = id;
    debugger;
    //TODO:
    this.dialogService
      .open(ConfirmTemplateDialogComponent, {
        context: {
          title: "Потвърждаване на плащенето",
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {
        if (result == true) {
          debugger;
          rowId;
        }
      });
  }
}
