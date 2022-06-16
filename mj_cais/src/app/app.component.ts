import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import {
  changei18n,
  getCurrentResourceStrings,
} from "@infragistics/igniteui-angular";
import { NbAuthService } from "@nebular/auth";
import { TranslateService } from "@ngx-translate/core";
import { NgxPermissionsService } from "ngx-permissions";
import { map, of, switchMap, tap } from "rxjs";
import { UserAuthorityService } from "./@core/services/common/user-authority.service";

@Component({
  selector: "ngx-app",
  template: `<ngx-spinner
      bdColor="rgba(0, 0, 0, 0.8)"
      size="default"
      color="#fff"
      type="ball-spin-clockwise"
      ><p style="color: white">Зареждане...</p></ngx-spinner
    >
    <router-outlet></router-outlet>`,
})
export class AppComponent implements OnInit {
  constructor(
    private http: HttpClient, 
    private permissionsService: NgxPermissionsService,
    private authService: NbAuthService,
    private userAuthorityService: UserAuthorityService,
    translate: TranslateService) {
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
      
    this.authService.onTokenChange().pipe(
      switchMap((tkn) => {
        return tkn.isValid()
          ? this.http.get("/auth/connect/userinfo")
          : of({});
      }),
      tap((data: any) => {
        if (Array.isArray(data?.role)) {
          this.permissionsService.loadPermissions(data.role);
        } else if (data?.role) {
          this.permissionsService.loadPermissions([data.role]);
        } else {
          this.permissionsService.loadPermissions([]);
        }
        if (data?.CsAuthorityId){
          this.userAuthorityService.csAuthorityId = data?.CsAuthorityId;
        }
      })
    ).subscribe();    
  }
}
