import { FormControl, FormGroup, Validators } from "@angular/forms";

export class EcrisTcnForm {
  public group: FormGroup;

  public id: FormControl;
  public action: FormControl;
  public status: FormControl;
  public bulletinId: FormControl;

  constructor() {
    this.id = new FormControl(null);
    this.action = new FormControl(null);
    this.status = new FormControl(null);
    this.bulletinId = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      action: this.action,
      status: this.status,
      bulletinId: this.bulletinId,
    });
  }
}
