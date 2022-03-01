export class BulletinOffenceModel {
    public id: string = null;
    public offenceCatId: string = null;
    public offenceCatName : string = null;
    public remarks: string = null;
    public ecrisOffCatId: string = null;
    public ecrisOffCatName: string = null;
    public offStartDate: Date = null;
    public offEndDate: Date = null;
    public offPlaceCountryId: string = null;
    public offPlaceCountryName: string = null;
    public offPlaceSubdivId: string = null;
    public offPlaceSubdivName : string = null;
    public offPlaceCityId: string = null;
    public offPlaceCityName: string = null;
    public offPlaceDescr: string = null;
    public occurrences: number = null;
    public isContiniuous: number = null;
    public offLvlComplId: string = null;
    public offLvlComplName: string = null;
    public offLvlPartId: string = null;
    public offLvlPartName: string = null;
    public respExemption: number = null;
    public recidivism: number = null;
    public formOfGuilt: string = null;

    constructor(init?: Partial<BulletinOffenceModel>) {
      if (init) {
        this.id = init.id ?? null;
        this.offenceCatId = init.offenceCatId ?? null;
        this.remarks = init.remarks ?? null;
        this.ecrisOffCatId = init.ecrisOffCatId ?? null;
        this.offStartDate = init.offStartDate ?? null;
        this.offEndDate = init.offEndDate ?? null;
        this.offPlaceCountryId = init.offPlaceCountryId ?? null;
        this.offPlaceSubdivId = init.offPlaceSubdivId ?? null;
        this.offPlaceCityId = init.offPlaceCityId ?? null;
        this.offPlaceDescr = init.offPlaceDescr ?? null;
        this.occurrences = init.occurrences ?? null;
        this.isContiniuous = init.isContiniuous ?? null;
        this.offLvlComplId = init.offLvlComplId ?? null;       
        this.offLvlPartId = init.offLvlPartId ?? null;
        this.respExemption = init.respExemption ?? null;
        this.recidivism = init.recidivism ?? null;
        this.formOfGuilt = init.formOfGuilt ?? null;
        this.offenceCatName = init.offenceCatName ?? null;
        this.ecrisOffCatName = init.ecrisOffCatName ?? null;
        this.offPlaceCountryName = init.offPlaceCountryName ?? null;
        this.offPlaceSubdivName = init.offPlaceSubdivName ?? null;
        this.offPlaceCityName = init.offPlaceCityName ?? null;
        this.offLvlComplName = init.offLvlComplName ?? null;
        this.offLvlPartName = init.offLvlPartName ?? null;
      }
    }
  }
  