import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { ApplicationStatisticsFormComponent } from "./application-statistics-form/application-statistics-form.component";
import { BulletinStatisticsFormComponent } from "./bulletin-statistics-form/bulletin-statistics-form.component";
import { StatisticsResolver } from "./_data/statistics.resolver";

const routes: Routes = [
  {
    path: "bulletins",
    component: BulletinStatisticsFormComponent,
    resolve: { dbData: StatisticsResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [
          RoleNameEnum.Judge,
          RoleNameEnum.Normal,
          RoleNameEnum.Admin,
          RoleNameEnum.GlobalAdmin,
          RoleNameEnum.CentralAuth,
        ],
      },
    },
  },
  {
    path: "applications",
    component: ApplicationStatisticsFormComponent,
    resolve: { dbData: StatisticsResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [
          RoleNameEnum.Judge,
          RoleNameEnum.Normal,
          RoleNameEnum.Admin,
          RoleNameEnum.GlobalAdmin,
          RoleNameEnum.CentralAuth,
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
export class StatisticsRoutingModule {}
