import { FormControl, FormGroup } from "@angular/forms";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class AdministrationsExtUicForm extends BaseForm {
    public group: FormGroup;
    public name: FormControl;
    public value: FormControl;
  
    constructor() {
      super();
      this.name = new FormControl(null);
      this.value = new FormControl(null);
  
      this.group = new FormGroup({
        id: this.id,
        version: this.version,
        name: this.name,
        value: this.value
      });
    }
  }
  