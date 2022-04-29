import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseNomenclatureModel } from "../../../../models/nomenclature/base-nomenclature.model";
import { DateFormatService } from "../../../../services/common/date-format.service";
import { FormUtils } from "../../../../utils/form.utils";
import { DatePrecisionConstants } from "./_models/date-precision.constants";
import { DatePrecisionModelForm } from "./_models/date-precision.form";

@Component({
  selector: "cais-date-precision",
  templateUrl: "./date-precision.component.html",
  styleUrls: ["./date-precision.component.scss"],
})
export class DatePrecisionComponent implements OnInit {
  constructor(
    public formUtils: FormUtils,
    private dateService: DateFormatService
  ) {}

  public inputHasBeenInteracted: boolean;

  @ViewChild("monthPicker") monthPicker;

  @Input() formModel: DatePrecisionModelForm;
  @Input() parentGroup: FormGroup;

  public items: BaseNomenclatureModel[] = DatePrecisionConstants.allData;
  public showFullDate: boolean = false;
  public showYearAndMonth: boolean = false;
  public showYear: boolean = false;
  public dateValue: Date;
  public showMonthPicker: boolean = false;
  public yearMonthDisplayInput: FormControl;
  public yearDisplayInput: FormControl;

  ngOnInit() {
    this.yearMonthDisplayInput = new FormControl();
    this.yearDisplayInput = new FormControl(null, [
      Validators.maxLength(4),
      Validators.minLength(4),
    ]);
  }

  onDatePercisionChange(value: string) {
    this.showFullDate = value == DatePrecisionConstants.fullDate.id;
    this.showYearAndMonth = value == DatePrecisionConstants.yearAndMonth.id;
    this.showYear = value == DatePrecisionConstants.year.id;
    if (this.showFullDate) {
    }
  }

  public onSelection(selectedDate) {
    this.showMonthPicker = false;
    let dateFormated = this.dateService.displayDate(selectedDate);
    this.formModel.date.patchValue(selectedDate);
    this.yearMonthDisplayInput.patchValue(dateFormated);
  }

  public onClick(evt) {
    this.showMonthPicker = true;
  }

  public onFocusout(evt) {
    if (evt.relatedTarget) {
      let isClickedNext = evt.relatedTarget.classList.contains(
        "igx-calendar-picker__next"
      );
      let isClickedPrev = evt.relatedTarget.classList.contains(
        "igx-calendar-picker__prev"
      );
      let isClickedInMonth = evt.relatedTarget.hasAttribute("ng-reflect-date");
      let isClickedInYear = evt.relatedTarget.classList.contains(
        "igx-calendar-picker__date"
      );
      this.showMonthPicker =
        isClickedNext || isClickedInMonth || isClickedInYear || isClickedPrev;
      return;
    }

    this.showMonthPicker = false;
  }

  public onFocusoutYear(evt) {
    let year = this.yearDisplayInput.value;
    let date = new Date(year, 1, 1);
    this.formModel.date.patchValue(date);
  }
}
