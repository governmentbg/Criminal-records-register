import { LookupModel } from "../../inputs/lookup/models/lookup.model";

export class AddressModel {
  districtId: number;
  municipalityId: number;
  cityId: number;
  foreignCountryAddress: string;
  country: LookupModel;

  constructor(init?: Partial<AddressModel>) {
    this.country = init?.country ?? null;
    this.districtId = init?.districtId ?? null;
    this.municipalityId = init?.municipalityId ?? null;
    this.cityId = init?.cityId ?? null;
    this.foreignCountryAddress = init?.foreignCountryAddress ?? null;
  }
}
