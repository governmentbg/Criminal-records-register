import { FormControl, FormGroup, Validators } from "@angular/forms";

export class MultipleChooseForm {
  public group: FormGroup;

  public selectedPrimaryKeys: FormControl;
  public selectedForeignKeys: FormControl;
  public isChanged: FormControl;

  constructor(isRequired?: boolean, disabled?: boolean) {

    let validators = isRequired && !disabled? [Validators.required] : [];
    this.selectedPrimaryKeys = new FormControl([]);
    this.selectedForeignKeys = new FormControl([], validators);
    this.isChanged = new FormControl(false);

    if(disabled){
      this.selectedPrimaryKeys.disable();
      this.selectedForeignKeys.disable();
      this.isChanged.disable();
    }

    this.group = new FormGroup({
      selectedPrimaryKeys: this.selectedPrimaryKeys,
      selectedForeignKeys: this.selectedForeignKeys,
      isChanged: this.isChanged,
    });
  }
}