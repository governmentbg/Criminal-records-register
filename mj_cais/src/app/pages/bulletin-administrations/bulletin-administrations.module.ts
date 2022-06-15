import { NgModule } from "@angular/core";
import { BulletinAdministrationsRoutingModule } from "./bulletin-administrations-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { BulletinAdministrationFormComponent } from "./bulletin-administration-form/bulletin-administration-form.component";
import { BulletinAdministrationOverviewComponent } from "./bulletin-administration-overview/bulletin-administration-overview.component";

@NgModule({
  declarations: [
    BulletinAdministrationOverviewComponent,
    BulletinAdministrationFormComponent,
  ],
  imports: [
    CoreComponentModule,
    BulletinAdministrationsRoutingModule,
  ],
})
export class BulletinAdministrationsModule {}
