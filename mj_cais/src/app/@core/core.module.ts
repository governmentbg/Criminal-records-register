import {
  ModuleWithProviders,
  NgModule,
  Optional,
  SkipSelf,
} from "@angular/core";
import { CommonModule } from "@angular/common";
import { NbAuthModule, NbDummyAuthStrategy } from "@nebular/auth";
import { NbSecurityModule, NbRoleProvider } from "@nebular/security";
import { of as observableOf } from "rxjs";
import { MatDatepickerModule } from "@angular/material/datepicker";
import {
  MatMomentDateModule,
  MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from "@angular/material-moment-adapter";
import { MatInputModule } from "@angular/material/input";
import {
  NgxMatDatetimePickerModule,
  NgxMatNativeDateModule,
} from "@angular-material-components/datetime-picker";

import { throwIfAlreadyLoaded } from "./module-import-guard";
import { UserData } from "./data/users";
import { ElectricityData } from "./data/electricity";
import { SmartTableData } from "./data/smart-table";
import { UserActivityData } from "./data/user-activity";
import { OrdersChartData } from "./data/orders-chart";
import { ProfitChartData } from "./data/profit-chart";
import { TrafficListData } from "./data/traffic-list";
import { EarningData } from "./data/earning";
import { OrdersProfitChartData } from "./data/orders-profit-chart";
import { TrafficBarData } from "./data/traffic-bar";
import { ProfitBarAnimationChartData } from "./data/profit-bar-animation-chart";
import { TemperatureHumidityData } from "./data/temperature-humidity";
import { SolarData } from "./data/solar";
import { TrafficChartData } from "./data/traffic-chart";
import { StatsBarData } from "./data/stats-bar";
import { CountryOrderData } from "./data/country-order";
import { StatsProgressBarData } from "./data/stats-progress-bar";
import { VisitorsAnalyticsData } from "./data/visitors-analytics";
import { SecurityCamerasData } from "./data/security-cameras";

import { UserService } from "./mock/users.service";
import { ElectricityService } from "./mock/electricity.service";
import { SmartTableService } from "./mock/smart-table.service";
import { UserActivityService } from "./mock/user-activity.service";
import { OrdersChartService } from "./mock/orders-chart.service";
import { ProfitChartService } from "./mock/profit-chart.service";
import { TrafficListService } from "./mock/traffic-list.service";
import { EarningService } from "./mock/earning.service";
import { OrdersProfitChartService } from "./mock/orders-profit-chart.service";
import { TrafficBarService } from "./mock/traffic-bar.service";
import { ProfitBarAnimationChartService } from "./mock/profit-bar-animation-chart.service";
import { TemperatureHumidityService } from "./mock/temperature-humidity.service";
import { SolarService } from "./mock/solar.service";
import { TrafficChartService } from "./mock/traffic-chart.service";
import { StatsBarService } from "./mock/stats-bar.service";
import { CountryOrderService } from "./mock/country-order.service";
import { StatsProgressBarService } from "./mock/stats-progress-bar.service";
import { VisitorsAnalyticsService } from "./mock/visitors-analytics.service";
import { SecurityCamerasService } from "./mock/security-cameras.service";
import { MockDataModule } from "./mock/mock-data.module";
import { CardHeaderComponent } from "./components/forms/card-header/card-header.component";
import { InputComponent } from "./components/forms/inputs/input/input.component";
import { ValidationMessageComponent } from "./components/validation-message/validation-message.component";
import { ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "../shared.module";
import { AutocompleteComponent } from "./components/forms/inputs/autocomplete/autocomplete.component";
import { CheckboxGroupComponent } from "./components/forms/inputs/checkbox-group/checkbox-group.component";
import { RadioGroupComponent } from "./components/forms/inputs/radio-group/radio-group.component";
import { NgSelectModule } from "@ng-select/ng-select";
import { GridWithTransactionsComponent } from "./components/grid/grid-with-transactions.component";
import { NbCardModule } from "@nebular/theme";
import { ConfirmDialogComponent } from "./components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CaisGridPagerComponent } from './components/grid/cais-grid-pager/cais-grid-pager.component';
import { LookupComponent } from './components/forms/inputs/lookup/lookup.component';
import { MultipleChooseComponent } from './components/forms/inputs/multiple-choose/multiple-choose.component';
import { AddressFormComponent } from './components/forms/address-form/address-form.component';

const socialLinks = [
  {
    url: "https://github.com/akveo/nebular",
    target: "_blank",
    icon: "github",
  },
  {
    url: "https://www.facebook.com/akveo/",
    target: "_blank",
    icon: "facebook",
  },
  {
    url: "https://twitter.com/akveo_inc",
    target: "_blank",
    icon: "twitter",
  },
];

const DATA_SERVICES = [
  { provide: UserData, useClass: UserService },
  { provide: ElectricityData, useClass: ElectricityService },
  { provide: SmartTableData, useClass: SmartTableService },
  { provide: UserActivityData, useClass: UserActivityService },
  { provide: OrdersChartData, useClass: OrdersChartService },
  { provide: ProfitChartData, useClass: ProfitChartService },
  { provide: TrafficListData, useClass: TrafficListService },
  { provide: EarningData, useClass: EarningService },
  { provide: OrdersProfitChartData, useClass: OrdersProfitChartService },
  { provide: TrafficBarData, useClass: TrafficBarService },
  {
    provide: ProfitBarAnimationChartData,
    useClass: ProfitBarAnimationChartService,
  },
  { provide: TemperatureHumidityData, useClass: TemperatureHumidityService },
  { provide: SolarData, useClass: SolarService },
  { provide: TrafficChartData, useClass: TrafficChartService },
  { provide: StatsBarData, useClass: StatsBarService },
  { provide: CountryOrderData, useClass: CountryOrderService },
  { provide: StatsProgressBarData, useClass: StatsProgressBarService },
  { provide: VisitorsAnalyticsData, useClass: VisitorsAnalyticsService },
  { provide: SecurityCamerasData, useClass: SecurityCamerasService },
];

export class NbSimpleRoleProvider extends NbRoleProvider {
  getRole() {
    // here you could provide any role based on any auth flow
    return observableOf("guest");
  }
}

export const NB_CORE_PROVIDERS = [
  ...MockDataModule.forRoot().providers,
  ...DATA_SERVICES,
  ...NbAuthModule.forRoot({
    strategies: [
      NbDummyAuthStrategy.setup({
        name: "email",
        delay: 3000,
      }),
    ],
    forms: {
      login: {
        socialLinks: socialLinks,
      },
      register: {
        socialLinks: socialLinks,
      },
    },
  }).providers,

  NbSecurityModule.forRoot({
    accessControl: {
      guest: {
        view: "*",
      },
      user: {
        parent: "guest",
        create: "*",
        edit: "*",
        remove: "*",
      },
    },
  }).providers,

  {
    provide: NbRoleProvider,
    useClass: NbSimpleRoleProvider,
  },
];

const COMPONENTS = [
  CardHeaderComponent,
  InputComponent,
  ValidationMessageComponent,
  AutocompleteComponent,
  CheckboxGroupComponent,
  RadioGroupComponent,
  GridWithTransactionsComponent,
  ConfirmDialogComponent,
  CaisGridPagerComponent,
  LookupComponent,
  MultipleChooseComponent,
  AddressFormComponent
];

@NgModule({
  imports: [
    ReactiveFormsModule,
    CommonModule,
    SharedModule,
    NgSelectModule,
    MatInputModule,
    MatDatepickerModule,
    MatMomentDateModule,
    NbCardModule,
    NgxMatDatetimePickerModule,
    NgxMatNativeDateModule
  ],
  declarations: [...COMPONENTS],
  providers: [
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
  ],
  exports: [NbAuthModule, SharedModule, NgSelectModule, ...COMPONENTS],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, "CoreModule");
  }

  static forRoot(): ModuleWithProviders<CoreModule> {
    return {
      ngModule: CoreModule,
      providers: [...NB_CORE_PROVIDERS],
    };
  }
}