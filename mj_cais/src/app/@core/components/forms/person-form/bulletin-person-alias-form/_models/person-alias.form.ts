import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class PersonAliasForm {
  public group: FormGroup;
  public id: FormControl;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public fullname: FormControl;
  public typeId: FormControl;
  public typeName: FormControl;
  public typeCode: FormControl;

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.firstname = new FormControl(null, [Validators.max(200)]);
    this.surname = new FormControl(null, [Validators.max(200)]);
    this.familyname = new FormControl(null, [Validators.max(200)]);
    this.fullname = new FormControl(null, [Validators.max(200)]);
    this.typeId = new FormControl(null);
    this.typeName = new FormControl(null);
    this.typeCode = new FormControl(null, [Validators.max(200)]);

    this.group = new FormGroup({
      id: this.id,
      firstname: this.firstname,
      surname: this.surname,
      familyname: this.familyname,
      fullname: this.fullname,
      typeId: this.typeId,
      typeName: this.typeName,
      typeCode: this.typeCode,
    });
  }
}
