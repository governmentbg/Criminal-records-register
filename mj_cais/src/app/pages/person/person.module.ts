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
import { NbRouteTabsetModule } from "@nebular/theme";

@NgModule({
  declarations: [
    PersonDetailsFormComponent,
    PersonBulletinOverviewComponent,
    PersonApplicationOverviewComponent,
    PersonFbbcOverviewComponent,
    PersonRemindFormComponent,
    PersonSearchFormComponent,
    PersonSearchOverviewComponent,
  ],
  imports: [
    CoreComponentModule, 
    PersonRoutingModule,
    NbRouteTabsetModule],
})
export class PersonModule {}
