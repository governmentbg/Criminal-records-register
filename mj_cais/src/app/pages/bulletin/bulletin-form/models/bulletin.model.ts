import { AddressModel } from "../../../../@core/components/forms/address-form/model/address.model";
import { MultipleChooseModel } from "../../../../@core/components/forms/inputs/multiple-choose/models/multiple-choose.model";

export class BulletinModel {
  public id: string = null;
  public version: number = null;
  public csAuthorityId: string = null;
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
  //public birthCityId: string = null;
 //public birthCountryId: string = null;
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
  public nationalities: MultipleChooseModel = new MultipleChooseModel();
  public address: AddressModel = new AddressModel();

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
      this.decidingAuthId = init.decidingAuthId ?? null;
      this.decisionTypeId = init.decisionTypeId ?? null;
      this.caseTypeId = init.caseTypeId ?? null;
      this.caseNumber = init.caseNumber ?? null;
      this.caseYear = init.caseYear ?? null;
      this.convRemarks = init.convRemarks ?? null;
      this.alphabeticalIndex = init.alphabeticalIndex ?? null;
      this.decisionEcli = init.decisionEcli ?? null;
      this.bulletinCreateDate = init.bulletinCreateDate ?? null;
      this.bulletinReceivedDate = init.bulletinReceivedDate ?? null;
      this.bulletinAuthorityId = init.bulletinAuthorityId ?? null;
      this.createdByNames = init.createdByNames ?? null;
      this.approvedByNames = init.approvedByNames ?? null;
      this.approvedByPosition = init.approvedByPosition ?? null;
      this.statusId = init.statusId ?? null;
      this.firstname = init.firstname ?? null;
      this.surname = init.surname ?? null;
      this.familyname = init.familyname ?? null;
      this.fullname = init.fullname ?? null;
      this.firstnameLat = init.firstnameLat ?? null;
      this.surnameLat = init.surnameLat ?? null;
      this.familynameLat = init.familynameLat ?? null;
      this.sex = init.sex ?? null;
      this.egn = init.egn ?? null;
      this.ln = init.ln ?? null;
      this.lnch = init.lnch ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthDatePrecision = init.birthDatePrecision ?? null;
      //this.birthCityId = init.birthCityId ?? null;
      //this.birthCountryId = init.birthCountryId ?? null;
      this.birthPlaceOther = init.birthPlaceOther ?? null;
      this.fullnameLat = init.fullnameLat ?? null;
      this.idDocNumber = init.idDocNumber ?? null;
      this.idDocCategoryId = init.idDocCategoryId ?? null;
      this.idDocTypeDescr = init.idDocTypeDescr ?? null;
      this.idDocIssuingAuthority = init.idDocIssuingAuthority ?? null;
      this.idDocIssuingDate = init.idDocIssuingDate ?? null;
      this.idDocIssuingDatePrec = init.idDocIssuingDatePrec ?? null;
      this.idDocValidDate = init.idDocValidDate ?? null;
      this.idDocValidDatePrec = init.idDocValidDatePrec ?? null;
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
      this.bulletinType = init.bulletinType ?? null;
      this.nationalities = init.nationalities ?? new MultipleChooseModel(),
      this.address = init.address ?? new AddressModel()
    }
  }
}
