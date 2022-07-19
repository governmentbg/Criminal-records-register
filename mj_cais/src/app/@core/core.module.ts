import {
  Injectable,
  ModuleWithProviders,
  NgModule,
  Optional,
  SkipSelf,
} from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  NbAuthModule,
  NbAuthService,
  NbOAuth2AuthStrategy,
} from "@nebular/auth";
import { NbSecurityModule, NbRoleProvider } from "@nebular/security";
import { map, Observable, of as observableOf, of, switchMap } from "rxjs";
import { throwIfAlreadyLoaded } from "./module-import-guard";
import { HttpClient } from "@angular/common/http";

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
  // providers: [
  //   { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
  // ],
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
