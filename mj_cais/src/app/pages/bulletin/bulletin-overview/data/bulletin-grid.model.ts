export class BulletinGridModel {
  public id: number = null;
  public registrationNumber: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;

  constructor(init?: Partial<BulletinGridModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.registrationNumber = init.registrationNumber ?? null;
      this.firstName = init.firstName ?? null;
      this.surName = init.surName ?? null;
      this.familyName = init.familyName ?? null;
    }
  }
}
