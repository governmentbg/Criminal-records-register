export class GraoPersonModel {
  public identifier: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;
  public birthDate: Date = null;

  constructor(init?: Partial<GraoPersonModel>) {
    this.identifier = init.identifier ?? null;
    this.firstName = init.firstName ?? null;
    this.surName = init.surName ?? null;
    this.familyName = init.familyName ?? null;
  }
}
