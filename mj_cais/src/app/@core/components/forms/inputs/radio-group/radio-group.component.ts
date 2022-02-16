import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { BaseNomenclatureModel } from "../../../../models/nomenclature/base-nomenclature.model";
import { FormUtils } from "../../../../utils/form.utils";

@Component({
  selector: "cais-radio-group",
  templateUrl: "./radio-group.component.html",
  styleUrls: ["./radio-group.component.scss"],
})
export class RadioGroupComponent implements OnInit {
  @Input() label: string;
  @Input() inputFormControl: FormControl;
  @Input() inputFormGroup: FormGroup;
  @Input() inputFormName: string;
  @Input() items: BaseNomenclatureModel[];

  @Output() onChange: EventEmitter<BaseNomenclatureModel> =
    new EventEmitter<BaseNomenclatureModel>();

  constructor(public formUtils: FormUtils) {}

  ngOnInit(): void {}

  public setInvalidContainer(): string {
    return this.inputFormControl.invalid &&
      (this.inputFormControl.touched || this.inputFormControl.dirty)
      ? "ng-invalid"
      : "";
  }

  onValueChanged(value) {
    let item = this.items.find((item) => item.id == value);
    this.onChange.emit(item);
  }
}
