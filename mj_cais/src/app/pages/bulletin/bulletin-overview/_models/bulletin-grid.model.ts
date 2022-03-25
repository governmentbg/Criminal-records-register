export class BulletinGridModel {
  public id: string = null;
  public registrationNumber: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;
  public statusId: string = null;
  public createdOn: Date = null;
  public alphabeticalIndex: string = null;
  public bulletinAuthorityName: string = null;
  public ln: string = null;
  public lnch: string = null;
  public egn: string = null;
  public deleteDate: Date = null;
  public rehabilitationDate: Date = null;
  public bulletinType: string = null;

  constructor(init?: Partial<BulletinGridModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.registrationNumber = init.registrationNumber ?? null;
      this.firstName = init.firstName ?? null;
      this.surName = init.surName ?? null;
      this.familyName = init.familyName ?? null;
      this.statusId = init.statusId ?? null;
      this.createdOn = init.createdOn ?? null;
      this.alphabeticalIndex = init.alphabeticalIndex ?? null;
      this.bulletinAuthorityName = init.bulletinAuthorityName ?? null;
      this.ln = init.ln ?? null;
      this.lnch = init.lnch ?? null;
      this.egn = init.egn ?? null;
      this.deleteDate = init.deleteDate ?? null;
      this.rehabilitationDate = init.rehabilitationDate ?? null;
      this.bulletinType = init.bulletinType ?? null;
    }
  }
}
