
export class BulletinStatusHistoryModel {
    public id: string = null;
    public oldStatus: string = null;
    public newStatus: string = null;
    public createdOn: string = null;
    public createdBy: string = null;
    public locked: string = null;
    public descr:string = null;

    constructor(init?: Partial<BulletinStatusHistoryModel>) {
      if (init) {
        this.id = init.id ?? null;
        this.oldStatus = init.oldStatus ?? null;
        this.newStatus = init.newStatus ?? null;
        this.createdOn = init.createdOn ?? null;
        this.createdBy = init.createdBy ?? null;
        this.locked = init.locked ?? null;
        this.descr = init.descr ?? null;    
      }
    }
  }
  