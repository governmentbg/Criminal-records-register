import { BaseModel } from "../../../../@core/models/common/base.model";

export class EcrisMsgNameModel extends BaseModel {
    public firstname: string = null;
    public surname: string = null;
    public familyname: string = null;
  
    constructor(init?: Partial<EcrisMsgNameModel>) {
      super(init);
      if (init) {
        this.firstname = init.firstname ?? null;
        this.surname = init.surname ?? null;
        this.familyname = init.familyname ?? null;
      }
    }
  }