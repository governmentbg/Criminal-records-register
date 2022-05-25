import { NgModule } from "@angular/core";
import { NbListModule, NbMenuModule, NbTabsetModule, NbToggleModule } from "@nebular/theme";

import { ThemeModule } from "../@theme/theme.module";
import { PagesComponent } from "./pages.component";
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
import { BulletinStatusHistoryOverviewComponent } from './bulletin/bulletin-form/tabs/bulletin-status-history-overview/bulletin-status-history-overview.component';
import { ApplicationFormComponent } from './application/application-form/application-form.component';
import { PostLoginComponent } from "./auth/post-login";
import { PersonDetailsFormComponent } from './person/person-details-form/person-details-form.component';
import { PersonBulletinOverviewComponent } from './person/person-details-form/grids/person-bulletin-overview/person-bulletin-overview.component';
import { PersonApplicationOverviewComponent } from './person/person-details-form/grids/person-application-overview/person-application-overview.component';
import { PersonFbbcOverviewComponent } from './person/person-details-form/grids/person-fbbc-overview/person-fbbc-overview.component';
import { PersonRemindFormComponent } from "./person/person-remind-form/person-remind-form.component";
import { PersonSearchFormComponent } from './person/person-search-form/person-search-form.component';
import { PersonSearchOverviewComponent } from './person/person-search-form/grids/person-search-overview/person-search-overview.component';
import { UsersOverviewComponent } from './users/users-overview/users-overview.component';
import { UsersExternalOverviewComponent } from './users-external/users-external-overview/users-external-overview.component';
import { UsersCitizenOverviewComponent } from './users-public/users-citizen-overview/users-citizen-overview.component';
import { UsersFormComponent } from './users/users-form/users-form.component';
import { NgxPermissionsModule } from "ngx-permissions";
import { ApplicationNewOverviewComponent } from './application/application-overview/application-new-overview/application-new-overview.component';
import { AdministrationsExtFormmComponent } from './administrations-external/administrations-ext-form/administrations-ext-form.component';
import { AdministrationsExtOverviewComponent } from './administrations-external/administrations-ext-overview/administrations-ext-overview.component';
import { UsersExternalFormComponent } from './users-external/users-external-form/users-external-form.component';
import { ApplicationWaitingPaymentComponent } from './application/application-overview/application-waiting-payment/application-waiting-payment.component';
import { BulletinEventsOverviewComponent } from './bulletin-events/bulletin-events-overview/bulletin-events-overview.component';
import { BulletinEventsArticleOverviewComponent } from './bulletin-events/bulletin-events-overview/tabs/bulletin-events-article-overview/bulletin-events-article-overview.component';
import { BulletinEventsDocumentOverviewComponent } from './bulletin-events/bulletin-events-overview/tabs/bulletin-events-document-overview/bulletin-events-document-overview.component';
import { ApplicationTaxFreeOverviewComponent } from './application/application-overview/application-tax-free-overview/application-tax-free-overview.component';

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    NbMenuModule,
    NbTabsetModule,
    NbListModule,
    MiscellaneousModule,
    SharedModule,
    CoreModule.forRoot(),
    MatMenuModule,
    NbToggleModule,
    NgxPermissionsModule.forChild(),
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
    BulletinStatusHistoryOverviewComponent,
    ApplicationFormComponent,
    PostLoginComponent,
    PersonDetailsFormComponent,
    PersonBulletinOverviewComponent,
    PersonApplicationOverviewComponent,
    PersonFbbcOverviewComponent,
    PersonRemindFormComponent,
    PersonSearchFormComponent,
    PersonSearchOverviewComponent,
    UsersOverviewComponent,
    UsersExternalOverviewComponent,
    UsersCitizenOverviewComponent,
    UsersFormComponent,
    ApplicationNewOverviewComponent,
    AdministrationsExtFormmComponent,
    AdministrationsExtOverviewComponent,
    UsersExternalFormComponent,
    ApplicationWaitingPaymentComponent,
    BulletinEventsOverviewComponent,
    BulletinEventsArticleOverviewComponent,
    BulletinEventsDocumentOverviewComponent,
    ApplicationTaxFreeOverviewComponent
  ],
  providers: [PagesMenu, BulletinResolver],
})
export class PagesModule {}
