import { Injectable } from "@angular/core";
import { AbstractControl } from "@angular/forms";
import { IgxGridComponent, Transaction } from "@infragistics/igniteui-angular";
import { NbAutocompleteDirective, NbOptionComponent } from "@nebular/theme";
import { SelectListModel } from "../models/common/select-list.model";
import { BaseNomenclatureModel } from "../models/nomenclature/base-nomenclature.model";

@Injectable({
  providedIn: "root",
})
export class FormUtils {
  constructor() {}

  public static newIdCounter = 0; // TODO: remove

  public static removeItem<T>(arr: Array<T>, value: T): Array<T> {
    const index = arr.indexOf(value);
    if (index > -1) {
      arr.splice(index, 1);
    }
    return arr;
  }

  public hasRequiredField = (abstractControl: AbstractControl): boolean => {
    if (abstractControl.validator) {
      const validator = abstractControl.validator({} as AbstractControl);
      if (validator && validator.required) {
        return true;
      }
    }
    return false;
  };

  public static mapNomenclatureToSelectList(
    baseNomenclatures: BaseNomenclatureModel[]
  ): SelectListModel[] {
    if (!baseNomenclatures) {
      return [];
    }

    return baseNomenclatures.map((baseNomenclature: BaseNomenclatureModel) => {
      return new SelectListModel({
        text: baseNomenclature.name,
        value: baseNomenclature.id,
      });
    });
  }

  public static handleNewRowId(event, grid: IgxGridComponent) {
    if (event.isAddRow) {
      let currentNewId = this.newIdCounter--;
      let transactionsContainer = grid.transactions as any;
      let transactions = transactionsContainer._transactions;

      let currentTransaction = transactions.find(
        (t) => t.id == event.rowID
      ) as Transaction;
      let state = transactionsContainer._states.get(event.rowID);

      state.value.id = currentNewId;
      currentTransaction.newValue.id = currentNewId;
      currentTransaction.id = currentNewId;
      transactionsContainer._states.delete(event.rowID);
      transactionsContainer._states.set(currentNewId, state);
    }
  }

  public static setOptionNbSelect(
    selectComponent: NbAutocompleteDirective<string>,
    optToCompare: any
  ) {
    setTimeout(() => {
      if (selectComponent) {
        const selectedOptions: NbOptionComponent[] = [];
        for (const option of selectComponent.autocomplete.options["_results"]) {
          if (optToCompare.value === option["value"]) {
            selectedOptions.push(option);
            break;
          }
        }
        for (const option of selectedOptions) {
          selectComponent.writeValue(option.value);
        }
        selectComponent["cd"].detectChanges();
      }
    });
  }

  public static getGridItemById(grid: IgxGridComponent, id: any) {
    var primaryKey = grid.primaryKey;
    var items = grid.data;
    var result = items.filter((item) => {
      return item[primaryKey] == id;
    });

    if (result.length) {
      return result[0];
    }

    return null;
  }
}
