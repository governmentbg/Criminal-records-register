import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { BaseNomenclatureModel } from "../../../../models/nomenclature/base-nomenclature.model";
import { FormUtils } from "../../../../utils/form.utils";

@Component({
  selector: "cais-checkbox-group",
  templateUrl: "./checkbox-group.component.html",
  styleUrls: ["./checkbox-group.component.scss"],
})
export class CheckboxGroupComponent implements OnInit {
  @Input() label: string;
  @Input() inputFormControl: FormControl;
  @Input() inputFormGroup: FormGroup;
  @Input() inputFormName: string;
  @Input() items: BaseNomenclatureModel[];

  public isForPreview: boolean;

  @Output() onChange: EventEmitter<BaseNomenclatureModel> =
    new EventEmitter<BaseNomenclatureModel>();

  constructor(public formUtils: FormUtils, public route: ActivatedRoute) {}

  ngOnInit(): void {
    this.isForPreview = this.route.snapshot.data["preview"];
  }

  onValueChanged($event, value) {
    let currentSelectedData: Array<number> = this.inputFormControl.value;
    let isChecked = $event.target.checked;
    if (isChecked) {
      currentSelectedData.push(value);
      this.inputFormControl.setValue(currentSelectedData);
    } else {
      let newValue = FormUtils.removeItem(currentSelectedData, value);
      this.inputFormControl.setValue(newValue);
    }

    let item = this.items.find((item) => item.id == value);
    this.onChange.emit(item);
  }

  isChecked(value): boolean {
    let selectedItems: Array<any> = this.inputFormControl.value;
    let result = selectedItems.includes(value);
    return result;
  }
}
