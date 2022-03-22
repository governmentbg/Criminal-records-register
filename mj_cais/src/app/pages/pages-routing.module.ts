import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";

import { PagesComponent } from "./pages.component";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { NotFoundComponent } from "./miscellaneous/not-found/not-found.component";
import { BulletinOverviewComponent } from "./bulletin/bulletin-overview/bulletin-overview.component";
import { BulletinFormComponent } from "./bulletin/bulletin-form/bulletin-form.component";
import { BulletinResolver } from "./bulletin/bulletin-form/data/bulletin.resolver";
import { FbbcOverviewComponent } from "./fbbc/fbbc-overview/fbbc-overview.component";
import { FbbcFormComponent } from "./fbbc/fbbc-form/fbbc-form.component";
import { FbbcResolver } from "./fbbc/fbbc-form/data/fbbc.resolver";

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
        component: BulletinOverviewComponent,
        // canActivate: [AuthGuard],
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
