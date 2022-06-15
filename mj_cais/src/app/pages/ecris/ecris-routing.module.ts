import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { EcrisIdentificationFormComponent } from "./ecris-message-form/ecris-identification-form/ecris-identification-form.component";
import { EcrisIdentificationResolver } from "./ecris-message-form/ecris-identification-form/_data/ecris-identification.resolver";
import { EcrisReqWaitingFormComponent } from "./ecris-message-form/ecris-req-waiting-form/ecris-req-waiting-form.component";
import { EcrisReqWaitingResolver } from "./ecris-message-form/ecris-req-waiting-form/_data/ecris-req-waiting.resolver";
import { EcrisIdentificationOverviewComponent } from "./ecris-message-overivew/ecris-identification-overview/ecris-identification-overview.component";
import { EcrisReqWaitingOverviewComponent } from "./ecris-message-overivew/ecris-req-waiting-overview/ecris-req-waiting-overview.component";

const routes: Routes = [
  {
    path: "identification",
    component: EcrisIdentificationOverviewComponent,
  },
  {
    path: "identification/edit/:ID",
    component: EcrisIdentificationFormComponent,
    resolve: { dbData: EcrisIdentificationResolver },
    data: { edit: true },
  },
  {
    path: "identification/preview/:ID",
    component: EcrisIdentificationFormComponent,
    resolve: { dbData: EcrisIdentificationResolver },
    data: { edit: true, preview: true },
  },
  {
    path: "req-waiting",
    component: EcrisReqWaitingOverviewComponent,
  },
  {
    path: "req-waiting/create",
    component: EcrisReqWaitingFormComponent,
    resolve: { dbData: EcrisReqWaitingResolver },
  },
  {
    path: "req-waiting/edit/:ID",
    component: EcrisReqWaitingFormComponent,
    resolve: { dbData: EcrisReqWaitingResolver },
    data: { edit: true },
  },
  {
    path: "req-waiting/preview/:ID",
    component: EcrisReqWaitingFormComponent,
    resolve: { dbData: EcrisReqWaitingResolver },
    data: { edit: true, preview: true },
  },
  {
    path: "",
    redirectTo: "identification",
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
export class EcrisRoutingModule {}
