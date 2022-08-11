import { NgModule } from "@angular/core";
import { InternalRequestRoutingModule } from "./internal-request-routing.module";
import { InternalRequestFormComponent } from "./internal-request-form/internal-request-form.component";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { InternalRequestBoxOverViewComponent } from './internal-request-box-over-view/internal-request-box-over-view.component';
import { InternalRequestDraftOvverviewComponent } from './internal-request-box-over-view/tabs/internal-request-draft-ovverview/internal-request-draft-ovverview.component';
import { InternalRequestInboxOverviewComponent } from './internal-request-box-over-view/tabs/internal-request-inbox-overview/internal-request-inbox-overview.component';
import { InternalRequestOutboxOverviewComponent } from './internal-request-box-over-view/tabs/internal-request-outbox-overview/internal-request-outbox-overview.component';

@NgModule({
  declarations: [
    InternalRequestFormComponent,
    InternalRequestBoxOverViewComponent,
    InternalRequestDraftOvverviewComponent,
    InternalRequestInboxOverviewComponent,
    InternalRequestOutboxOverviewComponent
  ],
  imports: [
    CoreComponentModule,
     InternalRequestRoutingModule],
})
export class InternalRequestModule {}
