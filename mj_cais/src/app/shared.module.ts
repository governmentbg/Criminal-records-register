import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import {
  IgxActionStripModule,
  IgxCalendarModule,
  IgxComboModule,
  IgxDialogModule,
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
import { TlCommonModule } from "@tl/tl-common";
import { NgxPermissionsModule } from "ngx-permissions";
import { FileUploadModule } from "ng2-file-upload";
import { HammerModule } from "@angular/platform-browser";

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
    IgxDialogModule,
    FileUploadModule,
    HammerModule, 
    IgxCalendarModule
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
    IgxDialogModule,
    FileUploadModule,
    HammerModule, 
    IgxCalendarModule
  ],
})
export class SharedModule {}
