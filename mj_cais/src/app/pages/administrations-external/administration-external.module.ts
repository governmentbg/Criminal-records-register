import { NgModule } from "@angular/core";
import { AdministrationExternalRoutingModule } from "./administration-external-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { AdministrationsExtFormmComponent } from "./administrations-ext-form/administrations-ext-form.component";
import { AdministrationsExtOverviewComponent } from "./administrations-ext-overview/administrations-ext-overview.component";

@NgModule({
  declarations: [
    AdministrationsExtFormmComponent,
    AdministrationsExtOverviewComponent,
  ],
  imports: [CoreComponentModule, AdministrationExternalRoutingModule],
})
export class AdministrationExternalModule {}
