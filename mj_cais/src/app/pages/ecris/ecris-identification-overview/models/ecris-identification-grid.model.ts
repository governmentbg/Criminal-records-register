export class EcrisIdentificationGridModel {
  public id: string = null;
  public msgTypeId: number = null;
  public msgTypeName: string = null;
  public msgTimestamp: Date = null;
  public identifier: string = null;
  public personNames: string = null;
  public birthDate: Date = null;
  public birthCountry: string = null;
  public birthCountryName: string = null;
  public birthCity: string = null;

  constructor(init?: Partial<EcrisIdentificationGridModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.msgTypeId = init.msgTypeId ?? null;
      this.msgTypeName = init.msgTypeName ?? null;
      this.msgTimestamp = init.msgTimestamp ?? null;
      this.identifier = init.identifier ?? null;
      this.personNames = init.personNames ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthCountry = init.birthCountry ?? null;
      this.birthCountryName = init.birthCountryName ?? null;
      this.birthCity = init.birthCity ?? null;
    }
  }
}
