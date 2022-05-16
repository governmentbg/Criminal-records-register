export class ApplicationGridModel {
  public id: string;
  public registrationNumber: string;
  public purpose: string;
  public firstname: string;
  public surname: string;
  public familyname: string;
  public fullname: string;
  public firstnameLat: string;
  public surnameLat: string;
  public familynameLat: string;
  public applicationTypeId: string;

  constructor(init?: Partial<ApplicationGridModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.registrationNumber = init.registrationNumber ?? null;
      this.purpose = init.purpose ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.fullname = init.fullname ?? null;
      this.firstnameLat = init.firstnameLat ?? null;
      this.surnameLat = init.surnameLat ?? null;
      this.familynameLat = init.familynameLat ?? null;
      this.applicationTypeId = init.applicationTypeId ?? null;
    }
  }
}
