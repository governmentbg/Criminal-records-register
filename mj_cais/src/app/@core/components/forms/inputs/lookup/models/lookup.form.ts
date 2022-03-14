import { FormControl, FormGroup, Validators } from "@angular/forms";

export class LookupForm {
  public group: FormGroup;

  public id: FormControl;
  public displayName: FormControl;

  constructor(isRequired?: boolean) {
    let validators = isRequired ? [Validators.required] : [];
    this.id = new FormControl("", validators);
    this.displayName = new FormControl("", validators);

    this.group = new FormGroup({
      id: this.id,
      displayName: this.displayName,
    });
  }

  public setValue(id, displayName) {
    this.id.setValue(id);
    this.displayName.setValue(displayName);
  }
}
