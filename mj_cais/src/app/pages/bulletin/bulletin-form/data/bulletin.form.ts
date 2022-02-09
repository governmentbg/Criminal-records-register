import { FormControl, FormGroup, Validators } from "@angular/forms";

export class BulletinForm {
  public group: FormGroup;

  public id: FormControl;
  public version: FormControl;
  public csAuthorityId: FormControl;
  public registrationNumber: FormControl;

  constructor() {
    this.id = new FormControl(null);
    this.version = new FormControl(null);
    this.csAuthorityId = new FormControl(null);
    this.registrationNumber = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      csAuthorityId: this.csAuthorityId,
      registrationNumber: this.registrationNumber,
    });
  }
}
