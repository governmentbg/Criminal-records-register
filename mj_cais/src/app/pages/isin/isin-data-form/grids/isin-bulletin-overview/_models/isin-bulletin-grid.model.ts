import { BaseModel } from "../../../../../../@core/models/common/base.model";

export class IsinBulletinGridModel extends BaseModel {
  public bulletinType: string = null;
  public registrationNumber: string = null;
  public birthDate: Date = null;
  public identifier: string = null;
  public nationalities: string[] = null;
  public birthPlace: string = null;
  public personName: string = null;
  public decisionType: string = null;
  public decisionNumber: string = null;
  public decisionDate: Date = null;
  public caseNumber: string = null;
  public decisionAuthName: string = null;

  constructor(init?: Partial<IsinBulletinGridModel>) {
    super(init);
    if (init) {
      this.bulletinType = init.bulletinType ?? null;
      this.registrationNumber = init.registrationNumber ?? null;
      this.birthDate = init.birthDate ?? null;
      this.identifier = init.identifier ?? null;
      this.nationalities = init.nationalities ?? null;
      this.birthPlace = init.birthPlace ?? null;
      this.personName = init.personName ?? null;
      this.decisionType = init.decisionType ?? null;
      this.decisionNumber = init.decisionNumber ?? null;
      this.decisionDate = init.decisionDate ?? null;
      this.caseNumber = init.caseNumber ?? null;
      this.decisionAuthName = init.decisionAuthName ?? null;
    }
  }
}
