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
  public legalProvisions: FormControl;
  public offStartDate: FormControl;
  public offEndDate: FormControl;
  public offPlaceCountryId: FormControl;
  public offPlaceCountryName: FormControl;
  public offPlaceSubdivId: FormControl;
  public offPlaceSubdivName: FormControl;
  public offPlaceCityId: FormControl;
  public offPlaceCityName: FormControl;
  public offPlaceDescr: FormControl;
  public occurrences: FormControl;
  public isContiniuous: FormControl;
  public respExemption: FormControl;
  public recidivism: FormControl;
  public offLvlComplId: FormControl;
  public offLvlComplName: FormControl;
  public offLvlPartId: FormControl;
  public offLvlPartName: FormControl;

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.offenceCatId = new FormControl(null, [Validators.required]);
    this.offenceCatName = new FormControl(null);
    this.formOfGuilt = new FormControl(null, [Validators.required]);
    this.remarks = new FormControl(null, [Validators.required]);
    this.ecrisOffCatId = new FormControl(null);
    this.ecrisOffCatName = new FormControl(null);
    this.legalProvisions = new FormControl(null);
    this.offStartDate = new FormControl(null, [Validators.required]);
    this.offEndDate = new FormControl(null, [Validators.required]);// тодо: крайна дата, ако е период
    this.offPlaceCountryId = new FormControl(null);
    this.offPlaceCountryName = new FormControl(null);
    this.offPlaceSubdivId = new FormControl(null);
    this.offPlaceSubdivName = new FormControl(null);
    this.offPlaceCityId = new FormControl(null);
    this.offPlaceCityName = new FormControl(null);
    this.offPlaceDescr = new FormControl(null);
    this.occurrences = new FormControl(null);
    this.isContiniuous = new FormControl(null);
    this.respExemption = new FormControl(null);
    this.recidivism = new FormControl(null);
    this.offLvlComplId = new FormControl(null);
    this.offLvlComplName = new FormControl(null);
    this.offLvlPartId = new FormControl(null);
    this.offLvlPartName = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      offenceCatId: this.offenceCatId,
      offenceCatName: this.offenceCatName,
      formOfGuilt: this.formOfGuilt,
      remarks: this.remarks,
      ecrisOffCatId: this.ecrisOffCatId,
      ecrisOffCatName: this.ecrisOffCatName,
      offStartDate: this.offStartDate,
      legalProvisions: this.legalProvisions,
      offEndDate: this.offEndDate,
      offPlaceCountryId: this.offPlaceCountryId,
      offPlaceCountryName: this.offPlaceCountryName,
      offPlaceSubdivId: this.offPlaceSubdivId,
      offPlaceSubdivName: this.offPlaceSubdivName,
      offPlaceCityId: this.offPlaceCityId,
      offPlaceCityName: this.offPlaceCityName,
      offPlaceDescr: this.offPlaceDescr,
      occurrences: this.occurrences,
      isContiniuous: this.isContiniuous,
      respExemption: this.respExemption,
      recidivism: this.recidivism,
      offLvlComplId: this.offLvlComplId,
      offLvlComplName: this.offLvlComplName,
      offLvlPartId: this.offLvlPartId,
      offLvlPartName: this.offLvlPartName,
    });
  }
}
