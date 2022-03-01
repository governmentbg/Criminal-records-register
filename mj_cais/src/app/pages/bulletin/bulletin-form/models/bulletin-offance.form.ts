import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class BulletinOffenceForm {
  public group: FormGroup;
  public id: FormControl;
  public offenceCatId: FormControl;
  public offenceCatName: FormControl;
  public formOfGuilt: FormControl;
  public remarks: FormControl;
  public ecrisOffCatId: FormControl;
  public ecrisOffCatName: FormControl;
  public offStartDate: FormControl;
  public offEndDate: FormControl;
  public offPlaceCountryId: FormControl;
  public offPlaceSubdivId: FormControl;
  public offPlaceCityId: FormControl;
  public offPlaceDescr: FormControl;
  public occurrences: FormControl;
  public isContiniuous: FormControl;
  public respExemption: FormControl;
  public recidivism: FormControl;
  public offLvlComplId: FormControl;
  public offLvlPartId: FormControl;

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.offenceCatId = new FormControl(null, [Validators.required]);
    this.offenceCatName = new FormControl(null);
    this.formOfGuilt = new FormControl(null);
    this.remarks = new FormControl(null);
    this.ecrisOffCatId = new FormControl(null);
    this.ecrisOffCatName = new FormControl(null);
    this.offStartDate = new FormControl(null);
    this.offEndDate = new FormControl(null);
    this.offPlaceCountryId = new FormControl(null);
    this.offPlaceSubdivId = new FormControl(null);
    this.offPlaceCityId = new FormControl(null);
    this.offPlaceDescr = new FormControl(null);
    this.occurrences = new FormControl(null);
    this.isContiniuous = new FormControl(null);
    this.respExemption = new FormControl(null);
    this.recidivism = new FormControl(null);
    this.offLvlComplId = new FormControl(null);
    this.offLvlPartId = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      offenceCatId: this.offenceCatId,
      offenceCatName: this.offenceCatName,
      formOfGuilt: this.formOfGuilt,
      remarks: this.remarks,
      ecrisOffCatId: this.ecrisOffCatId,
      offStartDate: this.offStartDate,
      ecrisOffCatName: this.ecrisOffCatName,
      offEndDate: this.offEndDate,
      offPlaceCountryId: this.offPlaceCountryId,
      offPlaceSubdivId: this.offPlaceSubdivId,
      offPlaceCityId: this.offPlaceCityId,
      offPlaceDescr: this.offPlaceDescr,
      occurrences: this.occurrences,
      isContiniuous: this.isContiniuous,
      respExemption: this.respExemption,
      recidivism: this.recidivism,
      offLvlComplId: this.offLvlComplId,
      offLvlPartId: this.offLvlPartId
    });
  }
}