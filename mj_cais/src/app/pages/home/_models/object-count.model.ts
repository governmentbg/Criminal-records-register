export class ObjectCountModel {
  public bulletinNewOfficeCount: number = null;
  public bulletinNewEISSCount: number = null;
  public bulletinForRehabilitationCount: number = null;
  public bulletinForDestructionCount: number = null;
  public isinDataCount: number = null;

  constructor(init?: Partial<ObjectCountModel>) {
    this.bulletinNewOfficeCount = init?.bulletinNewOfficeCount ?? null;
    this.bulletinNewEISSCount = init?.bulletinNewEISSCount ?? null;
    this.bulletinForRehabilitationCount =
      init?.bulletinForRehabilitationCount ?? null;
    this.bulletinForDestructionCount =
      init?.bulletinForDestructionCount ?? null;
    this.isinDataCount = init?.isinDataCount ?? null;
  }
}
