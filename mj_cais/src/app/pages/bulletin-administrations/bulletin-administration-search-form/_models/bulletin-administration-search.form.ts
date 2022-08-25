import { FormControl, FormGroup } from "@angular/forms";

export class BulletinAdministrationSearchForm {
  public group: FormGroup;

  public registrationNumber: FormControl;
  public bulletinType: FormControl;
  public statusId: FormControl;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public egn: FormControl;
  public lnch: FormControl;
  public birthDate: FormControl;
  public fromDate: FormControl;
  public toDate: FormControl;

  constructor() {
    this.registrationNumber = new FormControl(null);
    this.bulletinType = new FormControl(null);
    this.statusId = new FormControl(null);
    this.firstname = new FormControl(null);
    this.surname = new FormControl(null);
    this.familyname = new FormControl(null);
    this.egn = new FormControl(null);
    this.lnch = new FormControl(null);
    this.birthDate = new FormControl(null);
    let dateMonthBefore = new Date();
    dateMonthBefore.setMonth(dateMonthBefore.getMonth() - 1);
    this.fromDate = new FormControl(dateMonthBefore);
    this.toDate = new FormControl(new Date());

    this.group = new FormGroup({
      registrationNumber: this.registrationNumber,
      bulletinType: this.bulletinType,
      statusId: this.statusId,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      egn: this.egn,
      lnch: this.lnch,
      birthDate: this.birthDate,
      fromDate: this.fromDate,
      toDate: this.toDate,
    });
  }
}
