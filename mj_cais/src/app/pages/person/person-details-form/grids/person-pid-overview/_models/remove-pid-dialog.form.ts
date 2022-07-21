import { FormControl, FormGroup } from "@angular/forms";
import { MultipleChooseForm } from "../../../../../../@core/components/forms/inputs/multiple-choose/models/multiple-choose.form";

export class RemovePidDialogFrom {
  public group: FormGroup;

  public existinPersonId: FormControl;
  public pidId: FormControl;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public fullname: FormControl;
  public firstnameLat: FormControl;
  public surnameLat: FormControl;
  public familynameLat: FormControl;
  public fullnameLat: FormControl;
  public sex: FormControl;
  public birthDate: FormControl;
  public nationalities: MultipleChooseForm;
  public motherFirstname: FormControl;
  public motherSurname: FormControl;
  public motherFamilyname: FormControl;
  public motherFullname: FormControl;
  public fatherFirstname: FormControl;
  public fatherSurname: FormControl;
  public fatherFamilyname: FormControl;
  public fatherFullname: FormControl;

  constructor() {
    this.existinPersonId = new FormControl(null);
    this.pidId = new FormControl(null);
    this.firstname = new FormControl(null);
    this.surname = new FormControl(null);
    this.familyname = new FormControl(null);
    this.fullname = new FormControl(null);
    this.firstnameLat = new FormControl(null);
    this.surnameLat = new FormControl(null);
    this.familynameLat = new FormControl(null);
    this.fullnameLat = new FormControl(null);
    this.sex = new FormControl(null);
    this.birthDate = new FormControl(null);
    this.nationalities = new MultipleChooseForm();
    this.motherFirstname = new FormControl(null);
    this.motherSurname = new FormControl(null);
    this.motherFamilyname = new FormControl(null);
    this.motherFullname = new FormControl(null);
    this.fatherFirstname = new FormControl(null);
    this.fatherSurname = new FormControl(null);
    this.fatherFamilyname = new FormControl(null);
    this.fatherFullname = new FormControl(null);

    this.group = new FormGroup({
      existinPersonId: this.existinPersonId,
      pidId: this.pidId,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      fullname: this.fullname,
      firstnameLat: this.firstnameLat,
      surnameLat: this.surnameLat,
      familynameLat: this.familynameLat,
      fullnameLat: this.fullnameLat,
      sex: this.sex,
      birthDate: this.birthDate,
      nationalities: this.nationalities.group,
      motherFirstname: this.motherFirstname,
      motherSurname: this.motherSurname,
      motherFamilyname: this.motherFamilyname,
      motherFullname: this.motherFullname,
      fatherFirstname: this.fatherFirstname,
      fatherSurname: this.fatherSurname,
      fatherFamilyname: this.fatherFamilyname,
      fatherFullname: this.fatherFullname,
    });
  }
}
