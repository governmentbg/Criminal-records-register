import { NgModule } from "@angular/core";
import { InternalRequestRoutingModule } from "./internal-request-routing.module";
import { InternalRequestOverviewComponent } from "./internal-request-overview/internal-request-overview.component";
import { InternalRequestFormComponent } from "./internal-request-form/internal-request-form.component";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { InternalRequestBoxOverViewComponent } from './internal-request-box-over-view/internal-request-box-over-view.component';
import { InternalRequestDraftOvverviewComponent } from './internal-request-box-over-view/tabs/internal-request-draft-ovverview/internal-request-draft-ovverview.component';

@NgModule({
  declarations: [
    InternalRequestOverviewComponent,
    InternalRequestFormComponent,
    InternalRequestBoxOverViewComponent,
    InternalRequestDraftOvverviewComponent
  ],
  imports: [
    CoreComponentModule,
     InternalRequestRoutingModule],
})
export class InternalRequestModule {}
