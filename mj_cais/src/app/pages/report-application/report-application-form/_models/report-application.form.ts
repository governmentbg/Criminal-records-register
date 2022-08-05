import { FormControl, FormGroup, Validators } from "@angular/forms";
import { PersonContextEnum } from "../../../../@core/components/forms/person-form/_models/person-context-enum";
import { PersonForm } from "../../../../@core/components/forms/person-form/_models/person.form";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class ReportApplicationForm extends BaseForm {
  public group: FormGroup;

  public registrationNumber: FormControl;
  public registrationNumberDisplay: FormControl;
  public purpose: FormControl;
  public person: PersonForm;
  public applicantName: FormControl;
  public addrName: FormControl;
  public addrStr: FormControl;
  public addrDistrict: FormControl;
  public addrTown: FormControl;
  public addrState: FormControl;
  public addrPhone: FormControl;
  public addrEmail: FormControl;
  public description: FormControl;
  public statusCode: FormControl;
  public csAuthorityId: FormControl;
  public applicantDescr: FormControl;
  public purposeId: FormControl;
  public statusName: FormControl;
  public csAuthorityName: FormControl;
  public firstSignerId: FormControl;
  public secondSignerId: FormControl;

  constructor() {
    super();
    this.registrationNumber = new FormControl(null);
    this.registrationNumberDisplay = new FormControl(null);
    this.purpose = new FormControl(null);
    this.person = new PersonForm(PersonContextEnum.Application, false);
    this.applicantName = new FormControl(null);
    this.addrName = new FormControl(null);
    this.addrStr = new FormControl(null);
    this.addrTown = new FormControl(null);
    this.addrDistrict = new FormControl(null);
    this.addrState = new FormControl(null);
    this.addrPhone = new FormControl(null);
    this.addrEmail = new FormControl(null);
    this.description = new FormControl(null);
    this.statusCode = new FormControl(null);
    this.csAuthorityId = new FormControl(null);
    this.applicantDescr = new FormControl(null);
    this.purposeId = new FormControl(null);
    this.statusName = new FormControl(null);
    this.csAuthorityName = new FormControl(null);
    this.firstSignerId = new FormControl(null);
    this.secondSignerId = new FormControl(null);

    this.csAuthorityName.disable();
    this.registrationNumberDisplay.disable();
    this.statusName.disable();

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      registrationNumber: this.registrationNumber,
      registrationNumberDisplay: this.registrationNumberDisplay,
      purpose: this.purpose,
      person: this.person.group,
      applicantName: this.applicantName,
      addrName: this.addrName,
      addrStr: this.addrStr,
      addrTown: this.addrTown,
      addrDistrict: this.addrDistrict,
      addrState: this.addrState,
      addrPhone: this.addrPhone,
      addrEmail: this.addrEmail,
      description: this.description,
      statusCode: this.statusCode,
      csAuthorityId: this.csAuthorityId,
      applicantDescr: this.applicantDescr,
      purposeId: this.purposeId,
      statusName: this.statusName,
      csAuthorityName: this.csAuthorityName,
      firstSignerId: this.firstSignerId,
      secondSignerId: this.secondSignerId,
    });
  }
}
