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
import { SearchByEgnErrorDialogComponent } from "./application-request/search-by-egn-error-dialog/search-by-egn-error-dialog.component";
import { SearchByEgnDialogComponent } from "./application-request/search-by-egn-dialog/search-by-egn-dialog.component";
import { ApplicationRequestComponent } from "./application-request/application-request.component";
import { ApplicationEWebRequestsComponent } from './application-form/tabs/application-e-web-requests/application-e-web-requests.component';
import { ApplicationCertificateDocumentResultComponent } from './application-form/tabs/application-certificate-document-result/application-certificate-document-result.component';
import { CancelDialogComponent } from './application-form/cancel-dialog/cancel-dialog.component';
import { ApplicationCertificateCanceledComponent } from './application-form/tabs/application-certificate-canceled/application-certificate-canceled.component';
import { ApplicationSearchOverviewComponent } from './application-overview/application-search-overview/application-search-overview.component';
import { ApplicationSearchFormComponent } from './application-form/application-search-form/application-search-form.component';

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
    SearchByEgnErrorDialogComponent,
    SearchByEgnDialogComponent,
    ApplicationRequestComponent,
    ApplicationEWebRequestsComponent,
    ApplicationCertificateDocumentResultComponent,
    CancelDialogComponent,
    ApplicationCertificateCanceledComponent,
    ApplicationSearchOverviewComponent,
    ApplicationSearchFormComponent
  ],
  imports: [CoreComponentModule, ApplicationRoutingModule],
})
export class ApplicationModule {}
