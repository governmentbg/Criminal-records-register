import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { BulletinAdministrationFormComponent } from "./bulletin-administration-form/bulletin-administration-form.component";
import { BulletinAdministrationSearchFormComponent } from "./bulletin-administration-search-form/bulletin-administration-search-form.component";
import { BulletinAdministrationSearchResolver } from "./bulletin-administration-search-form/_data/bulletin-administration-search.resolver";

const routes: Routes = [
  {
    path: "",
    component: BulletinAdministrationSearchFormComponent,
    resolve: { dbData: BulletinAdministrationSearchResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Supervisor],
      },
    },
  },
  {
    path: "preview/:ID",
    component: BulletinAdministrationFormComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Supervisor],
      },
    },
  },
  {
    path: "",
    redirectTo: "bulletin-administrations",
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
export class BulletinAdministrationsRoutingModule {}
