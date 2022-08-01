export class BulletinCountModel {
  public newEISS: number = null;
  public forRehabilitation: number = null;
  public forDestruction: number = null;
  public article2211: number = null;
  public article2212: number = null;
  public article3000: number = null;
  public newDocument: number = null;
  public isinNew: number = null;
  public isinIdentified: number = null;

  constructor(init?: Partial<BulletinCountModel>) {
    this.newEISS = init?.newEISS ?? null;
    this.forRehabilitation = init?.forRehabilitation ?? null;
    this.forDestruction = init?.forDestruction ?? null;
    this.article2211 = init?.article2211 ?? null;
    this.article2212 = init?.article2212 ?? null;
    this.article3000 = init?.article3000 ?? null;
    this.newDocument = init?.newDocument ?? null;
    this.isinNew = init?.isinNew ?? null;
    this.isinIdentified = init?.isinIdentified ?? null;
  }
}
