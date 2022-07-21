import { NgModule } from "@angular/core";
import { EcrisRoutingModule } from "./ecris-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { EcrisIdentificationFormComponent } from "./ecris-message-form/ecris-identification-form/ecris-identification-form.component";
import { EcrisReqWaitingFormComponent } from "./ecris-message-form/ecris-req-waiting-form/ecris-req-waiting-form.component";
import { EcrisIdentificationOverviewComponent } from "./ecris-message-overivew/ecris-identification-overview/ecris-identification-overview.component";
import { EcrisReqWaitingOverviewComponent } from "./ecris-message-overivew/ecris-req-waiting-overview/ecris-req-waiting-overview.component";
import { EcrisMsgNamesOverviewComponent } from './ecris-message-form/ecris-identification-form/grids/ecris-msg-names-overview/ecris-msg-names-overview.component';
import { GraoPersonOverviewComponent } from './ecris-message-form/ecris-identification-form/grids/grao-person-overview/grao-person-overview.component';
import { EcrisReqPreviewComponent } from './ecris-message-form/ecris-req-preview/ecris-req-preview.component';

@NgModule({
  declarations: [
    EcrisIdentificationOverviewComponent,
    EcrisReqWaitingFormComponent,
    EcrisReqWaitingOverviewComponent,
    EcrisIdentificationFormComponent,
    EcrisMsgNamesOverviewComponent,
    GraoPersonOverviewComponent,
    EcrisReqPreviewComponent,
  ],
  imports: [CoreComponentModule, EcrisRoutingModule],
})
export class EcrisModule {}
