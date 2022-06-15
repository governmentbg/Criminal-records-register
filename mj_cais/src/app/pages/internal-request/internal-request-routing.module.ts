import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { InternalRequestFormComponent } from "./internal-request-form/internal-request-form.component";
import { InternalRequestResolver } from "./internal-request-form/_data/internal-request.resolver";
import { InternalRequestOverviewComponent } from "./internal-request-overview/internal-request-overview.component";

const routes: Routes = [
  {
    path: "",
    component: InternalRequestOverviewComponent,
  },
  {
    path: ":ID", // this id is bulletin id
    component: InternalRequestOverviewComponent,
  },
  {
    path: "create/:ID",
    component: InternalRequestFormComponent,
    resolve: { dbData: InternalRequestResolver },
  },
  {
    path: "edit/:ID",
    component: InternalRequestFormComponent,
    resolve: { dbData: InternalRequestResolver },
    data: { edit: true },
  },
  {
    path: "preview/:ID",
    component: InternalRequestFormComponent,
    resolve: { dbData: InternalRequestResolver },
    data: { edit: true, preview: true },
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
