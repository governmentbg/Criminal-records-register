import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { BaseForm } from "../../../../../models/common/base.form";

export class PersonAliasForm extends BaseForm {
  public group: FormGroup;
  public firstname: FormControl;
  public surname: FormControl;
  public familyname: FormControl;
  public fullname: FormControl;
  public typeId: FormControl;
  public typeName: FormControl;
  public typeCode: FormControl;

  constructor() {
    super();
    this.firstname = new FormControl(null, [Validators.max(200)]);
    this.surname = new FormControl(null, [Validators.max(200)]);
    this.familyname = new FormControl(null, [Validators.max(200)]);
    this.fullname = new FormControl(null, [Validators.max(200)]);
    this.typeId = new FormControl(null);
    this.typeName = new FormControl(null);
    this.typeCode = new FormControl(null, [Validators.max(200)]);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
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
