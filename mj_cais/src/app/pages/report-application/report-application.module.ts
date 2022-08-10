import { NgModule } from "@angular/core";
import { ReportApplicationRoutingModule } from "./report-application-routing.module";
import { ReportApplicationNewOverviewComponent } from "./report-application-overview/report-application-new-overview/report-application-new-overview.component";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { RegixRequestFormComponent } from "./regix-request-form/regix-request-form.component";
import { ReportApplicationFormComponent } from './report-application-form/report-application-form.component';
import { ReportAppHistoryOverviewComponent } from './report-application-form/tabs/report-app-history-overview/report-app-history-overview.component';
import { ReportApplicationApprovedOverviewComponent } from './report-application-overview/report-application-approved-overview/report-application-approved-overview.component';
import { ReportApplicationDeliveredOverviewComponent } from './report-application-overview/report-application-delivered-overview/report-application-delivered-overview.component';
import { AllGeneratedReportOverviewComponent } from "./all-generated-report-overview/all-generated-report-overview.component";
import { GeneratedReportOverviewComponent } from "./report-application-form/tabs/generated-report-overview/generated-report-overview.component";

@NgModule({
  declarations: [
    ReportApplicationNewOverviewComponent,
    RegixRequestFormComponent,
    ReportApplicationFormComponent,
    ReportAppHistoryOverviewComponent,
    ReportApplicationApprovedOverviewComponent,
    ReportApplicationDeliveredOverviewComponent,
    GeneratedReportOverviewComponent,
    AllGeneratedReportOverviewComponent, 
  ],
  imports: [CoreComponentModule, ReportApplicationRoutingModule],
})
export class ReportApplicationModule {}
