import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import {
  IgxActionStripModule,
  IgxCalendarModule,
  IgxComboModule,
  IgxDatePickerModule,
  IgxDialogModule,
  IgxGridModule,
  IgxTimePickerModule,
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
  NbListModule,
  NbMenuModule,
  NbRadioModule,
  NbSelectModule,
  NbTabsetModule,
  NbToggleModule,
} from "@nebular/theme";
import { TlCommonModule } from "@tl/tl-common";
import { FileUploadModule } from "ng2-file-upload";
import { HammerModule } from "@angular/platform-browser";
import { ThemeModule } from "./@theme/theme.module";
import { MatMenuModule } from "@angular/material/menu";
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
    IgxActionStripModule,
    IgxComboModule,
    IgxDialogModule,
    FileUploadModule,
    HammerModule, 
    IgxCalendarModule,
    ThemeModule,
    NbMenuModule,
    NbTabsetModule,
    NbListModule,
    MatMenuModule,
    NbToggleModule,
    NgxPermissionsModule.forChild(),
    IgxDatePickerModule,
    IgxTimePickerModule
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
    IgxActionStripModule,
    IgxComboModule,
    IgxDialogModule,
    FileUploadModule,
    HammerModule, 
    IgxCalendarModule,
    ThemeModule,
    NbMenuModule,
    NbTabsetModule,
    NbListModule,
    MatMenuModule,
    NbToggleModule,
    NgxPermissionsModule,
    IgxDatePickerModule,
    IgxTimePickerModule
  ],
})
export class SharedModule {}
