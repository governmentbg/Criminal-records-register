import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";

import { PagesComponent } from "./pages.component";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { NotFoundComponent } from "./miscellaneous/not-found/not-found.component";
import { BulletinFormComponent } from "./bulletin/bulletin-form/bulletin-form.component";
import { BulletinResolver } from "./bulletin/bulletin-form/_data/bulletin.resolver";
import { FbbcOverviewComponent } from "./fbbc/fbbc-overview/fbbc-overview.component";
import { FbbcFormComponent } from "./fbbc/fbbc-form/fbbc-form.component";
import { FbbcResolver } from "./fbbc/fbbc-form/data/fbbc.resolver";
import { EcrisIdentificationOverviewComponent } from "./ecris/ecris-message-overivew/ecris-identification-overview/ecris-identification-overview.component";
import { BulletinNewEissOverviewComponent } from "./bulletin/bulletin-overview/bulletin-neweiss-overview/bulletin-neweiss-overview.component";
import { BulletinActiveOverviewComponent } from "./bulletin/bulletin-overview/bulletin-active-overview/bulletin-active-overview.component";
import { BulletinForDestructionOverviewComponent } from "./bulletin/bulletin-overview/bulletin-fordestruction-overview/bulletin-fordestruction-overview.component";
import { BulletinForRehabilitationOverviewComponent } from "./bulletin/bulletin-overview/bulletin-forrehabilitation-overview/bulletin-forrehabilitation-overview.component";
import { EcrisMessageFormComponent } from "./ecris/ecris-message-form/ecris-message-form.component";
import { EcrisMessageResolver } from "./ecris/ecris-message-form/_data/ecris-message.resolver";
import { InternalRequestOverviewComponent } from "./internal-request/internal-request-overview/internal-request-overview.component";

const routes: Routes = [
  {
    path: "",
    component: PagesComponent,
    children: [
      {
        path: "iot-dashboard",
        component: DashboardComponent,
      },
      {
        path: "bulletins",
        component: BulletinActiveOverviewComponent,
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
        path: "fbbcs",
        component: FbbcOverviewComponent,
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
        path: "ecris-messages/create",
        component: EcrisMessageFormComponent,
        resolve: { dbData: EcrisMessageResolver },
        // canActivate: [AuthGuard],
      },
      {
        path: "ecris-messages/edit/:ID",
        component: EcrisMessageFormComponent,
        resolve: { dbData: EcrisMessageResolver },
        data: { edit: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "ecris-messages/preview/:ID",
        component: EcrisMessageFormComponent,
        resolve: { dbData: EcrisMessageResolver },
        data: { edit: true, preview: true },
        // canActivate: [AuthGuard],
      },
      {
        path: "layout",
        loadChildren: () =>
          import("./layout/layout.module").then((m) => m.LayoutModule),
      },
      {
        path: "forms",
        loadChildren: () =>
          import("./forms/forms.module").then((m) => m.FormsModule),
      },
      {
        path: "ui-features",
        loadChildren: () =>
          import("./ui-features/ui-features.module").then(
            (m) => m.UiFeaturesModule
          ),
      },
      {
        path: "modal-overlays",
        loadChildren: () =>
          import("./modal-overlays/modal-overlays.module").then(
            (m) => m.ModalOverlaysModule
          ),
      },
      {
        path: "extra-components",
        loadChildren: () =>
          import("./extra-components/extra-components.module").then(
            (m) => m.ExtraComponentsModule
          ),
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
        redirectTo: "iot-dashboard",
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
