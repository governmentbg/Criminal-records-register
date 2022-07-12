import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class LnchUtils {
  private CONTROLS = [21, 19, 17, 13, 11, 9, 7, 3, 1];
  private _value: string;

  constructor(value: string) {
    this._value = value;
  }

  private value() {
    return this._value;
  }

  private isValid() {
    if (this._value.length !== 10) {
      return false;
    }

    let sum = 0;

    for (let i = 0; i < this._value.length - 1; i++) {
      let num = Number(this._value.charAt(i));
      if (isNaN(num)) return false;
      sum += ~~this._value.charAt(i) * this.CONTROLS[i];
    }

    const mod = sum % 10;
    return mod === ~~this._value.charAt(9);
  }

  public static isValid(value): boolean {
    var result = new LnchUtils(value).isValid();
    return result;
  }
}
