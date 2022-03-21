export class AddressModel {
  countryId: number;
  districtId: number;
  municipalityId: number;
  cityId: number;

  constructor(init?: Partial<AddressModel>) {
    this.countryId = init?.countryId ?? null;
    this.districtId = init?.districtId ?? null;
    this.municipalityId = init?.municipalityId ?? null;
    this.cityId = init?.cityId ?? null;
  }
}
