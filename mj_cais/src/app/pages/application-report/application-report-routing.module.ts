import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from '../miscellaneous/not-found/not-found.component';
import { ApplicationReportFormComponent } from './application-report-form/application-report-form.component';
import { ApplicationReportResolver } from './application-report-form/_data/application-report.resolver';

const routes: Routes = [
  {
    path: "create",
    component: ApplicationReportFormComponent,
    resolve: { dbData: ApplicationReportResolver },
  },
  {
    path: "edit/:ID",
    component: ApplicationReportFormComponent,
    resolve: { dbData: ApplicationReportResolver },
    data: { edit: true },
  },
  {
    path: "",
    redirectTo: "application-reports",
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
export class ApplicationReportRoutingModule { }
