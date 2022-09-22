import { FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { BaseForm } from "../../../../@core/models/common/base.form";
import { createEgnValidator } from "../../../../@core/validators/egn-validation-function";
import { createEmailValidator } from "../../../../@core/validators/email-validator-function";


export function userNameOrEGNValidator(): ValidatorFn {
  return (formGroup: FormGroup): ValidationErrors | null => {
    const [userNameValue, egnValue] = [
      formGroup.get('userName')!.value,
      formGroup.get('egn')!.value
    ];

    if (!userNameValue && !egnValue){
      formGroup.get('userName').setErrors({ userNameOrEGN: true });
      formGroup.get('egn').setErrors({ userNameOrEGN: true });
    } else {
      
      var userNameControl = formGroup.controls['userName'] as FormControl;
      if (userNameControl.invalid) {
        userNameControl.clearValidators();
        userNameControl.updateValueAndValidity();
      }
      var egnControl = formGroup.controls['egn'] as FormControl;
      if (egnControl.invalid) {
        egnControl.clearValidators();
        egnControl.setValidators([
          createEgnValidator()
        ]);
        egnControl.updateValueAndValidity();
      }


      return null;
    }
  
    return !userNameValue && !egnValue
      ? { 'userNameOrEGN': { value: 'Потребителите трябва да имат ЕГН и/или потребителско име!' } }
      : null;
  };
}

export class UsersExternalForm extends BaseForm {
  public group: FormGroup;

  public name: FormControl;
  public active: FormControl;
  public isAdmin: FormControl;
  public email: FormControl;
  public egn: FormControl;
  public phone: FormControl;
  public userName: FormControl;
  public position: FormControl;
  public password: FormControl;
  public confirmPassword: FormControl;
  public administrationId: FormControl;
  public regCertSubject: FormControl;
  public ou: FormControl;
  public uic: FormControl;
  public denied: FormControl;
  public remarks: FormControl;

  constructor() {
    super();
    this.name = new FormControl(null, [Validators.required]);
    this.active = new FormControl(null);
    this.isAdmin = new FormControl(null);
    this.email = new FormControl(null, [createEmailValidator()]);
    this.egn = new FormControl(null, [createEgnValidator()]);
    this.position = new FormControl(null);
    this.userName = new FormControl(null);
    this.phone = new FormControl(null);
    this.password = new FormControl(null);
    this.confirmPassword = new FormControl(null);
    this.administrationId = new FormControl(null, [Validators.required]);
    this.regCertSubject =new FormControl(null);
    this.ou =new FormControl(null);
    this.uic =new FormControl(null);
    this.remarks =new FormControl(null);
    this.denied =new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      name: this.name,
      active: this.active,
      isAdmin: this.isAdmin,
      email: this.email,
      phone: this.phone,
      egn: this.egn,
      password: this.password,
      confirmPassword: this.confirmPassword,
      userName: this.userName, 
      position: this.position,
      administrationId: this.administrationId,
      regCertSubject: this.regCertSubject,
      ou: this.ou,
      uic: this.uic,
      remarks: this.remarks,
      denied: this.denied,
    }, userNameOrEGNValidator()
    );
  }
}
