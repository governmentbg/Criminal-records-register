import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class UnlockBulletinForm extends BaseForm {
  public group: FormGroup;
  public bulletinId: FormControl;
  public status: FormControl;
  public descr: FormControl;

  constructor() {
    super();
    this.bulletinId = new FormControl(null, [Validators.required]);
    this.status = new FormControl(null, [Validators.required]);
    this.descr = new FormControl(null, [Validators.required]);

    this.group = new FormGroup({
      id: this.id,
      bulletinId: this.bulletinId,
      status: this.status,
      descr: this.descr,
    });
  }
}
