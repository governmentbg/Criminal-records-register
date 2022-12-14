import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NgxPermissionsGuard } from "ngx-permissions";
import { PersonSearchFormComponent } from "../../@core/components/forms/person-search-form/person-search-form.component";
import { RoleNameEnum } from "../../@core/constants/role-name.enum";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { PersonDetailsFormComponent } from "./person-details-form/person-details-form.component";
import { PersonDetailsResolver } from "./person-details-form/_data/person-details.resolver";
import { PersonRemindFormComponent } from "./person-remind-form/person-remind-form.component";

const routes: Routes = [
  {
    path: "",
    component: PersonSearchFormComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "preview/:ID",
    component: PersonDetailsFormComponent,
    resolve: { dbData: PersonDetailsResolver },
    canActivate: [NgxPermissionsGuard],
    data: {
      edit: true,
      preview: true,
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "remind/:ID",
    component: PersonRemindFormComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.Normal, RoleNameEnum.Judge],
      },
    },
  },
  {
    path: "",
    redirectTo: "people",
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
export class PersonRoutingModule {}
