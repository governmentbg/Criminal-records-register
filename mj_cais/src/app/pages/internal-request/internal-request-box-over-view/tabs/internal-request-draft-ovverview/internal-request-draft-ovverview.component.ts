import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
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
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(InternalRequestStatusType.Draft, true);
  }

  public hideStatus: boolean = true;

  ngOnInit() {
    super.ngOnInit();
  }
}
