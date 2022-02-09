import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { APP_INITIALIZER, LOCALE_ID, NgModule } from "@angular/core";
import { HttpClient, HttpClientModule } from "@angular/common/http";
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
import { IgxExcelExporterService } from "@infragistics/igniteui-angular";
import { ConfigurationService } from "@tl/tl-common";
import { forkJoin } from "rxjs";
// import { CustomToastrService } from "./@core/services/common/custom-toastr.service";
import { TranslateLoader, TranslateModule } from "@ngx-translate/core";
import { Observable } from "rxjs";
import { of } from "rxjs";
import { environment } from "../environments/environment";
import { ServiceWorkerModule } from "@angular/service-worker";
import { EditorModule } from "@tinymce/tinymce-angular";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";

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
  ],
  exports: [TranslateModule],
  bootstrap: [AppComponent],
  providers: [
    IgxExcelExporterService,
    // CustomToastrService,
    {
      provide: APP_INITIALIZER,
      useFactory: configureApp,
      deps: [ConfigurationService],
      multi: true,
    },
    { provide: LOCALE_ID, useValue: "bg" },
  ],
})
export class AppModule {}
