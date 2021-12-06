import { NgModule } from "@angular/core";
import { IgxLayoutModule } from "@infragistics/igniteui-angular";
import { TranslateModule } from "@ngx-translate/core";
import { HeaderComponent } from './header/header.component';

@NgModule({
  declarations: [
    HeaderComponent
  ],
  imports: [
    TranslateModule,
    IgxLayoutModule
  ],
  exports: [
    TranslateModule,
    HeaderComponent
  ]
})
export class SharedModule {}
