import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { ApplicationFormComponent } from "./application-form/application-form.component";
import { ApplicationSearchFormComponent } from "./application-form/application-search-form/application-search-form.component";
import { ApplicationSearchResolver } from "./application-form/_data/application-search.resolver";
import { ApplicationResolver } from "./application-form/_data/application.resolver";
import { ApplicationBulletinsSelectionComponent } from "./application-overview/application-bulletins-selection/application-bulletins-selection.component";
import { ApplicationForCheckComponent } from "./application-overview/application-for-check/application-for-check.component";
import { ApplicationForSigningComponent } from "./application-overview/application-for-signing/application-for-signing.component";
import { ApplicationForSiningByJudgeComponent } from "./application-overview/application-for-sining-by-judge/application-for-sining-by-judge.component";
import { ApplicationNewOverviewComponent } from "./application-overview/application-new-overview/application-new-overview.component";
import { ApplicationSearchOverviewComponent } from "./application-overview/application-search-overview/application-search-overview.component";
import { ApplicationTaxFreeOverviewComponent } from "./application-overview/application-tax-free-overview/application-tax-free-overview.component";
import { ApplicationWaitingPaymentComponent } from "./application-overview/application-waiting-payment/application-waiting-payment.component";
import { ApplicationRequestComponent } from "./application-request/application-request.component";

const routes: Routes = [
  {
    path: "",
    component: ApplicationNewOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "request",
    component: ApplicationRequestComponent,
  },
  {
    path: "create",
    component: ApplicationFormComponent,
    resolve: { dbData: ApplicationResolver },
  },
  {
    path: "edit/:ID",
    component: ApplicationFormComponent,
    resolve: { dbData: ApplicationResolver },
    data: { edit: true },
  },
  {
    path: "preview/:ID",
    component: ApplicationFormComponent,
    resolve: { dbData: ApplicationResolver },
    data: { edit: true, preview: true },
  },
  {
    path: "waiting-payment",
    component: ApplicationWaitingPaymentComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "tax-free",
    component: ApplicationTaxFreeOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "for-check",
    component: ApplicationForCheckComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "for-signing",
    component: ApplicationForSigningComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal],
      },
    },
  },
  {
    path: "for-signing-by-judge",
    component: ApplicationForSiningByJudgeComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "bulletin-selection",
    component: ApplicationBulletinsSelectionComponent,
  },
  {
    path: "search",
    component: ApplicationSearchOverviewComponent,
  },
  {
    path: "search/preview/:ID",
    component: ApplicationSearchFormComponent,
    resolve: { dbData: ApplicationSearchResolver },
    data: { edit: true, preview: true },
  },
  {
    path: "",
    redirectTo: "applications",
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
export class ApplicationRoutingModule {}
