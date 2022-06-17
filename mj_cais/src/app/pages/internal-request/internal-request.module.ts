import { NgModule } from "@angular/core";
import { InternalRequestRoutingModule } from "./internal-request-routing.module";
import { InternalRequestOverviewComponent } from "./internal-request-overview/internal-request-overview.component";
import { InternalRequestFormComponent } from "./internal-request-form/internal-request-form.component";
import { CoreComponentModule } from "../../@core/components/core-component.module";

@NgModule({
  declarations: [
    InternalRequestOverviewComponent,
    InternalRequestFormComponent
  ],
  imports: [
    CoreComponentModule,
     InternalRequestRoutingModule],
})
export class InternalRequestModule {}
