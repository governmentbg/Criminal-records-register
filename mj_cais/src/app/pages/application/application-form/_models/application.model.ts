import { AddressModel } from "../../../../@core/components/forms/address-form/_model/address.model";
import { PersonModel } from "../../../../@core/components/forms/person-form/_models/person.model";
import { BaseModel } from "../../../../@core/models/common/base.model";

export class ApplicationModel extends BaseModel {
  public registrationNumber: string = null;
  public purpose: string = null;
  public currentApplicationStatus: string = null;

  public personId: string = null;
  public applicantName: string = null;
  public address: string = null;

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
  public applicationCode: string = null;
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
  public birthAddress: AddressModel = new AddressModel();
  public person: PersonModel;

  constructor(init?: Partial<ApplicationModel>) {
    super(init);
    if (init) {
      this.registrationNumber = init.registrationNumber ?? null;
      this.purpose = init.purpose ?? null;
      this.currentApplicationStatus = init.currentApplicationStatus ?? null;
      this.personId = init.personId ?? null;
      this.person = init.person ?? null;
      this.applicantName = init.applicantName ?? null;
      this.address = init.address ?? null;
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
      this.applicationCode = init.applicationCode ?? null;
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
      this.birthAddress = init.birthAddress ?? new AddressModel();
    }
  }
}
