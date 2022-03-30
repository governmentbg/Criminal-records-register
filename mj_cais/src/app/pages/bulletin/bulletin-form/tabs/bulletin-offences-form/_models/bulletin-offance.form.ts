import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { AddressForm } from "../../../../../../@core/components/forms/address-form/model/address.form";
import { LookupForm } from "../../../../../../@core/components/forms/inputs/lookup/models/lookup.form";

export class BulletinOffenceForm {
  public group: FormGroup;
  public id: FormControl;
  public offenceCategory: LookupForm;
  public formOfGuilt: FormControl;
  public remarks: FormControl;
  public ecrisOffCatId: FormControl;
  public ecrisOffCatName: FormControl;
  public legalProvisions: FormControl;
  public offStartDate: FormControl;
  public offEndDate: FormControl;
  public occurrences: FormControl;
  public isContiniuous: FormControl;
  public respExemption: FormControl;
  public recidivism: FormControl;
  public offLvlComplId: FormControl;
  public offLvlComplName: FormControl;
  public offLvlPartId: FormControl;
  public offLvlPartName: FormControl;
  public offPlace: AddressForm;

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.offenceCategory = new LookupForm(true);
    this.formOfGuilt = new FormControl(null, [Validators.required]);
    this.remarks = new FormControl(null, [Validators.required]);
    this.ecrisOffCatId = new FormControl(null, [Validators.maxLength(50)]);
    this.ecrisOffCatName = new FormControl(null);
    this.legalProvisions = new FormControl(null);
    this.offStartDate = new FormControl(null, [Validators.required]);
    this.offEndDate = new FormControl(null, [Validators.required]); // тодо: крайна дата, ако е период
    this.occurrences = new FormControl(null);
    this.isContiniuous = new FormControl(null);
    this.respExemption = new FormControl(null);
    this.recidivism = new FormControl(null);
    this.offLvlComplId = new FormControl(null, [Validators.maxLength(50)]);
    this.offLvlComplName = new FormControl(null);
    this.offLvlPartId = new FormControl(null, [Validators.maxLength(50)]);
    this.offLvlPartName = new FormControl(null);
    this.offPlace = new AddressForm();

    this.group = new FormGroup({
      id: this.id,
      offenceCategory: this.offenceCategory.group,
      formOfGuilt: this.formOfGuilt,
      remarks: this.remarks,
      ecrisOffCatId: this.ecrisOffCatId,
      ecrisOffCatName: this.ecrisOffCatName,
      offStartDate: this.offStartDate,
      legalProvisions: this.legalProvisions,
      offEndDate: this.offEndDate,
      occurrences: this.occurrences,
      isContiniuous: this.isContiniuous,
      respExemption: this.respExemption,
      recidivism: this.recidivism,
      offLvlComplId: this.offLvlComplId,
      offLvlComplName: this.offLvlComplName,
      offLvlPartId: this.offLvlPartId,
      offLvlPartName: this.offLvlPartName,
      offPlace: this.offPlace.group,
    });
  }
}
