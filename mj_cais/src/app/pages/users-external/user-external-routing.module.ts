import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { UsersExternalChangePasswordComponent } from "./users-external-change-password/users-external-change-password.component";
import { UsersExternalChangePasswordResolver } from "./users-external-change-password/_data/users-external-password.resolver";
import { UsersExternalFormComponent } from "./users-external-form/users-external-form.component";
import { UsersExternalResolver } from "./users-external-form/_data/users-external.resolver";
import { UsersExternalOverviewComponent } from "./users-external-overview/users-external-overview.component";

const routes: Routes = [
  {
    path: "",
    component: UsersExternalOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ["GlobalAdmin"],
      },
    },
  },
  {
    path: "create",
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
    path: "edit/:ID",
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
    path: "change-password/:ID",
    component: UsersExternalChangePasswordComponent,
    resolve: { dbData: UsersExternalChangePasswordResolver },
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
    path: "",
    redirectTo: "users-external",
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
export class UserExternalRoutingModule {}
