import { Component, Injector, OnInit } from '@angular/core';
import { RemoteGridWithStatePersistance } from '../../../@core/directives/remote-grid-with-state-persistance.directive';
import { UserCitizenGridService } from './_data/user-citizen-grid.service';
import { UserCitizenGridModel } from './_models/user-citizen.grid.model';

@Component({
  selector: 'cais-users-citizen-overview',
  templateUrl: './users-citizen-overview.component.html',
  styleUrls: ['./users-citizen-overview.component.scss']
})
export class UsersCitizenOverviewComponent extends RemoteGridWithStatePersistance<
UserCitizenGridModel,
UserCitizenGridService
> {
  
  constructor(
    service: UserCitizenGridService,
    injector: Injector
    ) {
      super("users-citizen", service, injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

}
