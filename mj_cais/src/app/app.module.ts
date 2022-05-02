import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { APP_INITIALIZER, LOCALE_ID, NgModule } from "@angular/core";
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { CoreModule } from "./@core/core.module";
import { LoggerModule, NgxLoggerLevel } from "ngx-logger";
import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";
import { ThemeModule } from "./@theme/theme.module";
// import { AuthModule } from "./@auth/auth.module";
import { registerLocaleData } from "@angular/common";
import localeBg from "@angular/common/locales/bg";
import {
  NbDatepickerModule,
  NbDialogModule,
  NbMenuModule,
  NbSidebarModule,
  NbToastrModule,
} from "@nebular/theme";
import { IgxExcelExporterService, IgxGridTransaction, IgxTransactionService } from "@infragistics/igniteui-angular";
import { ConfigurationService } from "@tl/tl-common";
import { forkJoin } from "rxjs";
import { CustomToastrService } from "./@core/services/common/custom-toastr.service";
import { TranslateLoader, TranslateModule } from "@ngx-translate/core";
import { Observable } from "rxjs";
import { of } from "rxjs";
import { environment } from "../environments/environment";
import { ServiceWorkerModule } from "@angular/service-worker";
import { EditorModule } from "@tinymce/tinymce-angular";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { RouterExtService } from "./@core/services/common/router-ext.service";
import { NbAuthJWTInterceptor, NbAuthModule, NbAuthOAuth2Token, NbAuthService, NbOAuth2AuthStrategy, NbOAuth2GrantType, NbOAuth2ResponseType, NB_AUTH_TOKEN_INTERCEPTOR_FILTER } from "@nebular/auth";
import { AuthGuard } from "./@core/services/common/guard.service";

function customReadConfiguration(): Observable<[any, any]> {
  this.serviceUrl = environment.serviceUrl;
  const config = of({});
  const authConfig = of({});
  return forkJoin([config, authConfig]);
}

export function configureApp(configurationService: ConfigurationService) {
  ConfigurationService.prototype.readConfiguration = customReadConfiguration;
  const setupConfiguration$ = configurationService.readConfiguration();

  return () => forkJoin([setupConfiguration$]).toPromise();
}

// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, "assets/i18n/", ".json");
}

registerLocaleData(localeBg);

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    EditorModule,
    LoggerModule.forRoot({
      level: NgxLoggerLevel.DEBUG,
    }),

    // AuthModule.forRoot(),

    NbSidebarModule.forRoot(),
    NbMenuModule.forRoot(),
    NbDatepickerModule.forRoot(),
    NbDialogModule.forRoot(),
    NbToastrModule.forRoot(),
    ThemeModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
      defaultLanguage: "bg",
    }),
    ServiceWorkerModule.register("ngsw-worker.js", {
      enabled: environment.production,
      // Register the ServiceWorker as soon as the app is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: "registerImmediately",
    }),
    NbAuthModule.forRoot({
      strategies: [
        NbOAuth2AuthStrategy.setup( {
          name: 'eauth',
          clientId: 'cais-angular'
        })
      ]
    })
  ],
  exports: [TranslateModule],
  bootstrap: [AppComponent],
  providers: [
    IgxExcelExporterService,
    CustomToastrService,
    RouterExtService,
    AuthGuard,
    {
      provide: APP_INITIALIZER,
      useFactory: configureApp,
      deps: [ConfigurationService],
      multi: true,
    },
    { provide: LOCALE_ID, useValue: "bg" },
    { provide: IgxGridTransaction, useClass: IgxTransactionService },
    { 
      provide: NB_AUTH_TOKEN_INTERCEPTOR_FILTER, 
      useValue: function (req: any) { 
        if (req.url === '/auth/connect/token'){
          return true;
        } else {
          return false;
        }
      }, 
    },
    { provide: HTTP_INTERCEPTORS, useClass: NbAuthJWTInterceptor, multi: true }
  ],
})
export class AppModule {
  constructor(
    authService: NbAuthService,
    oauthStrategy: NbOAuth2AuthStrategy
  ){
    oauthStrategy.setOptions(
      {
        name: 'eauth',
        clientId: 'cais-angular',
        clientSecret: '',
        authorize: {
          endpoint: '/auth/connect/authorize',
          responseType: NbOAuth2ResponseType.CODE,
          scope: 'openid profile offline_access caisapi',
          redirectUri: `${window.location.origin}/postlogin`
        },
        token: {
          endpoint: '/auth/connect/token',
          grantType: NbOAuth2GrantType.AUTHORIZATION_CODE,
          redirectUri: `${window.location.origin}/postlogin`,
          class: NbAuthOAuth2Token,
        },
        refresh: {
            endpoint: '/auth/connect/token',
            grantType: NbOAuth2GrantType.REFRESH_TOKEN
        }
      }
    );
  }
}
