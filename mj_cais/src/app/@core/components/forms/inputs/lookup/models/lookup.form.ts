import { FormControl, FormGroup, Validators } from "@angular/forms";

export class LookupForm {
  public group: FormGroup;

  public id: FormControl;
  public displayName: FormControl;

  constructor(isRequired?: boolean, isDisabled?: boolean) {
    let validators = isRequired && (!isDisabled || isDisabled == null) ? [Validators.required] : [];
    this.id = new FormControl("", validators);
    this.displayName = new FormControl("", validators);

    if(isDisabled){
      this.id.disable();
      this.displayName.disable();
    }
    
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
