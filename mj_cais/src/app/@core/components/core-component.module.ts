import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ConfirmDialogComponent } from "./dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { AddressFormComponent } from "./forms/address-form/address-form.component";
import { CountryDialogComponent } from "./forms/address-form/dialog/country-dialog/country-dialog.component";
import { CardHeaderComponent } from "./forms/card-header/card-header.component";
import { AutocompleteComponent } from "./forms/inputs/autocomplete/autocomplete.component";
import { CheckboxGroupComponent } from "./forms/inputs/checkbox-group/checkbox-group.component";
import { DatePrecisionComponent } from "./forms/inputs/date-precision/date-precision.component";
import { InputComponent } from "./forms/inputs/input/input.component";
import { LookupComponent } from "./forms/inputs/lookup/lookup.component";
import { MultipleChooseComponent } from "./forms/inputs/multiple-choose/multiple-choose.component";
import { RadioGroupComponent } from "./forms/inputs/radio-group/radio-group.component";
import { PersonAliasFormComponent } from "./forms/person-form/person-alias-form/person-alias-form.component";
import { PersonFormComponent } from "./forms/person-form/person-form.component";
import { CaisGridPagerComponent } from "./grid/cais-grid-pager/cais-grid-pager.component";
import { GridWithTransactionsComponent } from "./grid/grid-with-transactions.component";
import { ValidationMessageComponent } from "./validation-message/validation-message.component";
import { NgSelectModule } from "@ng-select/ng-select";
import { NbAlertModule, NbCardModule } from "@nebular/theme";
import { SharedModule } from "../../shared.module";
import { ConfirmTemplateDialogComponent } from "./dialogs/confirm-template-dialog/confirm-template-dialog.component";
import { BulletinPersonInfoComponent } from "./shared/bulletin-person-info/bulletin-person-info.component";
import { CancelDialogComponent } from './dialogs/cancel-dialog/cancel-dialog.component';
import { EmptyComponent } from './empty/empty.component';
import { EncapsulatedHtmlComponent } from './encapsulated-html/encapsulated-html.component';
import { PersonSearchDialogComponent } from './dialogs/person-search-dialog/person-search-dialog.component';
import { PersonSearchFormComponent } from './forms/person-search-form/person-search-form.component';
import { RouterModule } from "@angular/router";
import { ErrorDialogComponent } from './dialogs/error-dialog/error-dialog.component';

const COMPONENTS = [
  CardHeaderComponent,
  InputComponent,
  ValidationMessageComponent,
  AutocompleteComponent,
  CheckboxGroupComponent,
  RadioGroupComponent,
  GridWithTransactionsComponent,
  ConfirmDialogComponent,
  CaisGridPagerComponent,
  LookupComponent,
  MultipleChooseComponent,
  AddressFormComponent,
  CountryDialogComponent,
  DatePrecisionComponent,
  PersonFormComponent,
  PersonAliasFormComponent,
  ConfirmTemplateDialogComponent,
  BulletinPersonInfoComponent,
  CancelDialogComponent,
  EmptyComponent,
  EncapsulatedHtmlComponent,
  PersonSearchDialogComponent,
  PersonSearchFormComponent,
  ErrorDialogComponent
];

@NgModule({
  declarations: [...COMPONENTS],
  exports: [...COMPONENTS, SharedModule],
  imports: [
    CommonModule,
    SharedModule,
    NgSelectModule,
    NbCardModule,
    RouterModule
  ],
})
export class CoreComponentModule {}
