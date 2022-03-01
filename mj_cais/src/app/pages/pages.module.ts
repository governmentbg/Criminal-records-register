import { NgModule } from "@angular/core";
import { NbMenuModule, NbTabsetModule } from "@nebular/theme";

import { ThemeModule } from "../@theme/theme.module";
import { PagesComponent } from "./pages.component";
import { DashboardModule } from "./dashboard/dashboard.module";
import { PagesRoutingModule } from "./pages-routing.module";
import { MiscellaneousModule } from "./miscellaneous/miscellaneous.module";
import { BulletinOverviewComponent } from "./bulletin/bulletin-overview/bulletin-overview.component";
import { SharedModule } from "../shared.module";
import { PagesMenu } from "./pages-menu";
import { BulletinFormComponent } from "./bulletin/bulletin-form/bulletin-form.component";
import { BulletinResolver } from "./bulletin/bulletin-form/data/bulletin.resolver";
import { CoreModule } from "../@core/core.module";
import { BulletinOffenceFormComponent } from './bulletin/bulletin-form/tabs/bulletin-offence-form/bulletin-offence-form.component';

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    NbMenuModule,
    NbTabsetModule,
    DashboardModule,
    MiscellaneousModule,
    SharedModule,
    CoreModule.forRoot(),
  ],
  declarations: [
    PagesComponent,
    BulletinOverviewComponent,
    BulletinFormComponent,
    BulletinOffenceFormComponent,
  ],
  providers: [PagesMenu, BulletinResolver],
})
export class PagesModule {}
