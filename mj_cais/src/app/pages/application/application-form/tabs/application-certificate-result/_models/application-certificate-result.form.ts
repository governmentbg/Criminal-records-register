import { FormControl, FormGroup } from "@angular/forms";
import { BaseForm } from "../../../../../../@core/models/common/base.form";

export class ApplicationCertificateResultForm extends BaseForm {
  public group: FormGroup;

  public firstSignerId: FormControl;
  public secondSignerId: FormControl;
  public validFrom: FormControl;
  public validTo: FormControl;
  
  constructor() {
    super();
    this.firstSignerId = new FormControl(null);
    this.secondSignerId = new FormControl(null);
    this.validFrom = new FormControl(null);
    this.validTo = new FormControl(null);
   
    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      firstSignerId: this.firstSignerId,
      secondSignerId: this.secondSignerId,
      validFrom: this.validFrom,
      validTo: this.validTo,   
    });
  }
}
