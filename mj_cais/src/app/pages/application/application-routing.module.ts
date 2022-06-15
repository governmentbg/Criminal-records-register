import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { ApplicationFormComponent } from "./application-form/application-form.component";
import { ApplicationResolver } from "./application-form/_data/application.resolver";
import { ApplicationBulletinsSelectionComponent } from "./application-overview/application-bulletins-selection/application-bulletins-selection.component";
import { ApplicationForCheckComponent } from "./application-overview/application-for-check/application-for-check.component";
import { ApplicationForSigningComponent } from "./application-overview/application-for-signing/application-for-signing.component";
import { ApplicationForSiningByJudgeComponent } from "./application-overview/application-for-sining-by-judge/application-for-sining-by-judge.component";
import { ApplicationNewOverviewComponent } from "./application-overview/application-new-overview/application-new-overview.component";
import { ApplicationTaxFreeOverviewComponent } from "./application-overview/application-tax-free-overview/application-tax-free-overview.component";
import { ApplicationWaitingPaymentComponent } from "./application-overview/application-waiting-payment/application-waiting-payment.component";
import { ApplicationRequestComponent } from "./application-request/application-request.component";

const routes: Routes = [
  {
    path: "",
    component: ApplicationNewOverviewComponent,
  },
  {
    path: "request",
    component: ApplicationRequestComponent,
    resolve: { dbData: ApplicationResolver },
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
  },
  {
    path: "tax-free",
    component: ApplicationTaxFreeOverviewComponent,
  },
  {
    path: "for-check",
    component: ApplicationForCheckComponent,
  },
  {
    path: "for-signing",
    component: ApplicationForSigningComponent,
  },
  {
    path: "for-signing-by-judge",
    component: ApplicationForSiningByJudgeComponent,
  },
  {
    path: "bulletin-selection",
    component: ApplicationBulletinsSelectionComponent,
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
