import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class UsersExternalForm {
    public group: FormGroup;
    public id: FormControl;
    public name: FormControl;
    public active: FormControl;
    public isAdmin: FormControl;
    public email: FormControl;
    public egn: FormControl;
    public position: FormControl;
    public administrationId: FormControl;
  
    constructor() {
      var guid = Guid.create().toString();
      this.id = new FormControl(guid, [Validators.required]);
      this.name = new FormControl(null, [Validators.required]);
      this.active = new FormControl(null);
      this.isAdmin = new FormControl(null);
      this.email = new FormControl(null);
      this.egn = new FormControl(null, [Validators.required]);
      this.position = new FormControl(null);
      this.administrationId = new FormControl(null, [Validators.required]);
  
      this.group = new FormGroup({
        id: this.id,
        name: this.name,
        active: this.active,
        isAdmin: this.isAdmin,
        email: this.email,
        egn: this.egn,
        position: this.position,
        administrationId: this.administrationId
      });
    }
  }