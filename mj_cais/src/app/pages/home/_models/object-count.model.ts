import { BaseModel } from "../../../@core/models/common/base.model";
export class ObjectCountModel extends BaseModel {
  public bulletinNewOfficeCount: number = null;
  public bulletinNewEISSCount: number = null;
  public bulletinForRehabilitationCount: number = null;
  public bulletinForDestructionCount: number = null;
  public isinNewCount: number = null;
  public isinIdentifiedCount: number = null;
  public newIsinMessagesCount: number = null;
  public ecrisForIdentificationCount: number = null;
  public ecrisWaitingForCSAuthorityCount: number = null;
  public bulletinEventArticle2211Count: number = null;
  public bulletinEventArticle2212Count: number = null;
  public bulletinEventArticle3000Count: number = null;
  public bulletinEventNewDocumentCount: number = null;
  public applicationNewIdCount: number = null;
  public applicationCheckPaymentCount: number = null;
  public applicationCheckTaxFreeCount: number = null;
  public applicationBulletinsCheckCount: number = null;

  constructor(init?: Partial<ObjectCountModel>) {
    super(init);
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
    this.bulletinEventArticle2211Count =
      init?.bulletinEventArticle2211Count ?? null;
    this.bulletinEventArticle2212Count =
      init?.bulletinEventArticle2212Count ?? null;
    this.bulletinEventArticle3000Count =
      init?.bulletinEventArticle3000Count ?? null;
    this.bulletinEventNewDocumentCount =
      init?.bulletinEventNewDocumentCount ?? null;
    this.applicationNewIdCount = init?.applicationNewIdCount ?? null;
    this.applicationCheckPaymentCount =
      init?.applicationCheckPaymentCount ?? null;
    this.applicationCheckTaxFreeCount =
      init?.applicationCheckTaxFreeCount ?? null;
    this.applicationBulletinsCheckCount =
      init?.applicationBulletinsCheckCount ?? null;
  }
}
