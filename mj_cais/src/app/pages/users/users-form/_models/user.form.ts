import { Input } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { MultipleChooseForm } from "../../../../@core/components/forms/inputs/multiple-choose/models/multiple-choose.form";
import { BaseNomenclatureModel } from "../../../../@core/models/nomenclature/base-nomenclature.model";

export class UserForm {
    public group: FormGroup;
    public id: FormControl;
    public firstname: FormControl;
    public surname: FormControl;
    public familyname: FormControl;
    public active: FormControl;
    public email: FormControl;
    public egn: FormControl;
    public position: FormControl;
    public csAuthorityId: FormControl;
    public roles: MultipleChooseForm;
  
    constructor() {
      var guid = Guid.create().toString();
      this.id = new FormControl(guid, [Validators.required]);
      this.firstname = new FormControl(null, [Validators.required]);
      this.surname = new FormControl(null, [Validators.required]);
      this.familyname = new FormControl(null, [Validators.required]);
      this.active = new FormControl(null);
      this.email = new FormControl(null);
      this.egn = new FormControl(null, [Validators.required]);
      this.position = new FormControl(null);
      this.csAuthorityId = new FormControl(null);
      this.roles = new MultipleChooseForm();
  
      this.group = new FormGroup({
        id: this.id,
        firstname: this.firstname,
        surname: this.surname,
        familyname: this.familyname,
        active: this.active,
        email: this.email,
        egn: this.egn,
        position: this.position,
        csAuthorityId: this.csAuthorityId,
        roles: this.roles.group,
      });
    }
  }