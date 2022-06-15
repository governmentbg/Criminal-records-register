import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { UsersFormComponent } from "./users-form/users-form.component";
import { UserResolver } from "./users-form/_data/user.resolver";
import { UsersOverviewComponent } from "./users-overview/users-overview.component";

const routes: Routes = [
  {
    path: "",
    component: UsersOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ["Admin", "GlobalAdmin"],
      },
    },
  },
  {
    path: "create",
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
    path: "edit/:ID",
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
    path: "preview/:ID",
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
    path: "",
    redirectTo: "users",
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
export class UserRoutingModule {}
