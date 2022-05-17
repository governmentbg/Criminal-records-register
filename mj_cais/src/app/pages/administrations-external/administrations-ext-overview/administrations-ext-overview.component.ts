import { Component, Injector, OnInit } from '@angular/core';
import { RemoteGridWithStatePersistance } from '../../../@core/directives/remote-grid-with-state-persistance.directive';
import { AdministrationsExtGridService } from './_data/administrations-ext-grid.service';
import { AdministrationsExtGridModel } from './_models/administrations-ext-grid.model';

@Component({
  selector: 'cais-administrations-ext-overview',
  templateUrl: './administrations-ext-overview.component.html',
  styleUrls: ['./administrations-ext-overview.component.scss']
})
export class AdministrationsExtOverviewComponent extends RemoteGridWithStatePersistance<
AdministrationsExtGridModel,
AdministrationsExtGridService
> {
  
constructor(
  service: AdministrationsExtGridService,
  injector: Injector
) {
  super("administrations-ext", service, injector);
}

  ngOnInit(): void {
    super.ngOnInit();
  }

}
