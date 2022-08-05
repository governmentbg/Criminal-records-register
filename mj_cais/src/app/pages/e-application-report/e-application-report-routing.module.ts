import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { EapplicationReportOverviewComponent } from "./e-application-report-overview/eapplication-report-overview/eapplication-report-overview.component";
import { EapplicationReportSearchPersOverviewComponent } from "./e-application-report-overview/eapplication-report-search-pers-overview/eapplication-report-search-pers-overview.component";

const routes: Routes = [
  {
    path: "reports/overview",
    component: EapplicationReportOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "search-pers/overview",
    component: EapplicationReportSearchPersOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "",
    redirectTo: "reports/overview",
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
export class EApplicationReportRoutingModule {}
