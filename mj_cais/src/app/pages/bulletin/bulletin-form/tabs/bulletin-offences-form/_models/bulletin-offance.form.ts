import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { AddressForm } from "../../../../../../@core/components/forms/address-form/_model/address.form";
import { DatePrecisionModelForm } from "../../../../../../@core/components/forms/inputs/date-precision/_models/date-precision.form";
import { LookupForm } from "../../../../../../@core/components/forms/inputs/lookup/models/lookup.form";
import { BaseForm } from "../../../../../../@core/models/common/base.form";

export class BulletinOffenceForm extends BaseForm {
  public group: FormGroup;
  public offenceCategory: LookupForm;
  public formOfGuiltId: FormControl;
  public formOfGuiltName: FormControl;
  public remarks: FormControl;
  public ecrisOffCatId: FormControl;
  public ecrisOffCatName: FormControl;
  public legalProvisions: FormControl;
  public offStartDate: DatePrecisionModelForm;
  public offEndDate: DatePrecisionModelForm;
  public offPlace: AddressForm;

  constructor() {
    super();
    this.offenceCategory = new LookupForm(true);
    this.formOfGuiltId = new FormControl(null);
    this.formOfGuiltName = new FormControl(null);
    this.remarks = new FormControl(null, [
      Validators.required,
      Validators.maxLength(2000),
    ]);
    this.ecrisOffCatId = new FormControl(null, [Validators.maxLength(50)]);
    this.ecrisOffCatName = new FormControl(null);
    this.legalProvisions = new FormControl(null,[Validators.maxLength(2000)]);
    this.offStartDate = new DatePrecisionModelForm(null);
    this.offEndDate = new DatePrecisionModelForm();
    this.offPlace = new AddressForm();

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      offenceCategory: this.offenceCategory.group,
      formOfGuiltId: this.formOfGuiltId,
      formOfGuiltName: this.formOfGuiltName,
      remarks: this.remarks,
      ecrisOffCatId: this.ecrisOffCatId,
      ecrisOffCatName: this.ecrisOffCatName,
      offStartDate: this.offStartDate.group,
      legalProvisions: this.legalProvisions,
      offEndDate: this.offEndDate.group,
      offPlace: this.offPlace.group,
    });
  }
}
