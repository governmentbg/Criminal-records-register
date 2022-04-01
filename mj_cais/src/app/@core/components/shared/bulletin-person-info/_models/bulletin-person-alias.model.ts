export class BulletinPersonAliasModel {
    public id: string = null;
    public firstname: string = null;
    public surname: string = null;
    public familyname: string = null;
    public fullname: string = null;
    public typeId: number = null;
    public typeCode: string = null;
    public typeName: string = null;
  
    constructor(init?: Partial<BulletinPersonAliasModel>) {
      if (init) {
        this.id = init.id ?? null;
        this.firstname = init.firstname ?? null;
        this.surname = init.surname ?? null;
        this.familyname = init.familyname ?? null;
        this.fullname = init.fullname ?? null;
        this.typeId = init.typeId ?? null;
        this.typeCode = init.typeCode ?? null;
        this.typeName = init.typeName ?? null;
      }
    }
  }
  