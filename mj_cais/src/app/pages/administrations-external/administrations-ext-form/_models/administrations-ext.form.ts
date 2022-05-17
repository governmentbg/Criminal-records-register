import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class AdministrationsExtForm {
    public group: FormGroup;
    public id: FormControl;
    public name: FormControl;
    public descr: FormControl;
    constructor() {
      var guid = Guid.create().toString();
      this.id = new FormControl(guid, [Validators.required]);
      this.name = new FormControl(null, [Validators.required]);
      this.descr = new FormControl(null);
  
      this.group = new FormGroup({
        id: this.id,
        name: this.name,
        descr: this.descr
      });
    }
  }