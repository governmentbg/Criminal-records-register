import { NgModule } from '@angular/core';
import { EApplicationRoutingModule } from './eapplication-routing.module';
import { SharedModule } from '../../shared.module';
import { EApplicationCheckPaymentOverviewComponent } from './e-application-overview/eapplication-check-payment-overview/eapplication-check-payment-overview.component';
import { EApplicationCheckTaxFreeOverviewComponent } from './e-application-overview/eapplication-check-tax-free-overview/eapplication-check-tax-free-overview.component';
import { CoreComponentModule } from '../../@core/components/core-component.module';
import { EapplicationCheckPaymentFormComponent } from './e-application-form/eapplication-check-payment-form/eapplication-check-payment-form.component';

@NgModule({
  declarations: [
    EApplicationCheckPaymentOverviewComponent,
    EApplicationCheckTaxFreeOverviewComponent,
    EapplicationCheckPaymentFormComponent
  ],
  imports: [
    CoreComponentModule,
    EApplicationRoutingModule
  ]
})
export class EApplicationModule { }
