import { NgModule } from "@angular/core";
import { ApplicationReportRoutingModule } from "./application-report-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { ApplicationReportFormComponent } from "./application-report-form/application-report-form.component";

@NgModule({
  declarations: [ApplicationReportFormComponent],
  imports: [CoreComponentModule, ApplicationReportRoutingModule],
})
export class ApplicationReportModule {}
