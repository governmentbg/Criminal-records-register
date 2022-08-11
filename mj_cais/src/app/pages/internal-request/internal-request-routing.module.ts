import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { InternalRequestBoxOverViewComponent } from "./internal-request-box-over-view/internal-request-box-over-view.component";
import { InternalRequestDraftOvverviewComponent } from "./internal-request-box-over-view/tabs/internal-request-draft-ovverview/internal-request-draft-ovverview.component";
import { InternalRequestFormComponent } from "./internal-request-form/internal-request-form.component";
import { InternalRequestResolver } from "./internal-request-form/_data/internal-request.resolver";
import { InternalRequestOverviewComponent } from "./internal-request-overview/internal-request-overview.component";

const routes: Routes = [
  {
    path: "",
    component: InternalRequestOverviewComponent,
  },
  {
    path: "box",
    component: InternalRequestBoxOverViewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "draft",
    component: InternalRequestDraftOvverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "create",
    component: InternalRequestFormComponent,
    resolve: { dbData: InternalRequestResolver },
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "edit/:ID",
    component: InternalRequestFormComponent,
    resolve: { dbData: InternalRequestResolver },
    data: {
      edit: true,
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "preview/:ID",
    component: InternalRequestFormComponent,
    resolve: { dbData: InternalRequestResolver },
    data: {
      edit: true,
      preview: true,
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "",
    redirectTo: "internal-requests",
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
export class InternalRequestRoutingModule {}
