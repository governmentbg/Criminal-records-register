import { NgModule } from '@angular/core';
import { InquiryRoutingModule } from './inquiry-routing.module';
import { CoreComponentModule } from '../../@core/components/core-component.module';
import { ReportBulletinSearchFormComponent } from './report-bulletin-search-form/report-bulletin-search-form.component';


@NgModule({
  declarations: [
    ReportBulletinSearchFormComponent
  ],
  imports: [
    CoreComponentModule,
    InquiryRoutingModule
  ]
})
export class InquiryModule { }
