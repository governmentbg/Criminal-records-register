import { NgModule } from "@angular/core";
import { StatisticsRoutingModule } from "./statistics-routing.module";
import { BulletinStatisticsFormComponent } from "./bulletin-statistics-form/bulletin-statistics-form.component";
import { ApplicationStatisticsFormComponent } from "./application-statistics-form/application-statistics-form.component";
import { StatisticsFormComponent } from "./statistics-form/statistics-form.component";
import { DailyStatisticsFormComponent } from "./daily-statistics-form/daily-statistics-form.component";
// import {
//   IgxDoughnutChartModule,
//   IgxItemLegendModule,
//   IgxPieChartModule,
// } from "igniteui-angular-charts";
import { CoreComponentModule } from "../../@core/components/core-component.module";

@NgModule({
  declarations: [
    BulletinStatisticsFormComponent,
    ApplicationStatisticsFormComponent,
    StatisticsFormComponent,
    DailyStatisticsFormComponent,
  ],
  imports: [
    CoreComponentModule,
    StatisticsRoutingModule,
    // IgxItemLegendModule,
    // IgxDoughnutChartModule,
    // IgxPieChartModule,
  ],
})
export class StatisticsModule {}
