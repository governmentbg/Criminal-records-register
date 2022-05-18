

export class BulletinEventsGridModel {
    public id: string = null;
    public eventType: string = null;
    public createdOn: Date = null;
    public registrationNumber: string = null;
    public egn: string = null;
    public lnch: string = null;
    public ln: string = null;
    public birthDate: Date = null;
    public personName: string = null;
    public description: Date = null;
  
    constructor(init?: Partial<BulletinEventsGridModel>) {
        this.id = init?.id ?? null;
        this.eventType = init?.eventType ?? null;
        this.createdOn = init?.createdOn ?? null;
        this.registrationNumber = init?.registrationNumber ?? null;
        this.egn = init?.egn ?? null;
        this.lnch = init?.lnch ?? null;
        this.ln = init?.ln ?? null;
        this.birthDate = init?.birthDate ?? null;
        this.personName = init?.personName ?? null;
        this.description = init?.description ?? null;

    }
  }
  