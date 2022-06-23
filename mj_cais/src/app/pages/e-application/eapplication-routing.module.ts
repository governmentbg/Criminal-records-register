import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
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
