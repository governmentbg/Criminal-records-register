import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { NotFoundComponent } from '../miscellaneous/not-found/not-found.component';
import { UsersCitizenOverviewComponent } from './users-citizen-overview/users-citizen-overview.component';

const routes: Routes = [
  {
    path: "",
    component: UsersCitizenOverviewComponent,
   canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ["Admin", "GlobalAdmin"],
      },
    },
  },
  {
    path: "",
    redirectTo: "users-public",
    pathMatch: "full",
  },
  {
    path: "**",
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserPublicRoutingModule { }
