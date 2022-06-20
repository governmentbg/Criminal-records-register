export class IsinCountModel {
  public new: number = null;
  public identified: number = null;

  constructor(init?: Partial<IsinCountModel>) {
    this.new = init?.new ?? null;
    this.identified = init?.identified ?? null;
  }
}