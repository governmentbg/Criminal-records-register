import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from '../miscellaneous/not-found/not-found.component';
import { IsinDataPreviewFormComponent } from './isin-data-form/isin-data-preview-form/isin-data-preview-form.component';
import { IsinDataSelectBulletinFormComponent } from './isin-data-form/isin-data-select-bulletin-form/isin-data-select-bulletin-form.component';
import { IsinIdentifiedOverviewComponent } from './isin-data-overview/isin-identified-overview/isin-identified-overview.component';
import { IsinNewOverviewComponent } from './isin-data-overview/isin-new-overview/isin-new-overview.component';

const routes: Routes = [
  {
    path: "new",
    component: IsinNewOverviewComponent,
  },
  {
    path: "identified",
    component: IsinIdentifiedOverviewComponent,
  },
  {
    path: "select-bulletin/:ID",
    component: IsinDataSelectBulletinFormComponent,
  },
  {
    path: "preview/:ID",
    component: IsinDataPreviewFormComponent,
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
  exports: [RouterModule]
})
export class IsinRoutingModule { }
