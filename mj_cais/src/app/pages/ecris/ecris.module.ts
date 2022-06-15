import { NgModule } from "@angular/core";
import { EcrisRoutingModule } from "./ecris-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { EcrisIdentificationFormComponent } from "./ecris-message-form/ecris-identification-form/ecris-identification-form.component";
import { EcrisReqWaitingFormComponent } from "./ecris-message-form/ecris-req-waiting-form/ecris-req-waiting-form.component";
import { EcrisIdentificationOverviewComponent } from "./ecris-message-overivew/ecris-identification-overview/ecris-identification-overview.component";
import { EcrisReqWaitingOverviewComponent } from "./ecris-message-overivew/ecris-req-waiting-overview/ecris-req-waiting-overview.component";

@NgModule({
  declarations: [
    EcrisIdentificationOverviewComponent,
    EcrisReqWaitingFormComponent,
    EcrisReqWaitingOverviewComponent,
    EcrisIdentificationFormComponent,
  ],
  imports: [CoreComponentModule, EcrisRoutingModule],
})
export class EcrisModule {}
