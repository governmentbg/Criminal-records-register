import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseForm } from "../../../../../@core/models/common/base.form";

export class DeleteBulletinForm extends BaseForm {
  public group: FormGroup;
  public description: FormControl;

  constructor() {
    super();
    this.description = new FormControl(null, [Validators.required]);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      description: this.description,
    });
  }
}
