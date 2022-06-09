import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class IsinDataGridModel extends BaseGridModel {
  public msgDateTime: Date = null;
  public identifier: string = null;
  public birthDate: Date = null;
  public birthCountryName: string = null;
  public personName: string = null;
  public nationalities: string[] = null;
  public decisionInfo: string = null;
  public caseInfo: string = null;
  public sanctionEndDate: Date = null;
  public sanctionStartDate: Date = null;
  public bulletinId: string = null;
  public sourceType: string = null;
  public sanctionType: string = null;

  constructor(init?: Partial<IsinDataGridModel>) {
    super(init);
      this.msgDateTime = init?.msgDateTime ?? null;
      this.identifier = init?.identifier ?? null;
      this.birthDate = init?.birthDate ?? null;
      this.birthCountryName = init?.birthCountryName ?? null;
      this.personName = init?.personName ?? null;
      this.nationalities = init?.nationalities ?? null;
      this.decisionInfo = init?.decisionInfo ?? null;
      this.caseInfo = init?.caseInfo ?? null;
      this.sanctionEndDate = init?.sanctionEndDate ?? null;
      this.sanctionStartDate = init?.sanctionStartDate ?? null;
      this.bulletinId = init?.bulletinId ?? null;
      this.sourceType = init?.sourceType ?? null;
      this.sanctionType = init?.sanctionType ?? null;
  }
}
