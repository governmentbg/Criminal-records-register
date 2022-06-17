import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { RoleNameEnum } from '../../@core/constants/role-name.enum';
import { NotFoundComponent } from '../miscellaneous/not-found/not-found.component';
import { EcrisTcnOverviewComponent } from './ecris-tcn-overview/ecris-tcn-overview.component';

const routes: Routes = [
  {   
    path: "",
    component: EcrisTcnOverviewComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: [RoleNameEnum.CentralAuth],
      },
    },
  },
  {
    path: "",
    redirectTo: "ecris-tcn",
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
export class EcrisTcnRoutingModule { }
