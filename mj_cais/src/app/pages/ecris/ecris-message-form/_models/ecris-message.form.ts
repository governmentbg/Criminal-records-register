import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class EcrisMessageForm {
  public group: FormGroup;
  public id: FormControl;
  public requestMsgId: FormControl;
  public fromAuthId: FormControl;
  public toAuthId: FormControl;
  public identifier: FormControl;
  public ecrisIdentifier: FormControl;
  public msgTimestamp: FormControl;
  public responseTypeId: FormControl;
  public ecrisMsgStatus: FormControl;
  public birthDate: FormControl;
  public birthCountry: FormControl;
  public birthCity: FormControl;
  public fbbcId: FormControl;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public sex: FormControl;
  public nationality1Code: FormControl;
  public nationality2Code: FormControl;
  public msgTypeId: FormControl;

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.requestMsgId = new FormControl(null);
    this.fromAuthId = new FormControl(null);
    this.toAuthId = new FormControl(null);
    this.identifier = new FormControl(null);
    this.ecrisIdentifier = new FormControl(null);
    this.msgTimestamp = new FormControl(null);
    this.responseTypeId = new FormControl(null);
    this.ecrisMsgStatus = new FormControl(null);
    this.birthDate = new FormControl(null);
    this.birthCountry = new FormControl(null);
    this.birthCity = new FormControl(null);
    this.fbbcId = new FormControl(null);
    this.firstname = new FormControl(null);
    this.surname = new FormControl(null);
    this.familyname = new FormControl(null);
    this.sex = new FormControl(null);
    this.nationality1Code = new FormControl(null);
    this.nationality2Code = new FormControl(null);
    this.msgTypeId = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      requestMsgId: this.requestMsgId,
      fromAuthId: this.fromAuthId,
      toAuthId: this.toAuthId,
      identifier: this.identifier,
      ecrisIdentifier: this.ecrisIdentifier,
      msgTimestamp: this.msgTimestamp,
      responseTypeId: this.responseTypeId,
      ecrisMsgStatus: this.ecrisMsgStatus,
      birthDate: this.birthDate,
      birthCountry: this.birthCountry,
      birthCity: this.birthCity,
      fbbcId: this.fbbcId,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      sex: this.sex,
      nationality1Code: this.nationality1Code,
      nationality2Code: this.nationality2Code,
      msgTypeId: this.msgTypeId,
    });
  }
}
