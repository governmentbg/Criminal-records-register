import { Component, Injector, OnInit } from '@angular/core';
import { RemoteGridWithStatePersistance } from '../../../../@core/directives/remote-grid-with-state-persistance.directive';
import { DateFormatService } from '../../../../@core/services/common/date-format.service';
import { ApplicationGridService } from '../_data/application-grid.service';
import { ApplicationGridModel } from '../_models/application-overview/application-grid.model';
import { ApplicationTypeStatusConstants } from '../_models/application-type-status.constants';

@Component({
  selector: 'cais-application-bulletins-selection.',
  templateUrl: './application-bulletins-selection.component.html',
  styleUrls: ['./application-bulletins-selection.component.scss']
})
export class ApplicationBulletinsSelectionComponent extends RemoteGridWithStatePersistance<
ApplicationGridModel,
ApplicationGridService
> {
constructor(
  service: ApplicationGridService,
  injector: Injector,
  public dateFormatService: DateFormatService
) {
  super("application-for-check-search", service, injector);
  this.service.updateUrlStatusForCert(ApplicationTypeStatusConstants.BulletinsSelection);
}

ngOnInit(): void {
  super.ngOnInit();
}
}