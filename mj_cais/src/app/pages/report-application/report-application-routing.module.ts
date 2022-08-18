import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { AllGeneratedReportOverviewComponent } from "./all-generated-report-overview/all-generated-report-overview.component";
import { RegixRequestFormComponent } from "./regix-request-form/regix-request-form.component";
import { ReportApplicationFormComponent } from "./report-application-form/report-application-form.component";
import { ReportApplicationResolver } from "./report-application-form/_data/report-application.resolver";
import { ReportApplicationApprovedOverviewComponent } from "./report-application-overview/report-application-approved-overview/report-application-approved-overview.component";
import { ReportApplicationDeliveredOverviewComponent } from "./report-application-overview/report-application-delivered-overview/report-application-delivered-overview.component";
import { ReportApplicationNewOverviewComponent } from "./report-application-overview/report-application-new-overview/report-application-new-overview.component";
import { SearchReportApplicationOverviewComponent } from "./search-report-application-overview/search-report-application-overview.component";

const routes: Routes = [
  {
    path: "",
    component: ReportApplicationNewOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "approved",
    component: ReportApplicationApprovedOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "delivered",
    component: ReportApplicationDeliveredOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "regix-request",
    component: RegixRequestFormComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "create",
    component: ReportApplicationFormComponent,
    resolve: { dbData: ReportApplicationResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "edit/:ID",
    component: ReportApplicationFormComponent,
    resolve: { dbData: ReportApplicationResolver },
    data: {
      edit: true,
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "preview/:ID",
    component: ReportApplicationFormComponent,
    resolve: { dbData: ReportApplicationResolver },
    data: {
      edit: true,
      preview: true,
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "all-generated-reports",
    component: AllGeneratedReportOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "search",
    component: SearchReportApplicationOverviewComponent,
  },
  {
    path: "",
    redirectTo: "report-applications",
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
export class ReportApplicationRoutingModule {}
