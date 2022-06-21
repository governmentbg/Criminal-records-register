export class FbbcCountModel {
    public new: number = null;
    public forDestruction: number = null;
  
    constructor(init?: Partial<FbbcCountModel>) {
      this.new = init?.new ?? null;
      this.forDestruction = init?.forDestruction ?? null;
    }
  }
  