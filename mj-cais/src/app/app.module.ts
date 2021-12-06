import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { IgxButtonModule, IgxNavbarModule } from '@infragistics/igniteui-angular';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { AuthHttpConfigModule } from './auth/auth-http-config.module';
import { LoggerModule, NgxLoggerLevel } from 'ngx-logger';
import { NgxPermissionsModule } from 'ngx-permissions';

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  console.log('func loader app module');
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    IgxNavbarModule,
    HttpClientModule,
    SharedModule,
    AuthHttpConfigModule,
    LoggerModule.forRoot(
      {
        level: NgxLoggerLevel.DEBUG,
      }
    ),
    NgxPermissionsModule.forRoot(),
    TranslateModule.forRoot({
      defaultLanguage: 'bg',
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      },
      extend: true,
      isolate: false
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
