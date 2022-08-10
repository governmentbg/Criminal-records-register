import { NgModule } from "@angular/core";
import { PersonRoutingModule } from "./person-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { PersonApplicationOverviewComponent } from "./person-details-form/grids/person-application-overview/person-application-overview.component";
import { PersonBulletinOverviewComponent } from "./person-details-form/grids/person-bulletin-overview/person-bulletin-overview.component";
import { PersonFbbcOverviewComponent } from "./person-details-form/grids/person-fbbc-overview/person-fbbc-overview.component";
import { PersonDetailsFormComponent } from "./person-details-form/person-details-form.component";
import { PersonRemindFormComponent } from "./person-remind-form/person-remind-form.component";
import { PersonSearchOverviewComponent } from "./person-search-form/grids/person-search-overview/person-search-overview.component";
import { PersonSearchFormComponent } from "./person-search-form/person-search-form.component";
import { PersonEApplicationOverviewComponent } from './person-details-form/grids/person-eapplication-overview/person-eapplication-overview.component';
import { PersonPidOverviewComponent } from './person-details-form/grids/person-pid-overview/person-pid-overview.component';
import { PersonGeneratedReportOverviewComponent } from './person-details-form/grids/person-generated-report-overview/person-generated-report-overview.component';

@NgModule({
  declarations: [
    PersonDetailsFormComponent,
    PersonBulletinOverviewComponent,
    PersonApplicationOverviewComponent,
    PersonFbbcOverviewComponent,
    PersonRemindFormComponent,
    PersonSearchFormComponent,
    PersonSearchOverviewComponent,
    PersonEApplicationOverviewComponent,
    PersonPidOverviewComponent,
    PersonGeneratedReportOverviewComponent,
  ],
  imports: [
    CoreComponentModule, 
    PersonRoutingModule],
})
export class PersonModule {}
