import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { APP_INITIALIZER, LOCALE_ID, NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
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
  NbChatModule,
  NbWindowModule,
} from "@nebular/theme";
import { IgxExcelExporterService } from "@infragistics/igniteui-angular";
import { ConfigurationService } from "@tl/tl-common";
import { forkJoin } from "rxjs";
// import { CustomToastrService } from "./@core/services/common/custom-toastr.service";
import { TranslateModule } from "@ngx-translate/core";
import { Observable } from "rxjs";
import { of } from "rxjs";
import { environment } from "../environments/environment";
import { ServiceWorkerModule } from "@angular/service-worker";

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

registerLocaleData(localeBg);

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,

    LoggerModule.forRoot({
      level: NgxLoggerLevel.DEBUG,
    }),

    NbSidebarModule.forRoot(),
    NbMenuModule.forRoot(),
    NbDatepickerModule.forRoot(),
    NbDialogModule.forRoot(),
    NbWindowModule.forRoot(),
    NbToastrModule.forRoot(),
    NbChatModule.forRoot({
      messageGoogleMapKey: "AIzaSyA_wNuCzia92MAmdLRzmqitRGvCF7wCZPY",
    }),
    CoreModule.forRoot(),
    ThemeModule.forRoot(),

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
