export class DatePrecisionModel {
  precision: string;
  date: Date;
  year: number;
  month: number;
  constructor(init?: Partial<DatePrecisionModel>) {
    this.precision = init?.precision;
    this.date = init?.date;
    this.year = init?.year;
    this.month = init?.month;
  }
}