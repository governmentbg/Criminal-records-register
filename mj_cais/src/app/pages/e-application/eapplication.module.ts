import { NgModule } from '@angular/core';
import { EApplicationRoutingModule } from './eapplication-routing.module';
import { SharedModule } from '../../shared.module';
import { EApplicationCheckPaymentOverviewComponent } from './e-application-overview/eapplication-check-payment-overview/eapplication-check-payment-overview.component';

@NgModule({
  declarations: [
    EApplicationCheckPaymentOverviewComponent
  ],
  imports: [
    SharedModule,
    EApplicationRoutingModule
  ]
})
export class EApplicationModule { }
