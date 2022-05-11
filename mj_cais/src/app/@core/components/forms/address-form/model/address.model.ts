import { LookupModel } from "../../inputs/lookup/models/lookup.model";

export class AddressModel {
  districtId: number;
  districtDisplayName: string;
  municipalityId: number;
  municipalityDisplayName: string;
  cityId: number;
  cityDisplayName: string;
  foreignCountryAddress: string;
  country: LookupModel;

  constructor(init?: Partial<AddressModel>) {
    this.country = init?.country ?? null;
    this.districtId = init?.districtId ?? null;
    this.districtDisplayName = init?.districtDisplayName ?? null;
    this.municipalityId = init?.municipalityId ?? null;
    this.municipalityDisplayName = init?.municipalityDisplayName ?? null;
    this.cityId = init?.cityId ?? null;
    this.cityDisplayName = init?.cityDisplayName ?? null;
    this.foreignCountryAddress = init?.foreignCountryAddress ?? null;
  }
}
