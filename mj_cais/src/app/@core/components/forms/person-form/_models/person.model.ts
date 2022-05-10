import { AddressModel } from "../../address-form/model/address.model";
import { MultipleChooseModel } from "../../inputs/multiple-choose/models/multiple-choose.model";

export class PersonModel {

  public id: string = null;
  public contextType: string = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public fullname: string = null;
  public firstnameLat: string = null;
  public surnameLat: string = null;
  public familynameLat: string = null;
  public fullnameLat: string = null;
  public sex: number = null;
  public birthDate: Date = null;
  public birthPlace: AddressModel = new AddressModel();
  public egn: string = null;
  public lnch: string = null;
  public ln: string = null;
  public afisNumber: string = null;
  public idDocNumber: string = null;
  public idDocCategoryId: string = null;
  public idDocTypeDescr: string = null;
  public idDocIssuingAuthority: string = null;
  public idDocIssuingDate: Date = null;
  public idDocValidDate: Date = null;
  public motherFirstname: string = null;
  public motherSurname: string = null;
  public motherFamilyname: string = null;
  public motherFullname: string = null;
  public fatherFirstname: string = null;
  public fatherSurname: string = null;
  public fatherFamilyname: string = null;
  public fatherFullname: string = null;
  public nationalities: MultipleChooseModel = new MultipleChooseModel();

  constructor(init?: Partial<PersonModel>) {
    this.firstname = init?.firstname ?? null;
    this.contextType = init?.contextType ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
    this.fullname = init?.fullname ?? null;
    this.firstnameLat = init?.firstnameLat ?? null;
    this.surnameLat = init?.surnameLat ?? null;
    this.familynameLat = init?.familynameLat ?? null;
    this.fullnameLat = init?.fullnameLat ?? null;
    this.sex = init?.sex ?? null;
    this.birthDate = init?.birthDate ?? null;
    this.birthPlace = init?.birthPlace ?? null;
    this.egn = init?.egn ?? null;
    this.lnch = init?.lnch ?? null;
    this.ln = init?.ln ?? null;
    this.afisNumber = init?.afisNumber ?? null;
    this.idDocNumber = init?.idDocNumber ?? null;
    this.idDocCategoryId = init?.idDocCategoryId ?? null;
    this.idDocTypeDescr = init?.idDocTypeDescr ?? null;
    this.idDocIssuingAuthority = init?.idDocIssuingAuthority ?? null;
    this.idDocIssuingDate = init?.idDocIssuingDate ?? null;
    this.idDocValidDate = init?.idDocValidDate ?? null;
    this.motherFirstname = init?.motherFirstname ?? null;
    this.motherSurname = init?.motherSurname ?? null;
    this.motherFamilyname = init?.motherFamilyname ?? null;
    this.motherFullname = init?.motherFullname ?? null;
    this.fatherFirstname = init?.fatherFirstname ?? null;
    this.fatherSurname = init?.fatherSurname ?? null;
    this.fatherFamilyname = init?.fatherFamilyname ?? null;
    this.fatherFullname = init?.fatherFullname ?? null;
    this.nationalities = init?.nationalities ?? null;
  }
}
