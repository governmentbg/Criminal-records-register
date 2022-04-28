export class DatePrecisionModel {
  precision: string;
  date: Date;
  constructor(init?: Partial<DatePrecisionModel>) {
    this.date = init?.date;
    this.precision = init?.precision;
  }
}