import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import {
  changei18n,
  getCurrentResourceStrings,
} from "@infragistics/igniteui-angular";
import { TranslateService } from "@ngx-translate/core";

@Component({
  selector: "ngx-app",
  template: "<router-outlet></router-outlet>",
})
export class AppComponent implements OnInit {
  constructor(private http: HttpClient, translate: TranslateService) {
    // this language will be used as a fallback when a translation isn't found in the current language
    translate.setDefaultLang("bg");

    // the lang to use, if the lang isn't available, it will use the current loader to get them
    translate.use("bg");
  }

  ngOnInit(): void {
    this.http
      .get("assets/ignite-ui.localization.json")
      .subscribe((data: any) => {
        const currentRS = getCurrentResourceStrings();

        for (const key of Object.keys(data)) {
          currentRS[key] = data[key];
        }
        changei18n(currentRS);
      });
  }
}
