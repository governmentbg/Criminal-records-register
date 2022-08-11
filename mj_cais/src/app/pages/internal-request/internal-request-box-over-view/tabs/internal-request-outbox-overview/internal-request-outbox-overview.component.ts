import { Component, Injector, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { RemoteGridWithStatePersistance } from '../../../../../@core/directives/remote-grid-with-state-persistance.directive';
import { DateFormatService } from '../../../../../@core/services/common/date-format.service';
import { InternalRequestMailBoxGridService } from '../_data/internal-request-mail-box-grid.service';
import { InternalRequestMailBoxGridModel } from '../_models/internal-request-mail-box-grid.model';
import { InternalRequestStatusType } from '../_models/internal-request-status.type';

@Component({
  selector: 'cais-internal-request-outbox-overview',
  templateUrl: './internal-request-outbox-overview.component.html',
  styleUrls: ['./internal-request-outbox-overview.component.scss']
})
export class InternalRequestOutboxOverviewComponent extends RemoteGridWithStatePersistance<
InternalRequestMailBoxGridModel,
InternalRequestMailBoxGridService
> {
constructor(
  service: InternalRequestMailBoxGridService,
  injector: Injector,
  public dateFormatService: DateFormatService,
  private loaderService: NgxSpinnerService
) {
  super("bulletins-search", service, injector);
  this.service.updateUrlStatus(InternalRequestStatusType.Outbox, true);
}

public hideStatus: boolean = true;

ngOnInit() {
  super.ngOnInit();
}

onShowAllChange(isChacked: boolean) {
  if (isChacked) {
    this.service.updateUrlStatus(InternalRequestStatusType.OutboxAll, true);
  } else {
    this.service.updateUrlStatus(InternalRequestStatusType.Outbox, true);
  }
  this.hideStatus = !isChacked;
  this.ngOnInit();
}
}
