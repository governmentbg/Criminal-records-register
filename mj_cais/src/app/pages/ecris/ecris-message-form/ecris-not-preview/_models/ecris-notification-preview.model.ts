export class EcrisNotificationPreviewModel {
  public id: string;
  public ecrisId: string;

  public firstName: string;
  public middleName: string;
  public lastName: string;
  public lastNameSecond: string;
  public fullName: string;
  public nationality: string;
  public birthday: string;
  public countryPerson: string;
  public municipalityPerson: string;
  public cityPerson: string;
  public personId: string;
  public sex: string;

  public country: string;
  public municipality: string;
  public city: string;
  public street: string;
  public postCode: string;
  public fullAdress: string;
  public adressNumber: string;
  public convictionSanctions: EcrisNotificationSanctionsModel[];
  public decisions: EcrisNotificationDescisionModel[];

  constructor(init?: Partial<EcrisNotificationPreviewModel>) {
    Object.assign(this, init);
  }
}

export class EcrisNotificationSanctionsModel {
  public commonCategory?: any;
  public alternative?: any;
  public nationalCategoryTitle: string;
  public convictionStartDate?: any;
  public convictionEndDate?: any;
  public convictionDuration?: any;
  public sanctionAmountOfIndividualFine: string;
  public remarks?: any;
  public sanctionIsSpecificToMinor: string;
  public sanctionNumberOfFines: string;
  public sanctionCurrencyOfFine: string;

  constructor(init?: Partial<EcrisNotificationSanctionsModel>) {
    Object.assign(this, init);
  }
}

export class EcrisNotificationDescisionModel {
  public decisionDate?: any;
  public decisionChangeType?: any;
  public decidingAuthorityName: string;

  constructor(init?: Partial<EcrisNotificationDescisionModel>) {
    Object.assign(this, init);
  }
}
