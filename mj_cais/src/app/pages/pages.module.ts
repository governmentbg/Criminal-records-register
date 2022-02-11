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
import { BulletinFormComponent } from "./bulletin/bulletin-form/bulletin-form.component";
import { BulletinResolver } from "./bulletin/bulletin-form/data/bulletin.resolver";
import { CoreModule } from "../@core/core.module";

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    NbMenuModule,
    DashboardModule,
    MiscellaneousModule,
    SharedModule,
    CoreModule.forRoot(),
  ],
  declarations: [
    PagesComponent,
    BulletinOverviewComponent,
    BulletinFormComponent,
  ],
  providers: [PagesMenu, BulletinResolver],
})
export class PagesModule {}
