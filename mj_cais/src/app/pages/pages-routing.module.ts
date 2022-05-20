import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";

import { PagesComponent } from "./pages.component";
import { NotFoundComponent } from "./miscellaneous/not-found/not-found.component";
import { BulletinFormComponent } from "./bulletin/bulletin-form/bulletin-form.component";
import { BulletinResolver } from "./bulletin/bulletin-form/_data/bulletin.resolver";

import { FbbcFormComponent } from "./fbbc/fbbc-form/fbbc-form.component";
import { FbbcResolver } from "./fbbc/fbbc-form/data/fbbc.resolver";
import { EcrisIdentificationOverviewComponent } from "./ecris/ecris-message-overivew/ecris-identification-overview/ecris-identification-overview.component";
import { BulletinNewEissOverviewComponent } from "./bulletin/bulletin-overview/bulletin-neweiss-overview/bulletin-neweiss-overview.component";
import { BulletinActiveOverviewComponent } from "./bulletin/bulletin-overview/bulletin-active-overview/bulletin-active-overview.component";
import { BulletinForDestructionOverviewComponent } from "./bulletin/bulletin-overview/bulletin-fordestruction-overview/bulletin-fordestruction-overview.component";
import { BulletinForRehabilitationOverviewComponent } from "./bulletin/bulletin-overview/bulletin-forrehabilitation-overview/bulletin-forrehabilitation-overview.component";
import { EcrisReqWaitingFormComponent } from "./ecris/ecris-message-form/ecris-req-waiting-form/ecris-req-waiting-form.component";
import { EcrisReqWaitingResolver } from "./ecris/ecris-message-form/ecris-req-waiting-form/_data/ecris-req-waiting.resolver";
import { InternalRequestOverviewComponent } from "./internal-request/internal-request-overview/internal-request-overview.component";
import { InternalRequestFormComponent } from "./internal-request/internal-request-form/internal-request-form.component";
import { InternalRequestResolver } from "./internal-request/internal-request-form/_data/internal-request.resolver";
import { EcrisReqWaitingOverviewComponent } from "./ecris/ecris-message-overivew/ecris-req-waiting-overview/ecris-req-waiting-overview.component";
import { FbbcActiveOverviewComponent } from "./fbbc/fbbc-overview/fbbc-active-overview/fbbc-active-overview.component";
import { FbbcForDestructionOverviewComponent } from "./fbbc/fbbc-overview/fbbc-fordestruction-overview/fbbc-fordestruction-overview.component";
import { IsinNewOverviewComponent } from "./isin/isin-data-overview/isin-new-overview/isin-new-overview.component";
import { EcrisIdentificationResolver } from "./ecris/ecris-message-form/ecris-identification-form/_data/ecris-identification.resolver";
import { EcrisIdentificationFormComponent } from "./ecris/ecris-message-form/ecris-identification-form/ecris-identification-form.component";
import { FbbcDestructedOverviewComponent } from "./fbbc/fbbc-overview/fbbc-destructed-overview/fbbc-destructed-overview.component";
import { IsinIdentifiedOverviewComponent } from "./isin/isin-data-overview/isin-identified-overview/isin-identified-overview.component";
import { IsinDataSelectBulletinFormComponent } from "./isin/isin-data-form/isin-data-select-bulletin-form/isin-data-select-bulletin-form.component";
import { IsinDataPreviewFormComponent } from "./isin/isin-data-form/isin-data-preview-form/isin-data-preview-form.component";
import { HomeComponent } from "./home/home.component";
import { BulletinNewOfficeOverviewComponent } from "./bulletin/bulletin-overview/bulletin-newoffice-overview/bulletin-newoffice-overview.component";
import { ApplicationFormComponent } from "./application/application-form/application-form.component";
import { ApplicationResolver } from "./application/application-form/data/application.resolver";
import { AuthGuard } from "../@core/services/common/guard.service";
import { PersonDetailsFormComponent } from "./person/person-details-form/person-details-form.component";
import { PersonDetailsResolver } from "./person/person-details-form/_data/person-details.resolver";
import { PersonRemindFormComponent } from "./person/person-remind-form/person-remind-form.component";
import { PersonSearchFormComponent } from "./person/person-search-form/person-search-form.component";
import { PersonSearchResolver } from "./person/person-search-form/_data/person-search.resolver";
import { UsersOverviewComponent } from "./users/users-overview/users-overview.component";
import { UsersExternalOverviewComponent } from "./users-external/users-external-overview/users-external-overview.component";
import { UsersCitizenOverviewComponent } from "./users-public/users-citizen-overview/users-citizen-overview.component";
import { UsersFormComponent } from "./users/users-form/users-form.component";
import { UserResolver } from "./users/users-form/_data/user.resolver";
import { NgxPermissionsGuard } from "ngx-permissions";
import { ApplicationNewOverviewComponent } from "./application/application-overview/application-new-overview/application-new-overview.component";
import { AdministrationsExtOverviewComponent } from "./administrations-external/administrations-ext-overview/administrations-ext-overview.component";
import { AdministrationsExtFormmComponent } from "./administrations-external/administrations-ext-form/administrations-ext-form.component";
import { AdministrationsExtResolver } from "./administrations-external/administrations-ext-form/_data/administrations-ext.resolver";
import { UsersExternalFormComponent } from "./users-external/users-external-form/users-external-form.component";
import { UsersExternalResolver } from "./users-external/users-external-form/_data/users-external.resolver";
import { BulletinEventsOverviewComponent } from "./bulletin-events/bulletin-events-overview/bulletin-events-overview.component";

