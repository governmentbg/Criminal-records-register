import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { BaseNomenclatureModel } from "../../../../models/nomenclature/base-nomenclature.model";
import { FormUtils } from "../../../../utils/form.utils";
import { DatePrecisionConstants } from "./_models/date-precision.constants";
import { DatePrecisionModelForm } from "./_models/date-precision.form";

@Component({
  selector: "cais-date-precision",
  templateUrl: "./date-precision.component.html",
  styleUrls: ["./date-precision.component.scss"],
})
export class DatePrecisionComponent implements OnInit {
  constructor(public formUtils: FormUtils) {}

  public inputHasBeenInteracted: boolean;

  @ViewChild("monthPicker") monthPicker;

  @Input() formModel: DatePrecisionModelForm;
  @Input() parentGroup: FormControl;
  @Input() label: string;
  @Input() hasTime: boolean = false;
  @Input() isRequired: boolean = false;

  public items: BaseNomenclatureModel[] = DatePrecisionConstants.allData;
  public showFullDate: boolean = false;
  public showYearAndMonth: boolean = false;
  public showYear: boolean = false;

  public dateValue: Date;
  public dateType: string = "date";

  ngOnInit() {
    this.dateType = this.hasTime ? "datetime" : "date";
    this.onDatePercisionChange(this.formModel.precision.value);
  }

  public setInvalidContainer(inputFormControl: FormControl): string {
    return inputFormControl.invalid &&
      (inputFormControl.touched || inputFormControl.dirty)
      ? "ng-invalid"
      : "";
  }

  public validationCss(inputFormControl: FormControl): string {
    return inputFormControl.invalid &&
      (inputFormControl.touched || inputFormControl.dirty)
      ? "status-danger"
      : "";
  }

  public onDialogOpen() {
    this.ngOnInit();
  }

  onDatePercisionChange(value: string) {
    this.showFullDate = value == DatePrecisionConstants.fullDate.id;
    this.showYearAndMonth = value == DatePrecisionConstants.yearAndMonth.id;
    this.showYear = value == DatePrecisionConstants.year.id;

    if (this.isRequired) {
      this.updateRequired();
    }

    if (this.formModel.date.value && this.showYearAndMonth) {
      let currentDate = new Date(this.formModel.date.value);
      let month = currentDate.getMonth();
      this.formModel.month.patchValue(month);
      let year = currentDate.getFullYear();
      this.formModel.year.patchValue(year);
      let newDate = new Date(year, month + 1, 1, 0, 0, 0, 0);
      this.formModel.date.patchValue(newDate);
    } else if (this.formModel.date.value && this.showYear) {
      let currentDate = new Date(this.formModel.date.value);
      let year = currentDate.getFullYear();
      this.formModel.year.patchValue(year);
      let newDate = new Date(year, 0, 1, 0, 0, 0, 0);
      this.formModel.date.patchValue(newDate);
    }
  }

  onInputForDateChange() {
    let month = 0;
    if (this.formModel.month.value) {
      month = this.formModel.month.value;
    }

    let year = this.formModel.year.value;
    let date = new Date(year, month, 1, 0, 0, 0, 0);
    this.formModel.date.patchValue(date);
  }

  private updateRequired() {
    this.formModel.date.clearValidators();
    this.formModel.year.clearValidators();
    this.formModel.month.clearValidators();
    if (this.showFullDate) {
      this.formModel.date.setValidators(Validators.required);
    } else if (this.showYearAndMonth) {
      this.formModel.year.setValidators([
        Validators.required,
        Validators.maxLength(4),
        Validators.minLength(4),
      ]);
      this.formModel.month.setValidators([
        Validators.required,
        Validators.maxLength(2),
        Validators.minLength(1),
      ]);
    } else if (this.showYear) {
      this.formModel.year.setValidators([
        Validators.required,
        Validators.maxLength(4),
        Validators.minLength(4),
      ]);
    }

    this.formModel.date.updateValueAndValidity();
    this.formModel.year.updateValueAndValidity();
    this.formModel.month.updateValueAndValidity();
  }
}
