export class PersonGridModel {
  public id: string = null;
  public identifier: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;
  public fullName: string = null;
  public birthDate: Date = null;

  constructor(init?: Partial<PersonGridModel>) {
    this.id = init?.id ?? null;
    this.identifier = init?.identifier ?? null;
    this.firstName = init?.firstName ?? null;
    this.surName = init?.surName ?? null;
    this.familyName = init?.familyName ?? null;
    this.fullName = init?.fullName ?? null;
    this.birthDate = init?.birthDate ?? null;
  }
}
