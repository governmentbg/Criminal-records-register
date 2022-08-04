import { NgModule } from "@angular/core";
import { ReportApplicationRoutingModule } from "./report-application-routing.module";
import { ReportApplicationNewOverviewComponent } from "./report-application-overview/report-application-new-overview/report-application-new-overview.component";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { RegixRequestFormComponent } from "./regix-request-form/regix-request-form.component";
import { ReportApplicationFormComponent } from './report-application-form/report-application-form.component';

@NgModule({
  declarations: [
    ReportApplicationNewOverviewComponent,
    RegixRequestFormComponent,
    ReportApplicationFormComponent,
  ],
  imports: [CoreComponentModule, ReportApplicationRoutingModule],
})
export class ReportApplicationModule {}
