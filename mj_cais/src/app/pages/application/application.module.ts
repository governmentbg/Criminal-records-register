import { NgModule } from "@angular/core";
import { ApplicationRoutingModule } from "./application-routing.module";
import { ApplicationCertificateResultComponent } from "./application-form/tabs/application-certificate-result/application-certificate-result.component";
import { ApplicationDocumentFormComponent } from "./application-form/tabs/application-document-form/application-document-form.component";
import { ApplicationStatusHistoryComponent } from "./application-form/tabs/application-status-history/application-status-history.component";
import { ApplicationBulletinsSelectionComponent } from "./application-overview/application-bulletins-selection/application-bulletins-selection.component";
import { ApplicationForCheckComponent } from "./application-overview/application-for-check/application-for-check.component";
import { ApplicationForSigningComponent } from "./application-overview/application-for-signing/application-for-signing.component";
import { ApplicationForSiningByJudgeComponent } from "./application-overview/application-for-sining-by-judge/application-for-sining-by-judge.component";
import { ApplicationNewOverviewComponent } from "./application-overview/application-new-overview/application-new-overview.component";
import { ApplicationTaxFreeOverviewComponent } from "./application-overview/application-tax-free-overview/application-tax-free-overview.component";
import { ApplicationWaitingPaymentComponent } from "./application-overview/application-waiting-payment/application-waiting-payment.component";
import { CoreComponentModule } from "../../@core/components/core-component.module";
import { ApplicationFormComponent } from "./application-form/application-form.component";

@NgModule({
  declarations: [
    ApplicationFormComponent,
    ApplicationNewOverviewComponent,
    ApplicationWaitingPaymentComponent,
    ApplicationTaxFreeOverviewComponent,
    ApplicationForCheckComponent,
    ApplicationForSigningComponent,
    ApplicationForSiningByJudgeComponent,
    ApplicationStatusHistoryComponent,
    ApplicationCertificateResultComponent,
    ApplicationBulletinsSelectionComponent,
    ApplicationDocumentFormComponent,
    
  ],
  imports: [CoreComponentModule, ApplicationRoutingModule],
})
export class ApplicationModule {}
