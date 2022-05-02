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
import { PersonOverviewComponent } from "./person/person-overview/person-overview.component";
import { PersonResolver } from "./person/person-form/_data/person.resolver";
import { PersonFormComponent } from "./person/person-form/person-form.component";
import { AuthGuard } from "../@core/services/common/guard.service";

const routes: Routes = [
  {
    path: "",
    component: PagesComponent,
    children: [
      {
        path: "home",
        component: HomeComponent,
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
        path: "internal-requests",
        component: InternalRequestOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "internal-requests/:ID",
        component: InternalRequestOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "internal-request/create",
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
        component: PersonOverviewComponent,
        // canActivate: [AuthGuard],
      },
      {
        path: "people/create",
        component: PersonFormComponent,
        resolve: { dbData: PersonResolver },
        // canActivate: [AuthGuard],
      },
      {
        path: "people/preview/:ID",
        component: PersonFormComponent,
        resolve: { dbData: PersonResolver },
        data: { edit: true, preview: true },
        // canActivate: [AuthGuard],
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
