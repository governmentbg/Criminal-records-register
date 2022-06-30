import { NgModule } from '@angular/core';
import { InquiryRoutingModule } from './inquiry-routing.module';
import { CoreComponentModule } from '../../@core/components/core-component.module';
import { ReportBulletinSearchFormComponent } from './report-bulletin-search-form/report-bulletin-search-form.component';
import { ReportBulletinSearchOverviewComponent } from './report-bulletin-search-form/grids/report-bulletin-search-overview/report-bulletin-search-overview.component';


@NgModule({
  declarations: [
    ReportBulletinSearchFormComponent,
    ReportBulletinSearchOverviewComponent
  ],
  imports: [
    CoreComponentModule,
    InquiryRoutingModule
  ]
})
export class InquiryModule { }
