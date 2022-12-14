import { NgModule } from "@angular/core";
import { InquiryRoutingModule } from "./inquiry-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { ReportBulletinSearchFormComponent } from "./report-bulletin-search-form/report-bulletin-search-form.component";
import { ReportBulletinSearchOverviewComponent } from "./report-bulletin-search-form/grids/report-bulletin-search-overview/report-bulletin-search-overview.component";
import { ReportPersonSearchFormComponent } from "./report-person-search-form/report-person-search-form.component";
import { ReportPersonSearchOverviewComponent } from "./report-person-search-form/grids/report-person-search-overview/report-person-search-overview.component";

@NgModule({
  declarations: [
    ReportBulletinSearchFormComponent,
    ReportBulletinSearchOverviewComponent,
    ReportPersonSearchFormComponent,
    ReportPersonSearchOverviewComponent,
  ],
  imports: [CoreComponentModule, InquiryRoutingModule],
})
export class InquiryModule {}
