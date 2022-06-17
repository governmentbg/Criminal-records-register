import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { IsinDataPreviewFormComponent } from "./isin-data-form/isin-data-preview-form/isin-data-preview-form.component";
import { IsinDataSelectBulletinFormComponent } from "./isin-data-form/isin-data-select-bulletin-form/isin-data-select-bulletin-form.component";
import { IsinIdentifiedOverviewComponent } from "./isin-data-overview/isin-identified-overview/isin-identified-overview.component";
import { IsinNewOverviewComponent } from "./isin-data-overview/isin-new-overview/isin-new-overview.component";

const routes: Routes = [
  {
    path: "new",
    component: IsinNewOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "identified",
    component: IsinIdentifiedOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "select-bulletin/:ID",
    component: IsinDataSelectBulletinFormComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth, RoleNameEnum.Normal,RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "preview/:ID",
    component: IsinDataPreviewFormComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth, RoleNameEnum.Normal,RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "",
    redirectTo: "new",
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
export class IsinRoutingModule {}
