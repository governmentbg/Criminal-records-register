import { PersonAliasModel } from "../../../../models/common/person-alias.model";

export class BulletinPersonInfoModel {
  public personId: string;
  public bulletinId: string;
  public bulletinReceivedDate: Date;
  public csAuthorityName: string;
  public firstname: string;
  public surname: string;
  public familyname: string;
  public firstnameLat: string;
  public surnameLat: string;
  public familynameLat: string;
  public sex: string;
  public birthDate: string;
  public egn: string;
  public lnch: string;
  public ln: string;
  public registrationNumber: string;
  public bulletinType: string;
  public decidingAuthName: string;
  public decisionNumber: string;
  public decisionDate: string;
  public caseNumber: string;
  public caseYear: string;
  public motherFirstname: string;
  public motherSurname: string;
  public motherFamilyname: string;
  public motherFullname: string;
  public fatherFirstname: string;
  public fatherSurname: string;
  public fatherFamilyname: string;
  public fatherFullname: string;
  public country: string;
  public city: string;
  public nationalities: string[];
  public foreignCountryAddress: string;
  public municipalityName: string;
  public districtname: string;
  public personAliases: PersonAliasModel[];
}