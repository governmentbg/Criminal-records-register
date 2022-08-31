import {
  IgxFilteringOperand,
  IgxStringFilteringOperand,
} from "@infragistics/igniteui-angular";

export class ContainsOnlyFilteringOperand extends IgxFilteringOperand {
  protected constructor() {
    super();
    this.operations = [
      {
        name: "contains",
        isUnary: false,
        iconName: "contains",
        logic: (target: string, searchVal: string, ignoreCase?: boolean) => {
          const search = IgxStringFilteringOperand.applyIgnoreCase(
            searchVal,
            ignoreCase
          );
          const targetDisplayValue = IgxStringFilteringOperand.applyIgnoreCase(
            target !== null && target !== undefined ? target : "",
            ignoreCase
          );
          return targetDisplayValue.indexOf(search) !== -1;
        },
      },
    ].concat(this.operations);
  }
}
