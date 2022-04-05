export class EcrisMessageGridModel {
  public id: string = null;
  public docTypeId: string = null;
  public docTypeName: string = null;
  public identifier: string = null;
  public ecrisIdentifier: string = null;
  public msgTimestamp: Date = null;
  public ecrisMsgStatus: string = null;
  public ecrisMsgStatusName: string = null;
  public birthDate: Date = null;
  public birthCountry: string = null;
  public birthCountryName: string = null;
  public birthCity: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public nationality1Code: string = null;
  public nationality2Code: string = null;

  constructor(init?: Partial<EcrisMessageGridModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.docTypeId = init.docTypeId ?? null;
      this.docTypeName = init.docTypeName ?? null;
      this.identifier = init.identifier ?? null;
      this.ecrisIdentifier = init.ecrisIdentifier ?? null;
      this.msgTimestamp = init.msgTimestamp ?? null;
      this.ecrisMsgStatus = init.ecrisMsgStatus ?? null;
      this.ecrisMsgStatusName = init.ecrisMsgStatusName ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthCountry = init.birthCountry ?? null;
      this.birthCountryName = init.birthCountryName ?? null;
      this.birthCity = init.birthCity ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.nationality1Code = init.nationality1Code ?? null;
      this.nationality2Code = init.nationality2Code ?? null;
    }
  }
}
