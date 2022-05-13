import { Component, Injector, OnInit } from '@angular/core';
import { RemoteGridWithStatePersistance } from '../../../@core/directives/remote-grid-with-state-persistance.directive';
import { UserGridService } from './_data/user-grid.service';
import { UserGridModel } from './_models/user.grid.model';

@Component({
  selector: 'cais-users-overview',
  templateUrl: './users-overview.component.html',
  styleUrls: ['./users-overview.component.scss']
})
export class UsersOverviewComponent extends RemoteGridWithStatePersistance<
UserGridModel,
UserGridService
> {
  
constructor(
  service: UserGridService,
  injector: Injector
) {
  super("users", service, injector);
}

  ngOnInit(): void {
    super.ngOnInit();
  }

}
