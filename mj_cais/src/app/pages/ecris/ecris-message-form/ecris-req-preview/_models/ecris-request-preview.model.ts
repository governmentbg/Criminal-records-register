export class EcrisRequestPreviewModel {
  public id: string;
  public ecrisId: string;
  public sendingMemberState: string;
  public receivingMemberState: string;
  public requestAuthorityName: string;
  public requestAuthorityType: string;
  public requestAuthorityCode: string;
  public firstName: string;
  public middleName: string;
  public lastName: string;
  public lastNameSecond: string;
  public fullName: string;
  public nationality: string;
  public countryPerson: string;
  public municipalityPerson: string;
  public cityPerson: string;
  public personId: string;
  public sex: string;
  public firstNameFormer: string;
  public middleNameFormer: string;
  public lastNameFormer: string;
  public country: string;
  public municipality: string;
  public city: string;
  public street: string;
  public postCode: string;
  public fullAdress: string;
  public adressNumber: string;
  public requestPurposeCategory: string;
  public requestPurpose: string;
  public concernedPersonConsent: string;
  public messageUrgency: string;
  public accusationOffenceCategory: string;
  public messageAccusation: string;
  public caseRefereranceNumber: string;

  constructor(init?: Partial<EcrisRequestPreviewModel>) {
    Object.assign(this, init);
  }
}
