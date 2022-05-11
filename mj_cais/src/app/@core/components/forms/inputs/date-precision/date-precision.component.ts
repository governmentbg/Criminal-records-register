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
  @Input() label: string;

  public items: BaseNomenclatureModel[] = DatePrecisionConstants.allData;
  public showFullDate: boolean = false;
  public showYearAndMonth: boolean = false;
  public showYear: boolean = false;

  public dateValue: Date;

  ngOnInit() {
    // todo: maping from server
    // add validation
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

  onDatePercisionChange(value: string) {
    this.showFullDate = value == DatePrecisionConstants.fullDate.id;
    this.showYearAndMonth = value == DatePrecisionConstants.yearAndMonth.id;
    this.showYear = value == DatePrecisionConstants.year.id;
  }
}
