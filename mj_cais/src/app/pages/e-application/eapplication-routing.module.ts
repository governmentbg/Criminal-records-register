import { EApplicationResolver } from "./e-application-form/_data/eapplication.resolver";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { EapplicationCheckPaymentFormComponent } from "./e-application-form/eapplication-check-payment-form/eapplication-check-payment-form.component";
import { EApplicationCheckPaymentOverviewComponent } from "./e-application-overview/eapplication-check-payment-overview/eapplication-check-payment-overview.component";
import { EApplicationCheckTaxFreeOverviewComponent } from "./e-application-overview/eapplication-check-tax-free-overview/eapplication-check-tax-free-overview.component";

const routes: Routes = [
  {
    path: "check-payment",
    component: EApplicationCheckPaymentOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "check-payment/preview/:ID",
    component: EapplicationCheckPaymentFormComponent,
    resolve: { dbData: EApplicationResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      edit: true,
      preview: true,
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "check-tax-free",
    component: EApplicationCheckTaxFreeOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "",
    redirectTo: "check-payment",
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
export class EApplicationRoutingModule {}
