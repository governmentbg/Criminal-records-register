import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class InternalRequestForm {
  public group: FormGroup;
  public id: FormControl;
  public regNumber: FormControl;
  public requestDate: FormControl;
  public description: FormControl;
  public bulletinId: FormControl;

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.regNumber = new FormControl(null);
    this.requestDate = new FormControl(Date.now);
    this.description = new FormControl(null);
    this.bulletinId = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      regNumber: this.regNumber,
      requestDate: this.requestDate,
      description: this.description,
      bulletinId: this.bulletinId
    });
  }
}
