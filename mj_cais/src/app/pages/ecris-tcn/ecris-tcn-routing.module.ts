import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from '../miscellaneous/not-found/not-found.component';
import { EcrisTcnOverviewComponent } from './ecris-tcn-overview/ecris-tcn-overview.component';

const routes: Routes = [
  {   
    path: "",
    component: EcrisTcnOverviewComponent,
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
