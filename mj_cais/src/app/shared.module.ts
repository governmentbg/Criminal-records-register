import { HttpClient } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import {
  IgxActionStripModule,
  IgxComboModule,
  IgxGridModule,
  IgxTreeGridModule,
} from "@infragistics/igniteui-angular";
import {
  NbAutocompleteModule,
  NbButtonModule,
  NbCardModule,
  NbCheckboxModule,
  NbDatepickerModule,
  NbFormFieldModule,
  NbIconModule,
  NbInputModule,
  NbRadioModule,
  NbSelectModule,
} from "@nebular/theme";
import { TranslateLoader, TranslateModule } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { TlCommonModule } from "@tl/tl-common";
import { NgxPermissionsModule } from "ngx-permissions";

@NgModule({
  declarations: [],
  imports: [
    TlCommonModule,
    NbSelectModule,
    NbButtonModule,
    IgxGridModule,
    IgxTreeGridModule,
    NbRadioModule,
    NbIconModule,
    NbCardModule,
    NbInputModule,
    NbFormFieldModule,
    ReactiveFormsModule,
    NbDatepickerModule,
    NbAutocompleteModule,
    NbCheckboxModule,
    NgxPermissionsModule.forRoot(),
    IgxActionStripModule,
    IgxComboModule,
  ],
  exports: [
    TlCommonModule,
    NbSelectModule,
    NbButtonModule,
    IgxGridModule,
    IgxTreeGridModule,
    NbRadioModule,
    NbCardModule,
    NbInputModule,
    NbDatepickerModule,
    NbFormFieldModule,
    NbIconModule,
    ReactiveFormsModule,
    NbAutocompleteModule,
    NbCheckboxModule,
    NgxPermissionsModule,
    IgxActionStripModule,
    IgxComboModule,
  ],
})
export class SharedModule {}
