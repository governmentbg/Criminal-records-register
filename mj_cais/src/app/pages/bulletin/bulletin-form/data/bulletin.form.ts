import { FormControl, FormGroup, Validators } from "@angular/forms";

export class BulletinForm {
  public group: FormGroup;

  public id: FormControl;
  public applicationNumber: FormControl;
  public applicationDate: FormControl;
  public customerTypeId: FormControl;

  constructor() {
    this.id = new FormControl(null, [Validators.required]);
    this.applicationNumber = new FormControl(null, [Validators.required]);
    this.applicationDate = new FormControl(null, [Validators.required]);
    this.customerTypeId = new FormControl(null, [Validators.required]);

    this.group = new FormGroup({
      id: this.id,
      applicationNumber: this.applicationNumber,
      applicationDate: this.applicationDate,
      customerTypeId: this.customerTypeId,
    });
  }
}
