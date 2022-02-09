export class SelectListModel {
  public text: string;
  public value: number;
  public selected: boolean;
  public code: string;

  constructor(init?: Partial<SelectListModel>) {
    if (init) {
      this.text = init.text;
      this.value = init.value;
      this.selected = init.selected;
      this.code = init.code;
    }
  }
}
