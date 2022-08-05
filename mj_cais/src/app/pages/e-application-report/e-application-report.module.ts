import { NgModule } from '@angular/core';
import { EApplicationReportRoutingModule } from './e-application-report-routing.module';
import { CoreComponentModule } from '../../@core/components/core-component.module';
import { EapplicationReportOverviewComponent } from './e-application-report-overview/eapplication-report-overview/eapplication-report-overview.component';
import { EapplicationReportSearchPersOverviewComponent } from './e-application-report-overview/eapplication-report-search-pers-overview/eapplication-report-search-pers-overview.component';


@NgModule({
  declarations: [
    EapplicationReportOverviewComponent,
    EapplicationReportSearchPersOverviewComponent
  ],
  imports: [
    CoreComponentModule,
    EApplicationReportRoutingModule
  ]
})
export class EApplicationReportModule { }
