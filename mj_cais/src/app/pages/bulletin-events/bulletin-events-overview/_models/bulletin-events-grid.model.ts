export class BulletinEventsGridModel {
  public id: string = null;
  public bulletinId: string = null;
  public eventType: string = null;
  public statusCode: string = null;
  public statusName: string = null;
  public createdOn: Date = null;
  public registrationNumber: string = null;
  public identifier: string = null;
  public birthDate: Date = null;
  public personName: string = null;
  public description: string = null;
  
  constructor(init?: Partial<BulletinEventsGridModel>) {
    this.id = init?.id ?? null;
    this.bulletinId = init?.bulletinId ?? null;
    this.eventType = init?.eventType ?? null;
    this.statusCode = init?.statusCode ?? null;
    this.statusName = init?.statusName ?? null;
    this.createdOn = init?.createdOn ?? null;
    this.registrationNumber = init?.registrationNumber ?? null;
    this.identifier = init?.identifier ?? null;
    this.birthDate = init?.birthDate ?? null;
    this.personName = init?.personName ?? null;
    this.description = init?.description ?? null;
  }
}
