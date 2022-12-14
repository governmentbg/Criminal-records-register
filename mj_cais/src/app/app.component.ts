import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import {
  changei18n,
  getCurrentResourceStrings,
} from "@infragistics/igniteui-angular";
import { NbAuthService } from "@nebular/auth";
import { TranslateService } from "@ngx-translate/core";
import { IgxResourceStringsBG } from "igniteui-angular-i18n";
import { NgxPermissionsService } from "ngx-permissions";
import { map, of, switchMap, tap } from "rxjs";
import { UserAuthorityService } from "./@core/services/common/user-authority.service";
import { UserInfoService } from "./@core/services/common/user-info.service";

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
    private userInfoService: UserInfoService,
    translate: TranslateService) {
    // this language will be used as a fallback when a translation isn't found in the current language
    translate.setDefaultLang("bg");

    // the lang to use, if the lang isn't available, it will use the current loader to get them
    translate.use("bg");
  }

  ngOnInit(): void {

    changei18n(IgxResourceStringsBG);

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
        if (data?.CsAuthorityId) {
          this.userAuthorityService.csAuthorityId = data?.CsAuthorityId;
          this.userInfoService.csAuthorityId = data?.CsAuthorityId;
        }
        if (data?.sub) {
          this.userInfoService.userId = data?.sub;
        }
      })
    ).subscribe();
  }
}
