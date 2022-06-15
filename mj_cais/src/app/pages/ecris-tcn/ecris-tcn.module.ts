import { NgModule } from '@angular/core';
import { EcrisTcnRoutingModule } from './ecris-tcn-routing.module';
import { EcrisTcnOverviewComponent } from './ecris-tcn-overview/ecris-tcn-overview.component';
import { CoreComponentModule } from '../../@core/components/core-component.module';

@NgModule({
  declarations: [EcrisTcnOverviewComponent],
  imports: [
    CoreComponentModule,
    EcrisTcnRoutingModule
  ]
})
export class EcrisTcnModule { }
