export class BulletinEventCountModel {
  public article2211: number = null;
  public article2212: number = null;
  public article3000: number = null;
  public newDocument: number = null;

  constructor(init?: Partial<BulletinEventCountModel>) {
    this.article2211 = init?.article2211 ?? null;
    this.article2212 = init?.article2212 ?? null;
    this.article3000 = init?.article3000 ?? null;
    this.newDocument = init?.newDocument ?? null;
  }
}