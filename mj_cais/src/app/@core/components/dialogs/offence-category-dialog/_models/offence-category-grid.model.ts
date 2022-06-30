import { BaseGridModel } from "../../../../models/common/base-grid.model";

export class OffenceCategoryGridModel extends BaseGridModel{
  public name: string = null;
  public code: string = null;

  constructor(init?: Partial<OffenceCategoryGridModel>) {
    super(init);
      this.name = init?.name;
      this.code = init?.code;   
  }
}
