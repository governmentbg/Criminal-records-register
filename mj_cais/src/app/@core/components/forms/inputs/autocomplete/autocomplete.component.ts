import { Component, ViewChild, Input } from "@angular/core";
import { ElementRef, EventEmitter, Output } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { NgSelectComponent } from "@ng-select/ng-select";
import { FormUtils } from "../../../../utils/form.utils";
import { BaseNomenclatureModel } from "../../../../models/nomenclature/base-nomenclature.model";

@Component({
  selector: "cais-autocomplete",
  templateUrl: "./autocomplete.component.html",
  styleUrls: ["./autocomplete.component.scss"],
})
export class AutocompleteComponent {
  constructor(public formUtils: FormUtils) {}

  public inputHasBeenInteracted: boolean;

  @Output() selectionChanged: EventEmitter<string> = new EventEmitter<string>();
  @Output() selectionCleared: EventEmitter<void> = new EventEmitter<void>();

  @ViewChild("autoInput") input: ElementRef;
  @ViewChild("autoControl") autoControl: NgSelectComponent;

  @Input() label: string;
  @Input() inputName: string;
  @Input() inputFormControl: FormControl;
  @Input() parentGroup: FormGroup;
  @Input() items: BaseNomenclatureModel[];
  @Input() firstOptionSelected: boolean = false;
  @Input() initialSelectedItemId: number;
  @Input() allowCustomValues: boolean = false;
  @Input() customValueIsNumber: boolean = true;

  @Input() appendTo: string;

  ngOnInit() {
    if (!this.inputFormControl.touched && this.firstOptionSelected) {
      if (this.items && this.items.length > 0) {
        this.inputFormControl.setValue(this.items[0].id);
      }
    }
  }

  onSelectionChange(value: string) {
    if (value) {
      this.selectionChanged.emit(value);
    } else {
      this.selectionCleared.emit();
    }
  }

  addTagFn(newValue) {
    if (this.customValueIsNumber) {
      return Number(newValue);
    } else {
      return newValue;
    }
  }
  
  public validationCss(): string {
    return this.inputFormControl.invalid &&
      (this.inputFormControl.touched || this.inputFormControl.dirty)
      ? "status-danger"
      : "";
  }
}
