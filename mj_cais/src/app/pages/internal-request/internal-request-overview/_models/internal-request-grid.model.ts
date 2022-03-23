

export class InternalRequestGridModel {

    public id: string = null;
    public regNumber: string = null;
    public requestDate: string = null;
    public reqStatus: string = null;
    public description: string = null;
    public bulletinNumber: string = null;
    public firstname: Date = null;
    public surname: string = null;
    public familyname: string = null;

    constructor(init?: Partial<InternalRequestGridModel>) {
      if (init) {
        this.id = init.id ?? null;
        this.regNumber = init.regNumber ?? null;
        this.requestDate = init.requestDate ?? null;
        this.reqStatus = init.reqStatus ?? null;
        this.description = init.description ?? null;
        this.bulletinNumber = init.bulletinNumber ?? null;
        this.firstname = init.firstname ?? null;
        this.surname = init.surname ?? null;
        this.familyname = init.familyname ?? null;
      }
    }
  }
  