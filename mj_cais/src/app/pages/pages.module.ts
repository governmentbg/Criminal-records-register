import { NgModule } from "@angular/core";
import { NbMenuModule, NbTabsetModule } from "@nebular/theme";

import { ThemeModule } from "../@theme/theme.module";
import { PagesComponent } from "./pages.component";
import { DashboardModule } from "./dashboard/dashboard.module";
import { PagesRoutingModule } from "./pages-routing.module";
import { MiscellaneousModule } from "./miscellaneous/miscellaneous.module";
import { SharedModule } from "../shared.module";
import { PagesMenu } from "./pages-menu";
import { BulletinFormComponent } from "./bulletin/bulletin-form/bulletin-form.component";
import { BulletinResolver } from "./bulletin/bulletin-form/_data/bulletin.resolver";
import { CoreModule } from "../@core/core.module";
import { BulletinOffencesFormComponent } from './bulletin/bulletin-form/tabs/bulletin-offences-form/bulletin-offences-form.component';
import { BulletinSanctionsFormComponent } from './bulletin/bulletin-form/tabs/bulletin-sanctions-form/bulletin-sanctions-form.component';
import { BulletinDecisionFormComponent } from './bulletin/bulletin-form/tabs/bulletin-decision-form/bulletin-decision-form.component';
import { FbbcOverviewComponent } from './fbbc/fbbc-overview/fbbc-overview.component';
import { FbbcFormComponent } from './fbbc/fbbc-form/fbbc-form.component';
import { BulletinDocumentFormComponent } from './bulletin/bulletin-form/tabs/bulletin-documents-form/bulletin-document-form.component';
import { FbbcDocumentFormComponent } from './fbbc/fbbc-form/grids/fbbc-document-form/fbbc-document-form.component';
import { EcrisIdentificationOverviewComponent } from "./ecris/ecris-identification-overview/ecris-identification-overview.component";
import { BulletinNewEissOverviewComponent } from './bulletin/bulletin-overview/bulletin-neweiss-overview/bulletin-neweiss-overview.component';
import { BulletinActiveOverviewComponent } from './bulletin/bulletin-overview/bulletin-active-overview/bulletin-active-overview.component';
import { BulletinForDestructionOverviewComponent } from './bulletin/bulletin-overview/bulletin-fordestruction-overview/bulletin-fordestruction-overview.component';
import { BulletinForRehabilitationOverviewComponent } from './bulletin/bulletin-overview/bulletin-forrehabilitation-overview/bulletin-forrehabilitation-overview.component';
import { MatMenuModule } from '@angular/material/menu';
import { EcrisMessageFormComponent } from './ecris/ecris-message-form/ecris-message-form.component';

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
    MatMenuModule
  ],
  declarations: [
    PagesComponent,
    BulletinFormComponent,
    BulletinOffencesFormComponent,
    BulletinSanctionsFormComponent,
    BulletinDecisionFormComponent,
    FbbcOverviewComponent,
    FbbcFormComponent,
    EcrisIdentificationOverviewComponent,
    BulletinDocumentFormComponent,
    FbbcDocumentFormComponent,
    BulletinNewEissOverviewComponent,
    BulletinActiveOverviewComponent,
    BulletinForDestructionOverviewComponent,
    BulletinForRehabilitationOverviewComponent,
    EcrisMessageFormComponent
    ],
  providers: [PagesMenu, BulletinResolver],
})
export class PagesModule {}
