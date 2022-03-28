import { FormControl, FormGroup, Validators } from "@angular/forms";

export class InternalRequestForm {
  public group: FormGroup;
  public id: FormControl;
  public regNumber: FormControl;
  public requestDate: FormControl;
  public description: FormControl;
  public bulletinId: FormControl;
  public reqStatusCode: FormControl;
  public responseDescr: FormControl;
  public reqStatusName: FormControl;

  constructor() {
    this.id = new FormControl(null);
    this.regNumber = new FormControl(null);
    this.requestDate = new FormControl(new Date);
    this.description = new FormControl(null);
    this.bulletinId = new FormControl(null);
    this.reqStatusCode = new FormControl(null);
    this.responseDescr = new FormControl(null);
    this.reqStatusName = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      regNumber: this.regNumber,
      requestDate: this.requestDate,
      description: this.description,
      bulletinId: this.bulletinId,
      reqStatusCode: this.reqStatusCode,
      responseDescr: this.responseDescr,
      reqStatusName: this.reqStatusName
    });
  }
}
