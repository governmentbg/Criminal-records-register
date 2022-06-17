import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { BulletinEventsOverviewComponent } from "../bulletin-events/bulletin-events-overview/bulletin-events-overview.component";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { BulletinFormComponent } from "./bulletin-form/bulletin-form.component";
import { BulletinResolver } from "./bulletin-form/_data/bulletin.resolver";
import { BulletinActiveOverviewComponent } from "./bulletin-overview/bulletin-active-overview/bulletin-active-overview.component";
import { BulletinForDestructionOverviewComponent } from "./bulletin-overview/bulletin-fordestruction-overview/bulletin-fordestruction-overview.component";
import { BulletinForRehabilitationOverviewComponent } from "./bulletin-overview/bulletin-forrehabilitation-overview/bulletin-forrehabilitation-overview.component";
import { BulletinNewEissOverviewComponent } from "./bulletin-overview/bulletin-neweiss-overview/bulletin-neweiss-overview.component";
import { BulletinNewOfficeOverviewComponent } from "./bulletin-overview/bulletin-newoffice-overview/bulletin-newoffice-overview.component";

const routes: Routes = [
  {
    path: "active",
    component: BulletinActiveOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "new-office",
    component: BulletinNewOfficeOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "new-eiss",
    component: BulletinNewEissOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "for-destruction",
    component: BulletinForDestructionOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "for-rehabilitation",
    component: BulletinForRehabilitationOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "create",
    component: BulletinFormComponent,
    resolve: { dbData: BulletinResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "edit/:ID",
    component: BulletinFormComponent,
    resolve: { dbData: BulletinResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      edit: true,
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "preview/:ID",
    component: BulletinFormComponent,
    resolve: { dbData: BulletinResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      edit: true,
      preview: true,
      permissions: {
        only: [
          RoleNameEnum.CentralAuth,
          RoleNameEnum.Judge,
          RoleNameEnum.Normal,
        ],
      },
    },
  },
  {
    path: "events",
    component: BulletinEventsOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      edit: true,
      preview: true,
      permissions: {
        only: [
          RoleNameEnum.CentralAuth,
          RoleNameEnum.Judge,
          RoleNameEnum.Normal,
        ],
      },
    },
  },
  {
    path: "",
    redirectTo: "bulletins",
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
export class BulletinRoutingModule {}
