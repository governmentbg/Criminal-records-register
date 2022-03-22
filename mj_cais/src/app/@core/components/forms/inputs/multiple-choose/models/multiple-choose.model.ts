export class MultipleChooseModel {

    public selectedPrimaryKeys: string[] | number [];
    public selectedForeignKeys: string[] | number [];
  
    constructor(init?: Partial<MultipleChooseModel>) {
      if (init) {
        this.selectedPrimaryKeys = init.selectedPrimaryKeys;
        this.selectedForeignKeys = init.selectedForeignKeys;
      }
    }
  }
  