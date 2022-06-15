import { NgModule } from "@angular/core";
import { IsinRoutingModule } from "./isin-routing.module";
import { IsinBulletinOverviewComponent } from "./isin-data-form/grids/isin-bulletin-overview/isin-bulletin-overview.component";
import { IsinDataFormComponent } from "./isin-data-form/isin-data-form/isin-data-form.component";
import { IsinDataPreviewFormComponent } from "./isin-data-form/isin-data-preview-form/isin-data-preview-form.component";
import { IsinDataSelectBulletinFormComponent } from "./isin-data-form/isin-data-select-bulletin-form/isin-data-select-bulletin-form.component";
import { IsinIdentifiedOverviewComponent } from "./isin-data-overview/isin-identified-overview/isin-identified-overview.component";
import { IsinNewOverviewComponent } from "./isin-data-overview/isin-new-overview/isin-new-overview.component";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { BulletinIsinFormComponent } from "../bulletin/bulletin-form/tabs/bulletin-isin-form/bulletin-isin-form.component";

@NgModule({
  declarations: [
    IsinNewOverviewComponent,
    IsinBulletinOverviewComponent,
    IsinIdentifiedOverviewComponent,
    IsinDataSelectBulletinFormComponent,
    IsinDataPreviewFormComponent,
    IsinDataFormComponent,
    BulletinIsinFormComponent,
  ],
  imports: [CoreComponentModule, IsinRoutingModule],
})
export class IsinModule {}
