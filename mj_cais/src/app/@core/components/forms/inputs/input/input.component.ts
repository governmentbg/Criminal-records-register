import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output
} from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { InputTypeConstants } from "../../../../constants/input-type.constants";
import { FormUtils } from "../../../../utils/form.utils";

@Component({
  selector: "cais-input",
  templateUrl: "./input.component.html",
  styleUrls: ["./input.component.scss"],
})
export class InputComponent implements OnInit {
  public InputTypeConstants = InputTypeConstants;

  @Input() label: string;
  @Input() inputName: string;
  @Input() inputFormControl: FormControl;
  @Input() parentGroup: FormGroup;
  @Input() type: string;
  @Input() inputType: string = InputTypeConstants.InputElement;
  @Input() isReadOnly: boolean;
  @Input() formatter: (item) => string;
  @Input() rows: string = "5";
  @Output() blur: EventEmitter<FocusEvent> = new EventEmitter();
  @Output() change: EventEmitter<any> = new EventEmitter<any>();
  @Output() keyUp: EventEmitter<KeyboardEvent> = new EventEmitter();

  constructor(public formUtils: FormUtils) {}

  public setInvalidContainer(): string {
    return this.inputFormControl.invalid &&
      (this.inputFormControl.touched || this.inputFormControl.dirty)
      ? "ng-invalid"
      : "";
  }

  public validationCss(): string {
    return this.inputFormControl.invalid &&
      (this.inputFormControl.touched || this.inputFormControl.dirty)
      ? "status-danger"
      : "";
  }

  public inputHasBeenInteracted(): boolean {
    return this.inputFormControl.touched || this.inputFormControl.dirty;
  }

  ngOnInit(): void {
    if (
      (this.type === "date" || this.type === "datetime") &&
      this.inputFormControl.value
    ) {
      let localDate = new Date(this.inputFormControl.value);
      this.inputFormControl.setValue(localDate);
    }
  }

  onBlur(event: FocusEvent) {
    this.blur.emit(event);
  }

  onKeyUp(event: KeyboardEvent) {
    this.keyUp.emit(event);
  }

  onChange(event: any) {
    this.change.emit(event);
  }

  formatDate(data) {
    return data.toLocaleDateString();
  }

  onNumberChange(event: any){
    if(this.inputFormControl.value == ""){
      this.inputFormControl.patchValue(null);
    }
    this.onChange(event);
  }
}
