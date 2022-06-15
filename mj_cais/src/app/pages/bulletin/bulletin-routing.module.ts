import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../../@core/services/common/guard.service";
import { BulletinEventsOverviewComponent } from "../bulletin-events/bulletin-events-overview/bulletin-events-overview.component";
import { NotFoundComponent } from "../miscellaneous/not-found/not-found.component";
import { BulletinFormComponent } from "./bulletin-form/bulletin-form.component";
import { BulletinResolver } from "./bulletin-form/_data/bulletin.resolver";
import { BulletinActiveOverviewComponent } from "./bulletin-overview/bulletin-active-overview/bulletin-active-overview.component";
import { BulletinForDestructionOverviewComponent } from "./bulletin-overview/bulletin-fordestruction-overview/bulletin-fordestruction-overview.component";
import { BulletinForRehabilitationOverviewComponent } from "./bulletin-overview/bulletin-forrehabilitation-overview/bulletin-forrehabilitation-overview.component";
import { BulletinNewEissOverviewComponent } from "./bulletin-overview/bulletin-neweiss-overview/bulletin-neweiss-overview.component";
import { BulletinNewOfficeOverviewComponent } from "./bulletin-overview/bulletin-newoffice-overview/bulletin-newoffice-overview.component";

const routes: Routes = [
  {
    path: "active",
    component: BulletinActiveOverviewComponent,
    // canActivate: [AuthGuard],
  },
  {
    path: "new-office",
    component: BulletinNewOfficeOverviewComponent,
    // canActivate: [AuthGuard],
  },
  {
    path: "new-eiss",
    component: BulletinNewEissOverviewComponent,
    // canActivate: [AuthGuard],
  },
  {
    path: "for-destruction",
    component: BulletinForDestructionOverviewComponent,
    // canActivate: [AuthGuard],
  },
  {
    path: "for-rehabilitation",
    component: BulletinForRehabilitationOverviewComponent,
  },
  {
    path: "create",
    component: BulletinFormComponent,
    resolve: { dbData: BulletinResolver },
    // canActivate: [AuthGuard],
  },
  {
    path: "edit/:ID",
    component: BulletinFormComponent,
    resolve: { dbData: BulletinResolver },
    data: { edit: true },
    // canActivate: [AuthGuard],
  },
  {
    path: "preview/:ID",
    component: BulletinFormComponent,
    resolve: { dbData: BulletinResolver },
    data: { edit: true, preview: true },
    // canActivate: [AuthGuard],
  },
  {
    path: "events",
    component: BulletinEventsOverviewComponent,
  },
  {
    path: "",
    redirectTo: "bulletins",
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
export class BulletinRoutingModule {}
