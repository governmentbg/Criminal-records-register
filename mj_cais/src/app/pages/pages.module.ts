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
import { BulletinOffencesFormComponent } from './bulletin/bulletin-form/tabs/bulletin-offences-form/bulletin-offences-form.component';
import { BulletinSanctionsFormComponent } from './bulletin/bulletin-form/tabs/bulletin-sanctions-form/bulletin-sanctions-form.component';
import { BulletinDecisionFormComponent } from './bulletin/bulletin-form/tabs/bulletin-decision-form/bulletin-decision-form.component';
import { FbbcOverviewComponent } from './fbbc/fbbc-overview/fbbc-overview.component';
import { FbbcFormComponent } from './fbbc/fbbc-form/fbbc-form.component';
import { BulletinDocumentFormComponent } from './bulletin/bulletin-form/tabs/bulletin-documents-form/bulletin-document-form.component';

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
    BulletinOffencesFormComponent,
    BulletinSanctionsFormComponent,
    BulletinDecisionFormComponent,
    FbbcOverviewComponent,
    FbbcFormComponent,
    BulletinDocumentFormComponent,
  ],
  providers: [PagesMenu, BulletinResolver],
})
export class PagesModule {}
