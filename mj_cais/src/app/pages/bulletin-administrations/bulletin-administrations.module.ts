import { NgModule } from "@angular/core";
import { BulletinAdministrationsRoutingModule } from "./bulletin-administrations-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { BulletinAdministrationFormComponent } from "./bulletin-administration-form/bulletin-administration-form.component";
import { BulletinAdministrationSearchFormComponent } from './bulletin-administration-search-form/bulletin-administration-search-form.component';
import { BulletinAdministrationSearchOverviewComponent } from "./bulletin-administration-search-form/bulletin-administration-search-overview/bulletin-administration-search-overview.component";

@NgModule({
  declarations: [
    BulletinAdministrationFormComponent,
    BulletinAdministrationSearchFormComponent,
    BulletinAdministrationSearchOverviewComponent,
  ],
  imports: [
    CoreComponentModule,
    BulletinAdministrationsRoutingModule,
  ],
})
export class BulletinAdministrationsModule {}
