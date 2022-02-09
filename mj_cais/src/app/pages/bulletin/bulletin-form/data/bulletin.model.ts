export class BulletinModel {
  public id: string = null;
  public version: string = null;
  public csAuthorityId: number = null;
  public registrationNumber: string = null;
  public sequentialIndex: number = null;
  public decisionNumber: string = null;
  public decisionDate: Date = null;
  public decisionFinalDate: Date = null;
  public decidingAuthId: string = null;
  public decisionTypeId: string = null;
  public caseTypeId: string = null;
  public caseNumber: string = null;
  public caseYear: number = null;
  public convRemarks: string = null;
  public alphabeticalIndex: string = null;
  public decisionEcli: string = null;
  public bulletinCreateDate: Date = null;
  public bulletinReceivedDate: Date = null;
  public bulletinAuthorityId: string = null;
  public createdByNames: string = null;
  public approvedByNames: string = null;
  public approvedByPosition: string = null;
  public statusId: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public fullname: string = null;
  public firstnameLat: string = null;
  public surnameLat: string = null;
  public familynameLat: string = null;
  public sex: number = null;
  public egn: string = null;
  public ln: string = null;
  public lnch: string = null;
  public birthDate: Date = null;
  public birthDatePrecision: string = null;
  public birthCityId: string = null;
  public birthCountryId: string = null;
  public birthPlaceOther: string = null;
  public fullnameLat: string = null;
  public idDocNumber: string = null;
  public idDocCategoryId: string = null;
  public idDocTypeDescr: string = null;
  public idDocIssuingAuthority: string = null;
  public idDocIssuingDate: Date = null;
  public idDocIssuingDatePrec: string = null;
  public idDocValidDate: Date = null;
  public idDocValidDatePrec: string = null;
  public motherFirstname: string = null;
  public motherFamilyname: string = null;
  public motherFullname: string = null;
  public fatherFirstname: string = null;
  public fatherSurname: string = null;
  public fatherFamilyname: string = null;
  public fatherFullname: string = null;
  public motherSurname: string = null;
  public afisNumber: string = null;
  public convIsTransmittable: number = null;
  public convRetPeriodEndDate: Date = null;
  public createdByPosition: string = null;
  public bulletinType: string = null;

  constructor(init?: Partial<BulletinModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.version = init.version ?? null;
      this.csAuthorityId = init.csAuthorityId ?? null;
      this.registrationNumber = init.registrationNumber ?? null;
      this.sequentialIndex = init.sequentialIndex ?? null;
      this.decisionNumber = init.decisionNumber ?? null;
      this.decisionDate = init.decisionDate ?? null;
      this.decisionFinalDate = init.decisionFinalDate ?? null;
      // TODO: add other fields
    }
  }
}
