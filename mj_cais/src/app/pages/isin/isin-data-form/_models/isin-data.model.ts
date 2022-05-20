import { BaseModel } from "../../../../@core/models/common/base.model";

export class IsinDataModel extends BaseModel {
  public msgDateTime: Date = null;
  public status: string = null;
  public identifier: string = null;
  public firstName: string = null;
  public surName: string = null;
  public familyName: string = null;
  public birthDate: Date = null;
  public sex: string = null;
  public country1Name: string = null;
  public country2Name: string = null;
  public birthCountryName: string = null;
  public birthPlace: string = null;

  public decisionType: string = null;
  public decisionNumber: string = null;
  public decisionDate: Date = null;
  public decisionFinalDate: Date = null;
  public decisionAuthName: string = null;

  public caseType: string = null;
  public caseNumber: string = null;
  public caseYear: number = null;
  public caseAuthName: string = null;

  public sanctionStartDate: Date = null;
  public sanctionEndDate: Date = null;

  constructor(init?: Partial<IsinDataModel>) {
    super(init);
    if (init) {
      this.msgDateTime = init.msgDateTime ?? null;
      this.status = init.status ?? null;
      this.identifier = init.identifier ?? null;
      this.firstName = init.firstName ?? null;
      this.surName = init.surName ?? null;
      this.familyName = init.familyName ?? null;
      this.birthDate = init.birthDate ?? null;
      this.sex = init.sex ?? null;
      this.country1Name = init.country1Name ?? null;
      this.country2Name = init.country2Name ?? null;
      this.birthCountryName = init.birthCountryName ?? null;
      this.birthPlace = init.birthPlace ?? null;
      this.decisionType = init.decisionType ?? null;
      this.decisionNumber = init.decisionNumber ?? null;
      this.decisionDate = init.decisionDate ?? null;
      this.decisionFinalDate = init.decisionFinalDate ?? null;
      this.decisionAuthName = init.decisionAuthName ?? null;
      this.caseType = init.caseType ?? null;
      this.caseNumber = init.caseNumber ?? null;
      this.caseYear = init.caseYear ?? null;
      this.caseAuthName = init.caseAuthName ?? null;
      this.sanctionStartDate = init.sanctionStartDate ?? null;
      this.sanctionEndDate = init.sanctionEndDate ?? null;
    }
  }
}
