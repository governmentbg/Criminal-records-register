export class PersonApplicationGridModel {
  public id: string = null;
  public registrationNumber: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;

  constructor(init?: Partial<PersonApplicationGridModel>) {
    this.id = init?.id ?? null;
    this.registrationNumber = init?.registrationNumber ?? null;
    this.firstName = init?.firstName ?? null;
    this.surName = init?.surName ?? null;
    this.familyName = init?.familyName ?? null;
  }
}