import { Component, Injector } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
import { ConfirmTemplateDialogComponent } from "../../../../@core/components/dialogs/confirm-template-dialog/confirm-template-dialog.component";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { ApplicationGridService } from "../_data/application-grid.service";
import { ApplicationGridModel } from "../_models/application-overview/application-grid.model";
import { ApplicationTypeStatusConstants } from "../_models/application-type-status.constants";

@Component({
  selector: "cais-application-waiting-payment",
  templateUrl: "./application-waiting-payment.component.html",
  styleUrls: ["./application-waiting-payment.component.scss"],
})
export class ApplicationWaitingPaymentComponent extends RemoteGridWithStatePersistance<
  ApplicationGridModel,
  ApplicationGridService
> {
  constructor(
    private dialogService: NbDialogService,
    service: ApplicationGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("application-waiting-payment-search", service, injector);
    this.service.updateUrlStatus(ApplicationTypeStatusConstants.CheckPayment);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  public confirmPayment(id: any): void {
    let rowId = id;
      //TODO: 
      this.dialogService
      .open(ConfirmTemplateDialogComponent, { context: {
        title: 'Потвърждаване на плащенето',
        color: "success",
       
      },closeOnBackdropClick: false })
      .onClose.subscribe((result) => {
        if(result == true){
          rowId;
        }
        
      });
  }
}
