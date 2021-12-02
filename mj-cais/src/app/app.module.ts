import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { IgxNavbarModule } from '@infragistics/igniteui-angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    IgxNavbarModule,
   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
