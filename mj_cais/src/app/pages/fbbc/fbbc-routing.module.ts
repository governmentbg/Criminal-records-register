import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { FbbcResolver } from "./fbbc-form/data/fbbc.resolver";
import { FbbcFormComponent } from "./fbbc-form/fbbc-form.component";
import { FbbcActiveOverviewComponent } from "./fbbc-overview/fbbc-active-overview/fbbc-active-overview.component";
import { FbbcDestructedOverviewComponent } from "./fbbc-overview/fbbc-destructed-overview/fbbc-destructed-overview.component";
import { FbbcForDestructionOverviewComponent } from "./fbbc-overview/fbbc-fordestruction-overview/fbbc-fordestruction-overview.component";

const routes: Routes = [
  {
    path: "",
    component: FbbcActiveOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "for-destruction",
    component: FbbcForDestructionOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "destructed",
    component: FbbcDestructedOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "create",
    component: FbbcFormComponent,
    resolve: { dbData: FbbcResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "edit/:ID",
    component: FbbcFormComponent,
    resolve: { dbData: FbbcResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      edit: true,
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "preview/:ID",
    component: FbbcFormComponent,
    resolve: { dbData: FbbcResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      edit: true,
      preview: true,
      permissions: {
        only: [
          RoleNameEnum.CentralAuth,
          RoleNameEnum.Normal,
          RoleNameEnum.Judge,
        ],
      },
    },
  },
  {
    path: "",
    redirectTo: "fbbcs",
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
export class FbbcRoutingModule {}
