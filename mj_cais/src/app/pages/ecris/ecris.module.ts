import { NgModule } from "@angular/core";
import { EcrisRoutingModule } from "./ecris-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { EcrisIdentificationFormComponent } from "./ecris-message-form/ecris-identification-form/ecris-identification-form.component";
import { EcrisReqWaitingFormComponent } from "./ecris-message-form/ecris-req-waiting-form/ecris-req-waiting-form.component";
import { EcrisIdentificationOverviewComponent } from "./ecris-message-overivew/ecris-identification-overview/ecris-identification-overview.component";
import { EcrisReqWaitingOverviewComponent } from "./ecris-message-overivew/ecris-req-waiting-overview/ecris-req-waiting-overview.component";
import { GraoPersonOverviewComponent } from './ecris-message-form/ecris-identification-form/grids/grao-person-overview/grao-person-overview.component';
import { EcrisReqPreviewComponent } from './ecris-message-form/ecris-req-preview/ecris-req-preview.component';
import { EcrisNotPreviewComponent } from './ecris-message-form/ecris-not-preview/ecris-not-preview.component';
import { EcrisNotSanctionComponent } from './ecris-message-form/ecris-not-preview/ecris-not-sanction/ecris-not-sanction.component';
import { EcrisNotDescisionComponent } from './ecris-message-form/ecris-not-preview/ecris-not-descision/ecris-not-descision.component';
import { EcrisResponsePreviewComponent } from './ecris-message-form/ecris-response-preview/ecris-response-preview.component';
import { ResultFromSearchOverviewComponent } from './ecris-message-form/ecris-identification-form/grids/result-from-search-overview/result-from-search-overview.component';
import { EcrisInboxOverviewComponent } from './ecris-inbox-overview/ecris-inbox-overview.component';
import { EcrisInboxFormComponent } from './ecris-inbox-form/ecris-inbox-form.component';
import { EcrisOutboxOverviewComponent } from './ecris-outbox-overview/ecris-outbox-overview.component';
import { EcrisOutboxFormComponent } from './ecris-outbox-form/ecris-outbox-form.component';
import { EcrisNotResponsePreviewComponent } from './ecris-message-form/ecris-not-response-preview/ecris-not-response-preview.component';

@NgModule({
  declarations: [
    EcrisIdentificationOverviewComponent,
    EcrisReqWaitingFormComponent,
    EcrisReqWaitingOverviewComponent,
    EcrisIdentificationFormComponent,
    GraoPersonOverviewComponent,
    EcrisReqPreviewComponent,
    EcrisNotPreviewComponent,
    EcrisNotSanctionComponent,
    EcrisNotDescisionComponent,
    EcrisResponsePreviewComponent,
    ResultFromSearchOverviewComponent,
    EcrisInboxOverviewComponent,
    EcrisInboxFormComponent,
    EcrisOutboxOverviewComponent,
    EcrisOutboxFormComponent,
    EcrisNotResponsePreviewComponent,
  ],
  imports: [CoreComponentModule, EcrisRoutingModule],
})
export class EcrisModule {}
