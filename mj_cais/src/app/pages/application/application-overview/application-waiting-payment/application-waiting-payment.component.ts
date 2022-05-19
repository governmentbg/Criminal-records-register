import { Component, Injector, OnInit } from "@angular/core";
import { NbDialogService } from "@nebular/theme";
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
    super("application-search", service, injector);
    this.service.updateUrlStatus(ApplicationTypeStatusConstants.PaymentCheck);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
