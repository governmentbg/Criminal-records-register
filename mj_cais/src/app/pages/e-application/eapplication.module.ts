import { NgModule } from '@angular/core';
import { EApplicationRoutingModule } from './eapplication-routing.module';
import { SharedModule } from '../../shared.module';
import { EApplicationCheckPaymentOverviewComponent } from './e-application-overview/eapplication-check-payment-overview/eapplication-check-payment-overview.component';
import { EApplicationCheckTaxFreeOverviewComponent } from './e-application-overview/eapplication-check-tax-free-overview/eapplication-check-tax-free-overview.component';

@NgModule({
  declarations: [
    EApplicationCheckPaymentOverviewComponent,
    EApplicationCheckTaxFreeOverviewComponent
  ],
  imports: [
    SharedModule,
    EApplicationRoutingModule
  ]
})
export class EApplicationModule { }
