import {
  Injectable,
  ModuleWithProviders,
  NgModule,
  Optional,
  SkipSelf,
} from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  NbAuthJWTToken,
  NbAuthModule,
  NbAuthOAuth2Token,
  NbAuthService,
  NbDummyAuthStrategy,
  NbOAuth2AuthStrategy,
} from "@nebular/auth";
import { NbSecurityModule, NbRoleProvider } from "@nebular/security";
import { map, Observable, of as observableOf, of, switchMap } from "rxjs";
import { MatDatepickerModule } from "@angular/material/datepicker";
import {
  MatMomentDateModule,
  MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from "@angular/material-moment-adapter";
import { MatInputModule } from "@angular/material/input";

import { CardHeaderComponent } from "./components/forms/card-header/card-header.component";
import { InputComponent } from "./components/forms/inputs/input/input.component";
import { ValidationMessageComponent } from "./components/validation-message/validation-message.component";
import { SharedModule } from "../shared.module";
import { AutocompleteComponent } from "./components/forms/inputs/autocomplete/autocomplete.component";
import { CheckboxGroupComponent } from "./components/forms/inputs/checkbox-group/checkbox-group.component";
import { RadioGroupComponent } from "./components/forms/inputs/radio-group/radio-group.component";
import { NgSelectModule } from "@ng-select/ng-select";
import { GridWithTransactionsComponent } from "./components/grid/grid-with-transactions.component";
import { NbCardModule } from "@nebular/theme";
import { ConfirmDialogComponent } from "./components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CaisGridPagerComponent } from "./components/grid/cais-grid-pager/cais-grid-pager.component";
import { LookupComponent } from "./components/forms/inputs/lookup/lookup.component";
import { MultipleChooseComponent } from "./components/forms/inputs/multiple-choose/multiple-choose.component";
import { AddressFormComponent } from "./components/forms/address-form/address-form.component";
import { CountryDialogComponent } from "./components/forms/address-form/dialog/country-dialog/country-dialog.component";
import { throwIfAlreadyLoaded } from "./module-import-guard";
import { DatePrecisionComponent } from "./components/forms/inputs/date-precision/date-precision.component";
import { HttpClient } from "@angular/common/http";
import { PersonFormComponent } from "./components/forms/person-form/person-form.component";
import { PersonAliasFormComponent } from "./components/forms/person-form/person-alias-form/person-alias-form.component";
import { ConfirmTemplateDialogComponent } from "./components/dialogs/confirm-template-dialog/confirm-template-dialog.component";

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

@Injectable()
export class NbSimpleRoleProvider extends NbRoleProvider {
  constructor(
    private authService: NbAuthService,
    private httpClient: HttpClient
  ) {
    super();
  }

  getRole(): Observable<string> {
    return this.authService.onTokenChange().pipe(
      switchMap((tkn) => {
        return tkn.isValid()
          ? this.httpClient.get("/auth/connect/userinfo")
          : of({ role: "guest" });
      }),
      map((data: any) => {
        return data?.role;
      })
    );
  }
}

export const NB_CORE_PROVIDERS = [
  ...NbAuthModule.forRoot({
    strategies: [
      NbOAuth2AuthStrategy.setup({
        name: "eauth",
        clientId: "cais-angular",
      }),
    ],
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

@NgModule({
  imports: [CommonModule],
  exports: [NbAuthModule],
  declarations: [],
  providers: [
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
  ],
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
