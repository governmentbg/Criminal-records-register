import { Injectable } from "@angular/core";
import { NgxSpinnerService } from "ngx-spinner";

@Injectable({
  providedIn: "root",
})
export class LoaderService {
  constructor(private spinner: NgxSpinnerService) {}
  private interval;

  public showSpinner(service) {
    service.isLoading = true;
    this.spinner.show();
    this.interval = setInterval(() => {
      let isHiden = this.hideSpinner(service);
      if (isHiden) {
        clearInterval(this.interval);
      }
    }, 500);
  }

  public show() {
    this.spinner.show();
  }

  public hide() {
    this.spinner.hide();
  }

  public hideSpinner(service): boolean {
    if (!service.isLoading) {
      this.spinner.hide();
      return true;
    }
    return false;
  }

  public getInterval() {
    return this.interval;
  }

  public clearInterval() {
    clearInterval(this.interval);
  }
}
