import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { BulletinAdministrationFormComponent } from "./bulletin-administration-form/bulletin-administration-form.component";
import { BulletinAdministrationOverviewComponent } from "./bulletin-administration-overview/bulletin-administration-overview.component";

const routes: Routes = [
  {
    path: "",
    component: BulletinAdministrationOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ["Admin", "GlobalAdmin"],
      },
    },
  },
  {
    path: "preview/:ID",
    component: BulletinAdministrationFormComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ["Admin", "GlobalAdmin"],
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
