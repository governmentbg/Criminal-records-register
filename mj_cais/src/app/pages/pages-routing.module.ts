import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { PagesComponent } from "./pages.component";
import { NotFoundComponent } from "./miscellaneous/not-found/not-found.component";
import { HomeComponent } from "./home/home.component";
import { AuthGuard } from "../@core/services/common/guard.service";
import { HomeResolver } from "./home/_data/home.resolver";
import { EmptyComponent } from "../@core/components/empty/empty.component";
import { HelpComponent } from "./help/help.component";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../@core/constants/role-name.enum";

const routes: Routes = [
  {
    path: "",
    component: PagesComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      {
        path: "home",
        component: HomeComponent,
        resolve: { dbData: HomeResolver },
      },
      {
        path: "empty",
        component: EmptyComponent,
      },
      {
        path: "applications",
        loadChildren: () =>
          import("./application/application.module").then(
            (m) => m.ApplicationModule
          ),
      },
      {
        path: "report-applications",
        loadChildren: () =>
          import("./report-application/report-application.module").then(
            (m) => m.ReportApplicationModule
          ),
      },
      {
        path: "bulletins",
        loadChildren: () =>
          import("./bulletin/bulletin.module").then((m) => m.BulletinModule),
      },
      {
        path: "bulletin-administrations",
        loadChildren: () =>
          import(
            "./bulletin-administrations/bulletin-administrations.module"
          ).then((m) => m.BulletinAdministrationsModule),
      },
      {
        path: "ecris",
        loadChildren: () =>
          import("./ecris/ecris.module").then((m) => m.EcrisModule),
      },
      {
        path: "ecris-tcn",
        loadChildren: () =>
          import("./ecris-tcn/ecris-tcn.module").then((m) => m.EcrisTcnModule),
      },
      {
        path: "fbbcs",
        loadChildren: () =>
          import("./fbbc/fbbc.module").then((m) => m.FbbcModule),
      },
      {
        path: "internal-requests",
        loadChildren: () =>
          import("./internal-request/internal-request.module").then(
            (m) => m.InternalRequestModule
          ),
      },
      {
        path: "isin",
        loadChildren: () =>
          import("./isin/isin.module").then((m) => m.IsinModule),
      },
      {
        path: "people",
        loadChildren: () =>
          import("./person/person.module").then((m) => m.PersonModule),
      },
      {
        path: "users",
        loadChildren: () =>
          import("./users/user.module").then((m) => m.UserModule),
      },
      {
        path: "users-external",
        loadChildren: () =>
          import("./users-external/user-external.module").then(
            (m) => m.UserExternalModule
          ),
      },
      {
        path: "users-public",
        loadChildren: () =>
          import("./users-public/user-public.module").then(
            (m) => m.UserPublicModule
          ),
      },
      {
        path: "miscellaneous",
        loadChildren: () =>
          import("./miscellaneous/miscellaneous.module").then(
            (m) => m.MiscellaneousModule
          ),
      },
      {
        path: "administrations-ext",
        loadChildren: () =>
          import(
            "./administrations-external/administration-external.module"
          ).then((m) => m.AdministrationExternalModule),
      },
      {
        path: "e-applicaiton",
        loadChildren: () =>
          import("./e-application/eapplication.module").then(
            (m) => m.EApplicationModule
          ),
      },
      {
        path: "e-applicaiton-report",
        loadChildren: () =>
          import("./e-application-report/e-application-report.module").then(
            (m) => m.EApplicationReportModule
          ),
      },
      {
        path: "inquiry",
        loadChildren: () =>
          import("./inquiry/inquiry.module").then((m) => m.InquiryModule),
      },
      {
        path: "statistics",
        loadChildren: () =>
          import("./statistics/statistics.module").then(
            (m) => m.StatisticsModule
          ),
      },
      {
        path: "help",
        component: HelpComponent,
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            only: [
              RoleNameEnum.Normal,
              RoleNameEnum.Judge,
              RoleNameEnum.CentralAuth,
              RoleNameEnum.Admin,
              RoleNameEnum.GlobalAdmin,
            ],
          },
        },
      },
      {
        path: "",
        redirectTo: "home",
        pathMatch: "full",
      },
      {
        path: "**",
        component: NotFoundComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
