import { FormControl, FormGroup } from "@angular/forms";
export class PersonSearchForm {
  public group: FormGroup;

  public egn: FormControl;
  public lnch: FormControl;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public fullname: FormControl;
  public birthDate: FormControl;

  constructor() {
    this.egn = new FormControl(null);
    this.lnch = new FormControl(null);
    this.firstname = new FormControl(null);
    this.surname = new FormControl(null);
    this.familyname = new FormControl(null);
    this.fullname = new FormControl(null);
    this.birthDate = new FormControl(null);

    this.group = new FormGroup({
      egn: this.egn,
      lnch: this.lnch,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      fullname: this.fullname,
      birthDate: this.birthDate,
    });
  }
}
