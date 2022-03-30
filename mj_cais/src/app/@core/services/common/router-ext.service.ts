import { Injectable } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";

//https://stackoverflow.com/questions/41038970/how-to-determine-previous-page-url-in-angular#answer-48866813

@Injectable()
export class RouterExtService {
  private previousUrl: string = undefined;
  private currentUrl: string = undefined;

  constructor(public router: Router) {
    this.currentUrl = this.router.url;
    router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.previousUrl = this.currentUrl;
        this.currentUrl = event.url;
      }
    });
  }

  public getPreviousUrl() {
    return this.previousUrl;
  }
}
