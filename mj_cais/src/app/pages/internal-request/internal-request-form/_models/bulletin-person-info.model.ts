import { BulletinPersonAliasModel } from "../../../bulletin/bulletin-form/_models/bulletin-person-alias.model";

export class BulletinPersonInfoModel {
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
    public decisionTypeName: string;
    public decidingAuthName: string;
    public decisionNumber: string;
    public decisionDate: string;
    public caseNumber: string;
    public caseYear: string;
    public motherFullname: string;
    public fatherFullname: string;
    public country: string
    public city: string;
    public nationalities: string [];
    public foreignCountryAddress: string;
    public municipalityName: string;
    public districtname: string;
    public personAliases: BulletinPersonAliasModel[]
  }