import { NgModule } from '@angular/core';
import { EApplicationRoutingModule } from './eapplication-routing.module';
import { SharedModule } from '../../shared.module';
import { EApplicationCheckPaymentOverviewComponent } from './e-application-overview/eapplication-check-payment-overview/eapplication-check-payment-overview.component';
import { EApplicationCheckTaxFreeOverviewComponent } from './e-application-overview/eapplication-check-tax-free-overview/eapplication-check-tax-free-overview.component';
import { CoreComponentModule } from '../../@core/components/core-component.module';
import { EapplicationCheckPaymentFormComponent } from './e-application-form/eapplication-check-payment-form/eapplication-check-payment-form.component';
import { EApplicationCertificateResultComponent } from './e-application-form/eapplication-check-payment-form/tabs/e-application-certificate-result/e-application-certificate-result.component';
import { EApplicationStatusHistoryComponent } from './e-application-form/eapplication-check-payment-form/tabs/e-application-status-history/e-application-status-history.component';

@NgModule({
  declarations: [
    EApplicationCheckPaymentOverviewComponent,
    EApplicationCheckTaxFreeOverviewComponent,
    EapplicationCheckPaymentFormComponent,
    EApplicationCertificateResultComponent,
    EApplicationStatusHistoryComponent
  ],
  imports: [
    CoreComponentModule,
    EApplicationRoutingModule
  ]
})
export class EApplicationModule { }
