export class ObjectCountModel {
  public bulletinNewOfficeCount: number = null;
  public bulletinNewEISSCount: number = null;
  public bulletinForRehabilitationCount: number = null;
  public bulletinForDestructionCount: number = null;
  public isinNewCount: number = null;
  public isinIdentifiedCount: number = null;
  public newIsinMessagesCount: number = null;
  public ecrisForIdentificationCount: number = null;
  public ecrisWaitingForCSAuthorityCount: number = null;

  constructor(init?: Partial<ObjectCountModel>) {
    this.bulletinNewOfficeCount = init?.bulletinNewOfficeCount ?? null;
    this.bulletinNewEISSCount = init?.bulletinNewEISSCount ?? null;
    this.bulletinForRehabilitationCount =
      init?.bulletinForRehabilitationCount ?? null;
    this.bulletinForDestructionCount =
      init?.bulletinForDestructionCount ?? null;
    this.isinNewCount = init?.isinNewCount ?? null;
    this.isinIdentifiedCount = init?.isinIdentifiedCount ?? null;
    this.newIsinMessagesCount = init?.newIsinMessagesCount ?? null;
    this.ecrisForIdentificationCount =
      init?.ecrisForIdentificationCount ?? null;
    this.ecrisWaitingForCSAuthorityCount =
      init?.ecrisWaitingForCSAuthorityCount ?? null;
  }
}
