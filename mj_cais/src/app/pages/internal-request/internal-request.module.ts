import { NgModule } from "@angular/core";
import { InternalRequestRoutingModule } from "./internal-request-routing.module";
import { InternalRequestFormComponent } from "./internal-request-form/internal-request-form.component";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { InternalRequestBoxOverViewComponent } from "./internal-request-box-over-view/internal-request-box-over-view.component";
import { InternalRequestDraftOvverviewComponent } from "./internal-request-box-over-view/tabs/internal-request-draft-ovverview/internal-request-draft-ovverview.component";
import { InternalRequestInboxOverviewComponent } from "./internal-request-box-over-view/tabs/internal-request-inbox-overview/internal-request-inbox-overview.component";
import { InternalRequestOutboxOverviewComponent } from "./internal-request-box-over-view/tabs/internal-request-outbox-overview/internal-request-outbox-overview.component";
import { NbTooltipModule } from "@nebular/theme";
import { SelectPidDialogComponent } from "./internal-request-form/dialogs/select-pid-dialog/select-pid-dialog.component";
import { InternalRequestForJudgeOverviewComponent } from './internal-request-for-judge-overview/internal-request-for-judge-overview.component';

@NgModule({
  declarations: [
    InternalRequestFormComponent,
    InternalRequestBoxOverViewComponent,
    InternalRequestDraftOvverviewComponent,
    InternalRequestInboxOverviewComponent,
    InternalRequestOutboxOverviewComponent,
    SelectPidDialogComponent,
    InternalRequestForJudgeOverviewComponent,
  ],
  imports: [
    CoreComponentModule,
    InternalRequestRoutingModule,
    NbTooltipModule,
  ],
})
export class InternalRequestModule {}
