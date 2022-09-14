import { FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { BaseForm } from "../../../../@core/models/common/base.form";
import { createEgnValidator } from "../../../../@core/validators/egn-validation-function";
import { createEmailValidator } from "../../../../@core/validators/email-validator-function";


export class UsersExternalPasswordForm extends BaseForm {
  public group: FormGroup;

  public userName: FormControl;
  public password: FormControl;
  public confirmPassword: FormControl;

  constructor() {
    super();
    this.userName = new FormControl(null);
    this.password = new FormControl(null);
    this.confirmPassword = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      password: this.password,
      confirmPassword: this.confirmPassword,
      userName: this.userName, 
    }
    );
  }
}