const routes: Routes = [
  {
    path: "",
    component: PagesComponent,
    // canActivate: [AuthGuard],
    // canActivateChild: [AuthGuard],
    children: [
      {
        path: "home",
        component: HomeComponent,
      },
      {
        path: "applications",
        component: ApplicationNewOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "applications/create",
        component: ApplicationFormComponent,
        resolve: { dbData: ApplicationResolver },
        // canActivate: [AuthGuard],
      },
      {
        path: "applications/edit/:ID",
        component: ApplicationFormComponent,
        resolve: { dbData: ApplicationResolver },
        data: { edit: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "applications/preview/:ID",
        component: ApplicationFormComponent,
        resolve: { dbData: ApplicationResolver },
        data: { edit: true, preview: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "bulletins",
        component: BulletinActiveOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "bulletins-new-office",
        component: BulletinNewOfficeOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "bulletins-new-eiss",
        component: BulletinNewEissOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "bulletins-for-destruction",
        component: BulletinForDestructionOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "bulletins-for-rehabilitation",
        component: BulletinForRehabilitationOverviewComponent,
      },
      {
        path: "bulletins-requests",
        //component: PagesComponent, // todo:
      },
      {
        path: "bulletins/create",
        component: BulletinFormComponent,
        resolve: { dbData: BulletinResolver },
        // canActivate: [AuthGuard],
      },
      {
        path: "bulletins/edit/:ID",
        component: BulletinFormComponent,
        resolve: { dbData: BulletinResolver },
        data: { edit: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "bulletins/preview/:ID",
        component: BulletinFormComponent,
        resolve: { dbData: BulletinResolver },
        data: { edit: true, preview: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "bulletin-events",
        component: BulletinEventsOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "internal-requests",
        component: InternalRequestOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "internal-requests/:ID", // this id is bulletin id
        component: InternalRequestOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "internal-requests/create/:ID",
        component: InternalRequestFormComponent,
        resolve: { dbData: InternalRequestResolver },
      },
      {
        path: "internal-requests/edit/:ID",
        component: InternalRequestFormComponent,
        resolve: { dbData: InternalRequestResolver },
        data: { edit: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "internal-requests/preview/:ID",
        component: InternalRequestFormComponent,
        resolve: { dbData: InternalRequestResolver },
        data: { edit: true, preview: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "fbbcs",
        component: FbbcActiveOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "fbbcs-for-destruction",
        component: FbbcForDestructionOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "fbbcs-destructed",
        component: FbbcDestructedOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "fbbcs/create",
        component: FbbcFormComponent,
        resolve: { dbData: FbbcResolver },
        // canActivate: [AuthGuard],
      },
      {
        path: "fbbcs/edit/:ID",
        component: FbbcFormComponent,
        resolve: { dbData: FbbcResolver },
        data: { edit: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "fbbcs/preview/:ID",
        component: FbbcFormComponent,
        resolve: { dbData: FbbcResolver },
        data: { edit: true, preview: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "ecris-identification",
        component: EcrisIdentificationOverviewComponent,
        // canActivate: [AuthGuard],
      },

      {
        path: "ecris-identification/edit/:ID",
        component: EcrisIdentificationFormComponent,
        resolve: { dbData: EcrisIdentificationResolver },
        data: { edit: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "ecris-identification/preview/:ID",
        component: EcrisIdentificationFormComponent,
        resolve: { dbData: EcrisIdentificationResolver },
        data: { edit: true, preview: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "ecris-req-waiting",
        component: EcrisReqWaitingOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "ecris-req-waiting/create",
        component: EcrisReqWaitingFormComponent,
        resolve: { dbData: EcrisReqWaitingResolver },
        // canActivate: [AuthGuard],
      },
      {
        path: "ecris-req-waiting/edit/:ID",
        component: EcrisReqWaitingFormComponent,
        resolve: { dbData: EcrisReqWaitingResolver },
        data: { edit: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "ecris-req-waiting/preview/:ID",
        component: EcrisReqWaitingFormComponent,
        resolve: { dbData: EcrisReqWaitingResolver },
        data: { edit: true, preview: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "isin-new",
        component: IsinNewOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "isin-identified",
        component: IsinIdentifiedOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "isin-data/select-bulletin/:ID",
        component: IsinDataSelectBulletinFormComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "isin-data/preview/:ID",
        component: IsinDataPreviewFormComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "people",
        component: PersonSearchFormComponent,
        resolve: { dbData: PersonSearchResolver },

        // canActivate: [AuthGuard],
      },
      {
        path: "people/preview/:ID",
        component: PersonDetailsFormComponent,
        resolve: { dbData: PersonDetailsResolver },
        data: { edit: true, preview: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "people/remind/:ID",
        component: PersonRemindFormComponent,
        resolve: { dbData: PersonSearchResolver },
        // canActivate: [AuthGuard],
      },
      {
        path: "users",
        component: UsersOverviewComponent,
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            only: ["Admin", "GlobalAdmin"],
          },
        },
      },
      {
        path: "users/create",
        component: UsersFormComponent,
        resolve: { dbData: UserResolver },
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            only: ["Admin", "GlobalAdmin"],
          },
        },
      },
      {
        path: "users/edit/:ID",
        component: UsersFormComponent,
        resolve: { dbData: UserResolver },
        canActivate: [NgxPermissionsGuard],
        data: {
          edit: true,
          permissions: {
            only: ["Admin", "GlobalAdmin"],
          },
        },
      },
      {
        path: "users/preview/:ID",
        component: UsersFormComponent,
        resolve: { dbData: UserResolver },
        canActivate: [NgxPermissionsGuard],
        data: {
          edit: true,
          preview: true,
          permissions: {
            only: ["Admin", "GlobalAdmin"],
          },
        },
      },
      {
        path: "administrations-ext",
        component: AdministrationsExtOverviewComponent,
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            only: ["GlobalAdmin"],
          },
        },
      },
      {
        path: "administrations-ext/create",
        component: AdministrationsExtFormmComponent,
        resolve: { dbData: AdministrationsExtResolver },
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            only: ["GlobalAdmin"],
          },
        },
      },
      {
        path: "administrations-ext/edit/:ID",
        component: AdministrationsExtFormmComponent,
        resolve: { dbData: AdministrationsExtResolver },
        canActivate: [NgxPermissionsGuard],
        data: {
          edit: true,
          permissions: {
            only: ["GlobalAdmin"],
          },
        },
      },
      {
        path: "administrations-ext/preview/:ID",
        component: AdministrationsExtFormmComponent,
        resolve: { dbData: AdministrationsExtResolver },
        canActivate: [NgxPermissionsGuard],
        data: {
          edit: true,
          preview: true,
          permissions: {
            only: ["GlobalAdmin"],
          },
        },
      },
      {
        path: "users-external",
        component: UsersExternalOverviewComponent,
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            only: ["GlobalAdmin"],
          },
        },
      },

      {
        path: "users-external/create",
        component: UsersExternalFormComponent,
        resolve: { dbData: UsersExternalResolver },
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            only: ["GlobalAdmin"],
          },
        },
      },
      {
        path: "users-external/edit/:ID",
        component: UsersExternalFormComponent,
        resolve: { dbData: UsersExternalResolver },
        canActivate: [NgxPermissionsGuard],
        data: {
          edit: true,
          permissions: {
            only: ["GlobalAdmin"],
          },
        },
      },
      {
        path: "users-external/preview/:ID",
        component: UsersExternalFormComponent,
        resolve: { dbData: UsersExternalResolver },
        canActivate: [NgxPermissionsGuard],
        data: {
          edit: true,
          preview: true,
          permissions: {
            only: ["GlobalAdmin"],
          },
        },
      },
      {
        path: "users-public",
        component: UsersCitizenOverviewComponent,
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            only: ["Admin", "GlobalAdmin"],
          },
        },
      },
      {
        path: "miscellaneous",
        loadChildren: () =>
          import("./miscellaneous/miscellaneous.module").then(
            (m) => m.MiscellaneousModule
          ),
      },
      {
        path: "",
        redirectTo: "home",
        pathMatch: "full",
      },
      {
        path: "**",
        component: NotFoundComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
