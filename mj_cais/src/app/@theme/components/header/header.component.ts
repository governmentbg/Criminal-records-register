import { Component, Inject, OnDestroy, OnInit } from "@angular/core";
import {
  NbMediaBreakpointsService,
  NbMenuBag,
  NbMenuItem,
  NbMenuService,
  NbSidebarService,
  NbThemeService,
  NB_WINDOW,
} from "@nebular/theme";

import { map, switchMap, takeUntil } from "rxjs/operators";
import { of, Subject } from "rxjs";
import { NbAuthOAuth2Token, NbAuthResult, NbAuthService, NbTokenService } from "@nebular/auth";
import { HttpClient, HttpParams } from "@angular/common/http";
import { NGXLogger } from "ngx-logger";

@Component({
  selector: "ngx-header",
  styleUrls: ["./header.component.scss"],
  templateUrl: "./header.component.html",
})
export class HeaderComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();
  userPictureOnly: boolean = false;
  user: any;

  themes = [
    {
      value: "default",
      name: "Бяла",
    },
    {
      value: "dark",
      name: "Тъмна",
    },
  ];

  currentTheme = "default";

  userMenu = [
    // { title: "Profile" },
    { title: "Изход", data: { id: 'logout' } }
  ];

  constructor(
    private sidebarService: NbSidebarService,
    private menuService: NbMenuService,
    private themeService: NbThemeService,
    private breakpointService: NbMediaBreakpointsService,
    private authService: NbAuthService,
    private tokenService: NbTokenService,
    private logger: NGXLogger,
    private httpClient: HttpClient,
    @Inject(NB_WINDOW) protected window: any
  ) {}

  ngOnInit() {
    this.currentTheme = this.themeService.currentTheme;

   
    const { xl } = this.breakpointService.getBreakpointsMap();
    this.themeService
      .onMediaQueryChange()
      .pipe(
        map(([, currentBreakpoint]) => currentBreakpoint.width < xl),
        takeUntil(this.destroy$)
      )
      .subscribe(
        (isLessThanXl: boolean) => (this.userPictureOnly = isLessThanXl)
      );

    this.themeService
      .onThemeChange()
      .pipe(
        map(({ name }) => name),
        takeUntil(this.destroy$)
      )
      .subscribe((themeName) => (this.currentTheme = themeName));

      this.authService.isAuthenticated().pipe(
        switchMap((isAuthenticated) => {
          return isAuthenticated
            ? this.httpClient.get("/auth/connect/userinfo")
            : of({});
        }),
        map((data: any) => data?.Name)
      ).subscribe( name => this.user = {name: name});    
      
    this.menuService.onItemClick()
    .subscribe((menu: NbMenuBag) => {
      if (menu.item.data?.id === 'logout') {
        this.authService.getToken().subscribe( (token: NbAuthOAuth2Token) => {
          const t = token.getPayload();
          this.authService.logout('eauth')
          .pipe(takeUntil(this.destroy$))
          .subscribe((authResult: NbAuthResult) => {   
            if (authResult.isSuccess()){
              let params = new HttpParams();
              params = params.set('id_token_hint', t.id_token);
              params = params.set('post_logout_redirect_uri', window.location.origin);
              this.window.location.href = `${window.location.origin}/auth/connect/endsession?${params.toString()}`;
            }           
          });
        });
      }
    });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  changeTheme(themeName: string) {
    this.themeService.changeTheme(themeName);
  }

  toggleSidebar(): boolean {
    this.sidebarService.toggle(true, "menu-sidebar");

    return false;
  }

  navigateHome() {
    this.menuService.navigateHome();
    return false;
  }
}
