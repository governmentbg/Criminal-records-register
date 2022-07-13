import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class EgnUtils {
  private CONTROLS = [2, 4, 8, 5, 10, 9, 7, 3, 6];
  private _value: string;
  private _gender: string;
  private _birthday: any;

  constructor(value: string) {
    this._value = value;
    this._gender = "";
    this._birthday = {};
    this.parse();
  }

  private value() {
    return this._value;
  }

  private gender() {
    return this._gender;
  }

  private birthday() {
    return this._birthday;
  }

  private parse() {
    this._gender = ~~this._value.charAt(8) % 2 === 0 ? "m" : "f";

    const day = ~~this._value.substr(4, 2);
    let month = ~~this._value.substr(2, 2);
    let year = ~~this._value.substr(0, 2);

    if (month > 40) {
      year += 2000;
      month -= 40;
    } else if (month > 20) {
      year += 1800;
      month -= 20;
    } else {
      year += 1900;
    }

    this._birthday = { year, month, day };
  }

  private isValid(): boolean {
    if (this._value.length !== 10) return false;

    let sum = 0;
    for (let i = 0; i < this._value.length - 1; i++) {
      let num = Number(this._value.charAt(i));
      if (isNaN(num)) return false;

      sum += ~~this._value.charAt(i) * this.CONTROLS[i];
    }

    let mod = sum % 11;
    mod = mod < 10 ? mod : 0;

    return mod === ~~this._value.charAt(9);
  }

  public static isValid(value): boolean {
    var utils = new EgnUtils(value);
    var result = utils.isValid();
    return result;
  }
}
