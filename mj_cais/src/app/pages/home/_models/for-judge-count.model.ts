export class ForJudgeCountModel {
  public taxFree: number = null;
  public forSigningByJudge: number = null;
  public bulletinSelection: number = null;
  public internalRequests: number = null;

  constructor(init?: Partial<ForJudgeCountModel>) {
    this.taxFree = init?.taxFree ?? null;
    this.forSigningByJudge = init?.forSigningByJudge ?? null;
    this.bulletinSelection = init?.bulletinSelection ?? null;
    this.internalRequests = init?.internalRequests ?? null;
  }
}
