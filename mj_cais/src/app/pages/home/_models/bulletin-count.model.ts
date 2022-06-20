export class BulletinCountModel {
  public newOffice: number = null;
  public newEISS: number = null;
  public forRehabilitation: number = null;
  public forDestruction: number = null;

  constructor(init?: Partial<BulletinCountModel>) {
    this.newOffice = init?.newOffice ?? null;
    this.newEISS = init?.newEISS ?? null;
    this.forRehabilitation = init?.forRehabilitation ?? null;
    this.forDestruction = init?.forDestruction ?? null;
  }
}
