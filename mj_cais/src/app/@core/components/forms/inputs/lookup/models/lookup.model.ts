export class LookupModel {
    id: number = null;
    displayName: string = null;
  
    constructor(init?: Partial<LookupModel>) {
      if (init) {
        this.id = init.id;
        this.displayName = init.displayName;
      }
    }
  }
  