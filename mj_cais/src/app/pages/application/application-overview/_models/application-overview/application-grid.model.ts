export class ApplicationGridModel {
  public id: string;
  public registrationNumber: string;
  public purpose: string;
  public firstname: string;
  public surname: string;
  public familyname: string;
  public applicationTypeId: string;
  public birthDate: Date;
  public birthPlaceOther: string;

  constructor(init?: Partial<ApplicationGridModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.registrationNumber = init.registrationNumber ?? null;
      this.purpose = init.purpose ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthPlaceOther = init.birthPlaceOther ?? null;
      this.applicationTypeId = init.applicationTypeId ?? null;
    }
  }
}
