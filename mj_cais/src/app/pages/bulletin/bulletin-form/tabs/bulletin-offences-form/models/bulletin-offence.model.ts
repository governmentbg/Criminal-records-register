import { AddressModel } from "../../../../../../@core/components/forms/address-form/model/address.model";

export class BulletinOffenceModel {
  public id: string = null;
  public offenceCatId: string = null;
  public offenceCatName: string = null;
  public remarks: string = null;
  public ecrisOffCatId: string = null;
  public ecrisOffCatName: string = null;
  public offStartDate: Date = null;
  public offEndDate: Date = null;
  public occurrences: number = null;
  public isContiniuous: boolean = null;
  public offLvlComplId: string = null;
  public offLvlComplName: string = null;
  public offLvlPartId: string = null;
  public offLvlPartName: string = null;
  public respExemption: boolean = null;
  public recidivism: boolean = null;
  public formOfGuilt: string = null;
  public offPlace: AddressModel = null;

  constructor(init?: Partial<BulletinOffenceModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.offenceCatId = init.offenceCatId ?? null;
      this.remarks = init.remarks ?? null;
      this.ecrisOffCatId = init.ecrisOffCatId ?? null;
      this.offStartDate = init.offStartDate ?? null;
      this.offEndDate = init.offEndDate ?? null;
      this.occurrences = init.occurrences ?? null;
      this.isContiniuous = init.isContiniuous ?? null;
      this.offLvlComplId = init.offLvlComplId ?? null;
      this.offLvlPartId = init.offLvlPartId ?? null;
      this.respExemption = init.respExemption ?? null;
      this.recidivism = init.recidivism ?? null;
      this.formOfGuilt = init.formOfGuilt ?? null;
      this.offenceCatName = init.offenceCatName ?? null;
      this.ecrisOffCatName = init.ecrisOffCatName ?? null;
        this.offLvlComplName = init.offLvlComplName ?? null;
      this.offLvlPartName = init.offLvlPartName ?? null;
      this.offPlace = init.offPlace ?? null;
    }
  }
}
