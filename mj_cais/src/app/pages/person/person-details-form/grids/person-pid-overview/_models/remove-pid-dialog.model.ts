export class RemovePidDialogFrom {
  public existinPersonId: string = null;
  public pidId: Date = null;
  public firstname: string = null;
  public surname: string = null;
  public familyname: string = null;
  public fullname: string = null;
  public firstnameLat: string = null;
  public surnameLat: string = null;
  public familynameLat: string = null;
  public fullnameLat: string = null;
  public sex: string = null;
  public birthDate: string = null;
  public nationalities: string[] = null;
  public motherFirstname: string = null;
  public motherSurname: string = null;
  public motherFamilyname: string = null;
  public motherFullname: string = null;
  public fatherFirstname: string = null;
  public fatherSurname: string = null;
  public fatherFamilyname: string = null;
  public fatherFullname: string = null;

  constructor(init?: Partial<RemovePidDialogFrom>) {
    this.existinPersonId = init?.existinPersonId ?? null;
    this.pidId = init?.pidId ?? null;
    this.firstname = init?.firstname ?? null;
    this.surname = init?.surname ?? null;
    this.familyname = init?.familyname ?? null;
    this.fullname = init?.fullname ?? null;
    this.firstnameLat = init?.firstnameLat ?? null;
    this.surnameLat = init?.surnameLat ?? null;
    this.familynameLat = init?.familynameLat ?? null;
    this.fullnameLat = init?.fullnameLat ?? null;
    this.sex = init?.sex ?? null;
    this.birthDate = init?.birthDate ?? null;
    this.nationalities = init?.nationalities ?? null;
    this.motherFirstname = init?.motherFirstname ?? null;
    this.motherSurname = init?.motherSurname ?? null;
    this.motherFamilyname = init?.motherFamilyname ?? null;
    this.motherFullname = init?.motherFullname ?? null;
    this.fatherFirstname = init?.fatherFirstname ?? null;
    this.fatherSurname = init?.fatherSurname ?? null;
    this.fatherFamilyname = init?.fatherFamilyname ?? null;
    this.fatherFullname = init?.fatherFullname ?? null;
  }
}
