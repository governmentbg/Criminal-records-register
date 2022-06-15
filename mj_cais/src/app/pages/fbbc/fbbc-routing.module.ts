import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { FbbcResolver } from "./fbbc-form/data/fbbc.resolver";
import { FbbcFormComponent } from "./fbbc-form/fbbc-form.component";
import { FbbcActiveOverviewComponent } from "./fbbc-overview/fbbc-active-overview/fbbc-active-overview.component";
import { FbbcDestructedOverviewComponent } from "./fbbc-overview/fbbc-destructed-overview/fbbc-destructed-overview.component";
import { FbbcForDestructionOverviewComponent } from "./fbbc-overview/fbbc-fordestruction-overview/fbbc-fordestruction-overview.component";

const routes: Routes = [
  {
    path: "",
    component: FbbcActiveOverviewComponent,
  },
  {
    path: "for-destruction",
    component: FbbcForDestructionOverviewComponent,
  },
  {
    path: "destructed",
    component: FbbcDestructedOverviewComponent,
  },
  {
    path: "create",
    component: FbbcFormComponent,
    resolve: { dbData: FbbcResolver },
  },
  {
    path: "edit/:ID",
    component: FbbcFormComponent,
    resolve: { dbData: FbbcResolver },
    data: { edit: true },
  },
  {
    path: "preview/:ID",
    component: FbbcFormComponent,
    resolve: { dbData: FbbcResolver },
    data: { edit: true, preview: true },
  },
  {
    path: "",
    redirectTo: "fbbcs",
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
export class FbbcRoutingModule {}
