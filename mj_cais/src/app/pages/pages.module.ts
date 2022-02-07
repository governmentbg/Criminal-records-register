import { NgModule } from "@angular/core";
import { NbMenuModule } from "@nebular/theme";

import { ThemeModule } from "../@theme/theme.module";
import { PagesComponent } from "./pages.component";
import { DashboardModule } from "./dashboard/dashboard.module";
import { PagesRoutingModule } from "./pages-routing.module";
import { MiscellaneousModule } from "./miscellaneous/miscellaneous.module";
import { BulletinOverviewComponent } from "./bulletin/bulletin-overview/bulletin-overview.component";
import { SharedModule } from "../shared.module";
import { PagesMenu } from "./pages-menu";
import { BulletinFormComponent } from './bulletin/bulletin-form/bulletin-form.component';

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule.forRoot(),
    NbMenuModule,
    DashboardModule,
    MiscellaneousModule,
    SharedModule,
  ],
  declarations: [PagesComponent, BulletinOverviewComponent, BulletinFormComponent],
  providers: [PagesMenu]
})
export class PagesModule {}
