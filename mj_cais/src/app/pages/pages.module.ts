import { NgModule } from "@angular/core";
import { NbListModule, NbMenuModule, NbTabsetModule } from "@nebular/theme";

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
import { BulletinOffencesFormComponent } from "./bulletin/bulletin-form/tabs/bulletin-offences-form/bulletin-offences-form.component";
import { BulletinSanctionsFormComponent } from "./bulletin/bulletin-form/tabs/bulletin-sanctions-form/bulletin-sanctions-form.component";
import { BulletinDecisionFormComponent } from "./bulletin/bulletin-form/tabs/bulletin-decision-form/bulletin-decision-form.component";
import { FbbcActiveOverviewComponent } from "./fbbc/fbbc-overview/fbbc-active-overview/fbbc-active-overview.component";
import { FbbcFormComponent } from "./fbbc/fbbc-form/fbbc-form.component";
import { BulletinDocumentFormComponent } from "./bulletin/bulletin-form/tabs/bulletin-documents-form/bulletin-document-form.component";
import { FbbcDocumentFormComponent } from "./fbbc/fbbc-form/grids/fbbc-document-form/fbbc-document-form.component";
import { EcrisIdentificationOverviewComponent } from "./ecris/ecris-message-overivew/ecris-identification-overview/ecris-identification-overview.component";
import { BulletinNewEissOverviewComponent } from "./bulletin/bulletin-overview/bulletin-neweiss-overview/bulletin-neweiss-overview.component";
import { BulletinActiveOverviewComponent } from "./bulletin/bulletin-overview/bulletin-active-overview/bulletin-active-overview.component";
import { BulletinForDestructionOverviewComponent } from "./bulletin/bulletin-overview/bulletin-fordestruction-overview/bulletin-fordestruction-overview.component";
import { BulletinForRehabilitationOverviewComponent } from "./bulletin/bulletin-overview/bulletin-forrehabilitation-overview/bulletin-forrehabilitation-overview.component";
import { MatMenuModule } from "@angular/material/menu";
import { EcrisReqWaitingFormComponent } from "./ecris/ecris-message-form/ecris-req-waiting-form/ecris-req-waiting-form.component";
import { InternalRequestOverviewComponent } from "./internal-request/internal-request-overview/internal-request-overview.component";
import { EcrisIdentificationFormComponent } from "./ecris/ecris-message-form/ecris-identification-form/ecris-identification-form.component";
import { InternalRequestFormComponent } from "./internal-request/internal-request-form/internal-request-form.component";
import { EcrisReqWaitingOverviewComponent } from "./ecris/ecris-message-overivew/ecris-req-waiting-overview/ecris-req-waiting-overview.component";
import { FbbcForDestructionOverviewComponent } from "./fbbc/fbbc-overview/fbbc-fordestruction-overview/fbbc-fordestruction-overview.component";
import { OffenceCategoryDialogComponent } from "./bulletin/bulletin-form/tabs/bulletin-offences-form/dialog/offence-category-dialog/offence-category-dialog.component";
import { IsinNewOverviewComponent } from "./isin/isin-data-overview/isin-new-overview/isin-new-overview.component";
import { IsinBulletinOverviewComponent } from "./isin/isin-data-form/grids/isin-bulletin-overview/isin-bulletin-overview.component";
import { FbbcDestructedOverviewComponent } from "./fbbc/fbbc-overview/fbbc-destructed-overview/fbbc-destructed-overview.component";
import { IsinIdentifiedOverviewComponent } from "./isin/isin-data-overview/isin-identified-overview/isin-identified-overview.component";
import { IsinDataSelectBulletinFormComponent } from "./isin/isin-data-form/isin-data-select-bulletin-form/isin-data-select-bulletin-form.component";
import { IsinDataPreviewFormComponent } from "./isin/isin-data-form/isin-data-preview-form/isin-data-preview-form.component";
import { BulletinPersonInfoComponent } from "../@core/components/shared/bulletin-person-info/bulletin-person-info.component";
import { IsinDataFormComponent } from "./isin/isin-data-form/isin-data-form/isin-data-form.component";
import { BulletinIsinFormComponent } from "./bulletin/bulletin-form/tabs/bulletin-isin-form/bulletin-isin-form.component";
import { HomeComponent } from "./home/home.component";
import { BulletinNewOfficeOverviewComponent } from "./bulletin/bulletin-overview/bulletin-newoffice-overview/bulletin-newoffice-overview.component";

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    NbMenuModule,
    NbTabsetModule,
    NbListModule,
    DashboardModule,
    MiscellaneousModule,
    SharedModule,
    CoreModule.forRoot(),
    MatMenuModule,
  ],
  declarations: [
    PagesComponent,
    BulletinFormComponent,
    BulletinOffencesFormComponent,
    BulletinSanctionsFormComponent,
    BulletinDecisionFormComponent,
    FbbcActiveOverviewComponent,
    FbbcFormComponent,
    EcrisIdentificationOverviewComponent,
    BulletinDocumentFormComponent,
    FbbcDocumentFormComponent,
    BulletinNewEissOverviewComponent,
    BulletinActiveOverviewComponent,
    BulletinForDestructionOverviewComponent,
    BulletinForRehabilitationOverviewComponent,
    EcrisReqWaitingFormComponent,
    InternalRequestOverviewComponent,
    EcrisReqWaitingOverviewComponent,
    EcrisIdentificationFormComponent,
    InternalRequestFormComponent,
    FbbcForDestructionOverviewComponent,
    OffenceCategoryDialogComponent,
    IsinNewOverviewComponent,
    IsinBulletinOverviewComponent,
    FbbcDestructedOverviewComponent,
    IsinIdentifiedOverviewComponent,
    IsinDataSelectBulletinFormComponent,
    IsinDataPreviewFormComponent,
    BulletinPersonInfoComponent,
    IsinDataFormComponent,
    BulletinIsinFormComponent,
    HomeComponent,
    BulletinNewOfficeOverviewComponent,
  ],
  providers: [PagesMenu, BulletinResolver],
})
export class PagesModule {}
