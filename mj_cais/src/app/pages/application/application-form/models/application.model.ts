import { FormControl, FormGroup, Validators } from "@angular/forms";

export class ApplicationModel {
  public id: string = null;
  public registrationNumber: string = null;
  public purpose: string = null;
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
  public personId: string = null;
  public applicantName: string = null;
  public address: string = null;
  public motherFirstname: string = null;
  public motherSurname: string = null;
  public motherFamilyname: string = null;
  public fatherFirstname: string = null;
  public fatherSurname: string = null;
  public fatherFamilyname: string = null;
  public motherFullname: string = null;
  public fatherFullname: string = null;
  public purposeCountry: string = null;
  public purposePosition: string = null;
  public srvcResRcptMethId: string = null;
  public addrName: string = null;
  public addrStr: string = null;
  public addrDistrict: string = null;
  public addrTown: string = null;
  public addrState: string = null;
  public addrPhone: string = null;
  public addrEmail: string = null;
  public description: string = null;
  public email: string = null;
  public applicationTypeId: string = null;
  public csAuthorityId: string = null;
  public isLocal: boolean = null;
  public version: number = null;
  public purposeId: string = null;
  public paymentMethodId: string = null;
  public fromCosul: boolean = null;
  public docContentId: string = null;
  public statusCode: string = null;
  public birthDate: Date = null;
  public birthDatePrecision: string = null;
  public birthCountryId: string = null;
  public birthCityId: string = null;
  public birthPlaceOther: string = null;
  public userCitizenId: string = null;
  public userId: string = null;
  public userExtId: string = null;

  constructor(init?: Partial<ApplicationModel>) {
    if (init) {
      this.id = init.id ?? null;
      this.registrationNumber = init.registrationNumber ?? null;
      this.purpose = init.purpose ?? null;
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
      this.personId = init.personId ?? null;
      this.applicantName = init.applicantName ?? null;
      this.address = init.address ?? null;
      this.motherFirstname = init.motherFirstname ?? null;
      this.motherSurname = init.motherSurname ?? null;
      this.motherFamilyname = init.motherFamilyname ?? null;
      this.fatherFirstname = init.fatherFirstname ?? null;
      this.fatherSurname = init.fatherSurname ?? null;
      this.fatherFamilyname = init.fatherFamilyname ?? null;
      this.motherFullname = init.motherFullname ?? null;
      this.fatherFullname = init.fatherFullname ?? null;
      this.purposeCountry = init.purposeCountry ?? null;
      this.purposePosition = init.purposePosition ?? null;
      this.srvcResRcptMethId = init.srvcResRcptMethId ?? null;
      this.addrName = init.addrName ?? null;
      this.addrStr = init.addrStr ?? null;
      this.addrDistrict = init.addrDistrict ?? null;
      this.addrTown = init.addrTown ?? null;
      this.addrState = init.addrState ?? null;
      this.addrPhone = init.addrPhone ?? null;
      this.addrEmail = init.addrEmail ?? null;
      this.description = init.description ?? null;
      this.email = init.email ?? null;
      this.applicationTypeId = init.applicationTypeId ?? null;
      this.csAuthorityId = init.csAuthorityId ?? null;
      this.isLocal = init.isLocal ?? null;
      this.version = init.version ?? null;
      this.purposeId = init.purposeId ?? null;
      this.paymentMethodId = init.paymentMethodId ?? null;
      this.fromCosul = init.fromCosul ?? null;
      this.docContentId = init.docContentId ?? null;
      this.statusCode = init.statusCode ?? null;
      this.birthDate = init.birthDate ?? null;
      this.birthDatePrecision = init.birthDatePrecision ?? null;
      this.birthCountryId = init.birthCountryId ?? null;
      this.birthCityId = init.birthCityId ?? null;
      this.birthPlaceOther = init.birthPlaceOther ?? null;
      this.userCitizenId = init.userCitizenId ?? null;
      this.userId = init.userId ?? null;
      this.userExtId = init.userExtId ?? null;
    }
  }
}
