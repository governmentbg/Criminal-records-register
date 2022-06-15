import { NgModule } from "@angular/core";
import { FbbcRoutingModule } from "./fbbc-routing.module";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { FbbcFormComponent } from "./fbbc-form/fbbc-form.component";
import { FbbcDocumentFormComponent } from "./fbbc-form/grids/fbbc-document-form/fbbc-document-form.component";
import { FbbcActiveOverviewComponent } from "./fbbc-overview/fbbc-active-overview/fbbc-active-overview.component";
import { FbbcDestructedOverviewComponent } from "./fbbc-overview/fbbc-destructed-overview/fbbc-destructed-overview.component";
import { FbbcForDestructionOverviewComponent } from "./fbbc-overview/fbbc-fordestruction-overview/fbbc-fordestruction-overview.component";

@NgModule({
  declarations: [
    FbbcActiveOverviewComponent,
    FbbcFormComponent,
    FbbcDocumentFormComponent,
    FbbcForDestructionOverviewComponent,
    FbbcDestructedOverviewComponent,
  ],
  imports: [CoreComponentModule, FbbcRoutingModule],
})
export class FbbcModule {}
