import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { RegixRequestFormComponent } from "./regix-request-form/regix-request-form.component";
import { ReportApplicationNewOverviewComponent } from "./report-application-overview/report-application-new-overview/report-application-new-overview.component";

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
    path: "regix-request",
    component: RegixRequestFormComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  // {
  //   path: "create",
  //   component: ApplicationFormComponent,
  //   resolve: { dbData: ApplicationResolver },
  // },
  // {
  //   path: "edit/:ID",
  //   component: ApplicationFormComponent,
  //   resolve: { dbData: ApplicationResolver },
  //   data: { edit: true },
  // },
  // {
  //   path: "preview/:ID",
  //   component: ApplicationFormComponent,
  //   resolve: { dbData: ApplicationResolver },
  //   data: { edit: true, preview: true },
  // },

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
