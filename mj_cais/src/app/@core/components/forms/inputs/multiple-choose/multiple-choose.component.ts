import { Component, EventEmitter, Input, Output } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { BaseNomenclatureModel } from "../../../../models/nomenclature/base-nomenclature.model";
import { FormUtils } from "../../../../utils/form.utils";
import { MultipleChooseForm } from "./models/multiple-choose.form";

@Component({
  selector: "cais-multiple-choose",
  templateUrl: "./multiple-choose.component.html",
  styleUrls: ["./multiple-choose.component.scss"],
})
export class MultipleChooseComponent {

  @Input() label: string;
  @Input() inputFormControl: MultipleChooseForm;
  @Input() parentGroup: FormGroup;
  @Input() items: BaseNomenclatureModel[];

  @Output() selectionChanged: EventEmitter<string> = new EventEmitter<string>();
  @Output() selectionCleared: EventEmitter<void> = new EventEmitter<void>();

  public inputName = "selectedForeignKeys";

  constructor(public formUtils: FormUtils) {}

  onSelectionChange(value: string) {
    this.inputFormControl.isChanged.patchValue(true);
    if (value) {
      this.selectionChanged.emit(value);
    } else {
      this.selectionCleared.emit();
    }
  }

  public inputHasBeenInteracted(): boolean {
    return (
      this.inputFormControl.selectedForeignKeys.touched ||
      this.inputFormControl.selectedForeignKeys.dirty
    );
  }
}
