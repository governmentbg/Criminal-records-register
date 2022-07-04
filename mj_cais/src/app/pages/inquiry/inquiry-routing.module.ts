import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { ReportBulletinSearchFormComponent } from "./report-bulletin-search-form/report-bulletin-search-form.component";
import { ReportBulletinResolver } from "./report-bulletin-search-form/_data/report-bulletin.resolver";
import { ReportPersonSearchFormComponent } from "./report-person-search-form/report-person-search-form.component";
import { ReportPersonResolver } from "./report-person-search-form/_data/report-person.resolver";

const routes: Routes = [
  {
    path: "search-bulletins",
    component: ReportBulletinSearchFormComponent,
    resolve: { dbData: ReportBulletinResolver },
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
    path: "search-people",
    component: ReportPersonSearchFormComponent,
    resolve: { dbData: ReportPersonResolver },
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
    redirectTo: "search-bulletins",
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
export class InquiryRoutingModule {}
