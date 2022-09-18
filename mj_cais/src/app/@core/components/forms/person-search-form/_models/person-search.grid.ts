import { BaseGridModel } from "../../../../models/common/base-grid.model";

export class PersonSearchGridModel extends BaseGridModel {
  public pids: string = null;
  public personNames: string = null;
  public motherNames: string = null;
  public fatherNames: string = null;
  public sex: string = null;
  public birthDate: string = null;
  public bgCitizen: string = null;
  public nonBGCitizen: string = null;
  public isConvicted: string = null;
  public egnMatch: string = null;
  public lnchMatch: string = null;
  public firstnameMatch: string = null;
  public surnameMatch: string = null;
  public familynameMatch: string = null;
  public fullnameMatch: string = null;
  public birthdateMatch: string = null;
  public birthyearMatch: string = null;
  public initialsMatch: string = null;
  public twoWordsOfNameMatch: string = null;
  public twoInitialsOfNameMatch: string = null;
  public nonBGCitizenMatch: string = null;

  constructor(init?: Partial<PersonSearchGridModel>) {
    super(init);
    this.pids = init?.pids ?? null;
    this.personNames = init?.personNames ?? null;
    this.motherNames = init?.motherNames ?? null;
    this.fatherNames = init?.fatherNames ?? null;
    this.sex = init?.sex ?? null;
    this.birthDate = init?.birthDate ?? null;
    this.bgCitizen = init?.bgCitizen ?? null;
    this.nonBGCitizen = init?.nonBGCitizen ?? null;
    this.isConvicted = init?.isConvicted ?? null;
    this.egnMatch = init?.egnMatch ?? null;
    this.lnchMatch = init?.lnchMatch ?? null;
    this.firstnameMatch = init?.firstnameMatch ?? null;
    this.surnameMatch = init?.surnameMatch ?? null;
    this.familynameMatch = init?.familynameMatch ?? null;
    this.fullnameMatch = init?.fullnameMatch ?? null;
    this.birthdateMatch = init?.birthdateMatch ?? null;
    this.birthyearMatch = init?.birthyearMatch ?? null;
    this.initialsMatch = init?.initialsMatch ?? null;
    this.twoWordsOfNameMatch = init?.twoWordsOfNameMatch ?? null;
    this.twoInitialsOfNameMatch = init?.twoInitialsOfNameMatch ?? null;
    this.nonBGCitizenMatch = init?.nonBGCitizenMatch ?? null;
  }
}
