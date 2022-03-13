export class FbbcGridModel {
  public id: string = null;
  public receiveDate: Date = null;
  public egn: string = null;
  public birthDate: number = null;
  public birthCityId: string = null;
  public birthCountryId: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;

  constructor(init?: Partial<FbbcGridModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.receiveDate = init.receiveDate ?? null;
      this.egn = init.egn ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthCityId = init.birthCityId ?? null;
      this.birthCountryId = init.birthCountryId ?? null;
      this.firstName = init.firstName ?? null;
      this.surName = init.surName ?? null;
      this.familyName = init.familyName ?? null;
    }
  }
}
