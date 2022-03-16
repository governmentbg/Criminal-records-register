import { FormControl, FormGroup, Validators } from "@angular/forms";

export class MultipleChooseForm {
  public group: FormGroup;

  public selectedPrimaryKeys: FormControl;
  public selectedForeignKeys: FormControl;
  public isChanged: FormControl;

  constructor(isRequired?: boolean) {
    let validators = isRequired ? [Validators.required] : [];
    this.selectedPrimaryKeys = new FormControl();
    this.selectedForeignKeys = new FormControl("", validators);
    this.isChanged = new FormControl(false);

    this.group = new FormGroup({
        selectedPrimaryKeys: this.selectedPrimaryKeys,
        selectedForeignKeys: this.selectedForeignKeys,
        isChanged: this.isChanged
    });
  }
}