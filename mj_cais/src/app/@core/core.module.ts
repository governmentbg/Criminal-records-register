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
import { CountryDialogComponent } from './components/forms/address-form/dialog/country-dialog/country-dialog.component';
import { throwIfAlreadyLoaded } from "./module-import-guard";
import { DatePrecisionComponent } from './components/forms/inputs/date-precision/date-precision.component';
import { NgxSpinnerModule } from "ngx-spinner";

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

export class NbSimpleRoleProvider extends NbRoleProvider {
  getRole() {
    // here you could provide any role based on any auth flow
    return observableOf("guest");
  }
}

export const NB_CORE_PROVIDERS = [
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
  AddressFormComponent,
  CountryDialogComponent,
  DatePrecisionComponent 
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
    NgxMatNativeDateModule,
    NgxSpinnerModule 
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
