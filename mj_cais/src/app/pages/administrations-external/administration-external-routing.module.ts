import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { AdministrationsExtFormmComponent } from "./administrations-ext-form/administrations-ext-form.component";
import { AdministrationsExtResolver } from "./administrations-ext-form/_data/administrations-ext.resolver";
import { AdministrationsExtOverviewComponent } from "./administrations-ext-overview/administrations-ext-overview.component";

const routes: Routes = [
  {
    path: "",
    component: AdministrationsExtOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ["GlobalAdmin"],
      },
    },
  },
  {
    path: "create",
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
    path: "edit/:ID",
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
    path: "preview/:ID",
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
    path: "",
    redirectTo: "administrations-ext",
    pathMatch: "full",
  },
  {
    path: "**",
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdministrationExternalRoutingModule {}
