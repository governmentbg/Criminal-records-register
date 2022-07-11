import { BaseGridModel } from "../../../@core/models/common/base-grid.model";

export class ExportBulletinModel extends BaseGridModel {
  public registrationNumber: string = null;
  public bulletinType: string = null;
  public statusName: string = null;
  public ln: string = null;
  public lnch: string = null;
  public egn: string = null;
  public sequentialIndex: string = null;
  public decisionNumber: string = null;
  public decisionDate: Date = null;
  public decisionFinalDate: Date = null;
  public caseNumber: string = null;
  public caseYear: Date = null;
  public alphabeticalIndex: string = null;
  public decisionEcli: string = null;
  public bulletinCreateDate: Date = null;
  public bulletinReceivedDate: Date = null;
  public createdByNames: string = null;
  public approvedByNames: string = null;
  public approvedByPosition: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public fullname: string = null;
  public firstnameLat: string = null;
  public surnameLat: string = null;
  public familynameLat: string = null;
  public sex: string = null;
  public birthDate: Date = null;
  public birthPlaceOther: string = null;
  public fullnameLat: string = null;
  public idDocNumber: string = null;
  public idDocTypeDescr: string = null;
  public idDocIssuingAuthority: string = null;
  public idDocIssuingDate: Date = null;
  public idDocValidDate: Date = null;
  public motherFirstname: string = null;
  public motherFamilyname: string = null;
  public motherFullname: string = null;
  public fatherFirstname: string = null;
  public fatherSurname: string = null;
  public fatherFamilyname: string = null;
  public fatherFullname: string = null;
  public motherSurname: string = null;
  public afisNumber: string = null;
  public convIsTransmittable: string = null;
  public convRetPeriodEndDate: Date = null;
  public createdByPosition: string = null;
  public updatedOn: Date = null;
  public deleteDate: Date = null;
  public rehabilitationDate: Date = null;
  public ecrisConvictionId: string = null;
  public locked: boolean = null;
  public noSanction: boolean = null;
  public prevSuspSent: boolean = null;
  public prevSuspSentDescr: string = null;
  public suid: string = null;
  public euCitizen: boolean = null;
  public tcnCitizen: boolean = null;
  public birthCityName: string = null;
  public birthMunName: string = null;
  public birthDistrictName: string = null;
  public birthCountryName: string = null;
  public csAuthorityName: string = null;
  public caseAuthName: string = null;
  public decidingAuthName: string = null;
  public decisionTypeName: string = null;
  public caseTypeName: string = null;
  public bulletinAuthorityName: string = null;
  public createdByUsername: string = null;
  public updatedByUsername: string = null;
  public idDocCategoryName: string = null;
  public countryName1: string = null;
  public countryName2: string = null;
  public countryName3: string = null;
  public countryName4: string = null;

  constructor(init?: Partial<ExportBulletinModel>) {
    super(init);
    if (init) {
      this.registrationNumber = init.registrationNumber ?? null;
      this.bulletinType = init.bulletinType ?? null;
      this.statusName = init.statusName ?? null;
      this.ln = init.ln ?? null;
      this.lnch = init.lnch ?? null;
      this.egn = init.egn ?? null;
      this.sequentialIndex = init.sequentialIndex ?? null;
      this.decisionNumber = init.decisionNumber ?? null;
      this.decisionFinalDate = init.decisionFinalDate ?? null;
      this.caseNumber = init.caseNumber ?? null;
      this.caseYear = init.caseYear ?? null;
      this.alphabeticalIndex = init.alphabeticalIndex ?? null;
      this.decisionEcli = init.decisionEcli ?? null;
      this.bulletinCreateDate = init.bulletinCreateDate ?? null;
      this.bulletinReceivedDate = init.bulletinReceivedDate ?? null;
      this.createdByNames = init.createdByNames ?? null;
      this.approvedByNames = init.approvedByNames ?? null;
      this.approvedByPosition = init.approvedByPosition ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.fullname = init.fullname ?? null;
      this.firstnameLat = init.firstnameLat ?? null;
      this.surnameLat = init.surnameLat ?? null;
      this.familynameLat = init.familynameLat ?? null;
      this.sex = init.sex ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthPlaceOther = init.birthPlaceOther ?? null;
      this.fullnameLat = init.fullnameLat ?? null;
      this.idDocNumber = init.idDocNumber ?? null;
      this.idDocTypeDescr = init.idDocTypeDescr ?? null;
      this.idDocIssuingAuthority = init.idDocIssuingAuthority ?? null;
      this.idDocIssuingDate = init.idDocIssuingDate ?? null;
      this.idDocValidDate = init.idDocValidDate ?? null;
      this.motherFirstname = init.motherFirstname ?? null;
      this.motherFamilyname = init.motherFamilyname ?? null;
      this.motherFullname = init.motherFullname ?? null;
      this.fatherFirstname = init.fatherFirstname ?? null;
      this.fatherSurname = init.fatherSurname ?? null;
      this.fatherFamilyname = init.fatherFamilyname ?? null;
      this.fatherFullname = init.fatherFullname ?? null;
      this.motherSurname = init.motherSurname ?? null;
      this.afisNumber = init.afisNumber ?? null;
      this.convIsTransmittable = init.convIsTransmittable ?? null;
      this.convRetPeriodEndDate = init.convRetPeriodEndDate ?? null;
      this.createdByPosition = init.createdByPosition ?? null;
      this.updatedOn = init.updatedOn ?? null;
      this.deleteDate = init.deleteDate ?? null;
      this.rehabilitationDate = init.rehabilitationDate ?? null;
      this.ecrisConvictionId = init.ecrisConvictionId ?? null;
      this.locked = init.locked ?? null;
      this.noSanction = init.noSanction ?? null;
      this.prevSuspSent = init.prevSuspSent ?? null;
      this.prevSuspSentDescr = init.prevSuspSentDescr ?? null;
      this.suid = init.suid ?? null;
      this.euCitizen = init.euCitizen ?? null;
      this.tcnCitizen = init.tcnCitizen ?? null;
      this.birthCityName = init.birthCityName ?? null;
      this.birthMunName = init.birthMunName ?? null;
      this.birthDistrictName = init.birthDistrictName ?? null;
      this.birthCountryName = init.birthCountryName ?? null;
      this.csAuthorityName = init.csAuthorityName ?? null;
      this.caseAuthName = init.caseAuthName ?? null;
      this.decidingAuthName = init.decidingAuthName ?? null;
      this.decisionTypeName = init.decisionTypeName ?? null;
      this.caseTypeName = init.caseTypeName ?? null;
      this.bulletinAuthorityName = init.bulletinAuthorityName ?? null;
      this.createdByUsername = init.createdByUsername ?? null;
      this.updatedByUsername = init.updatedByUsername ?? null;
      this.idDocCategoryName = init.idDocCategoryName ?? null;
      this.countryName1 = init.countryName1 ?? null;
      this.countryName2 = init.countryName2 ?? null;
      this.countryName3 = init.countryName3 ?? null;
      this.countryName4 = init.countryName4 ?? null;
      this.decisionDate = init.decisionDate ?? null;
    }
  }
}
