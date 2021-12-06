import { APP_BASE_HREF } from '@angular/common';
import { Component, Injector } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, RouteConfigLoadEnd, RouteConfigLoadStart, Router } from '@angular/router';
import {TranslateService} from '@ngx-translate/core';
import { AuthenticatedResult, EventTypes, LoginResponse, OidcSecurityService, PublicEventsService, ValidationResult } from 'angular-auth-oidc-client';
import { NGXLogger } from 'ngx-logger';
import { NgxPermissionsService } from 'ngx-permissions';
import { filter, Observable, Subject } from 'rxjs';
import { LocalStorageService } from './shared/services/local-storage.service';

export const AUTH_PATHS = {
  MAIN: 'main',
  LOGIN: 'login',
  POSTLOGIN: 'postlogin',
  AUTHENTICATED: 'private',
  LOGIN_ERROR: 'loginerror'
};


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  private authorizing$ = new Subject<boolean>();
  public authorizing = false;

  private loadingModule$ = new Subject<boolean>();
  public loadingModule = false;
  public loadingModuleName = '';

  private navigating$ = new Subject<boolean>();
  public navigating = false;

  private validationError = '';
  protected isAuthenticated$: Observable<AuthenticatedResult>;
  private baseHref: string;

  constructor(
    public oidcSecurityService: OidcSecurityService,
    public eventService: PublicEventsService,
    private logger: NGXLogger,
    private router: Router,
    private permissionsService: NgxPermissionsService,
    private localStorageService: LocalStorageService,
    // Instantiate the service
    //  idleService: IdleService,
    private translate: TranslateService,
    injector: Injector) {
    // this language will be used as a fallback when a translation isn't found in the current language
    this.translate.setDefaultLang('bg');
    this.translate.use('bg');

    this.baseHref = injector.get<string>(APP_BASE_HREF);

    this.authorizing$.subscribe((authorizing) => {
      this.authorizing = authorizing;
    });

    this.loadingModule$.subscribe((loadingModule) => {
      this.loadingModule = loadingModule;
    });

    this.navigating$.subscribe((navigating) => {
      this.navigating = navigating;
    });

    this.eventService
      .registerForEvents()
      .pipe(filter((notification) => notification.type === EventTypes.NewAuthenticationResult))
      .subscribe((event) => {
        if (event.value.validationResult === ValidationResult.MaxOffsetExpired) {
          this.translate.get( 'authorization.maxOffsetExpired').subscribe(
            val => this.validationError = val
          )
          this.validationError = '';
        }
      }
      );

    this.router.events.subscribe(event => {
      switch (true) {
        case event instanceof RouteConfigLoadStart: {
          this.loadingModuleName = (event as RouteConfigLoadStart).route.path ?? '';
          this.loadingModule$.next(true);
          break;
        }
        case event instanceof RouteConfigLoadEnd: {
          this.loadingModule$.next(false);
          break;
        }
        case event instanceof NavigationStart: {
          this.navigating$.next(true);
          break;
        }

        case event instanceof NavigationEnd:
        case event instanceof NavigationCancel:
        case event instanceof NavigationError: {
          this.navigating$.next(false);
          break;
        }
        default: {
          break;
        }
      }
    });

    this.isAuthenticated$ = this.oidcSecurityService.isAuthenticated$;

    this.oidcSecurityService.checkAuth().subscribe({
      next: (isAuthenticated) => {
      if (isAuthenticated) {
        this.LoadPermissions(isAuthenticated);
        this.navigateToAuthorized(isAuthenticated);
      }
    },
      error: error => {
        this.logger.info(`${this.baseHref}${AUTH_PATHS.MAIN}`);
        if (error === 'no code in url') {
          this.router.navigate([`${this.baseHref}${AUTH_PATHS.MAIN}`]);
        } else {
          const path = `${this.baseHref}${AUTH_PATHS.LOGIN_ERROR}`;
          if (this.validationError !== undefined) {
            this.router.navigate([path], { queryParams: { error: this.validationError } });
          } else {
            this.router.navigate([path], { queryParams: { error } });
          }
        }
      }
    });
  }

  private LoadPermissions(loginResponse: LoginResponse) {
    this.logger.debug('Loading permissions');
    if (loginResponse?.userData?.realm_access?.roles) {
      if (Array.isArray(loginResponse?.userData?.realm_access?.roles)) {
        this.permissionsService.loadPermissions(loginResponse?.userData?.realm_access?.roles);
      } else if (loginResponse?.userData?.realm_access?.roles) {
        this.permissionsService.loadPermissions([loginResponse?.userData?.realm_access?.roles]);
      } else {
        this.permissionsService.loadPermissions([]);
      }
    }
  }

  private navigateToAuthorized(loginResponse: LoginResponse) {
    this.logger.info('navigateToAuthorized');
    if (window.location.pathname === `${this.baseHref}${AUTH_PATHS.POSTLOGIN}`) {
      if (loginResponse.isAuthenticated) {
        let path = this.localStorageService.read('redirect-custom');
        this.LoadPermissions(loginResponse);
        path = (path) ? path : `${this.baseHref}${AUTH_PATHS.MAIN}`;
        this.localStorageService.remove('redirect-custom');
        this.logger.debug(`Redirecting to ${path}`);
        let queryParams = {};
        const splittedPath = path.split('?');
        if (splittedPath.length > 1){
          path = splittedPath[0];
          // One liner from here: https://stackoverflow.com/questions/8648892/how-to-convert-url-parameters-to-a-javascript-object?page=1&tab=votes#tab-top
          queryParams = JSON.parse('{"' + decodeURI(splittedPath[1]).replace(/"/g, '\\"').replace(/&/g, '","').replace(/=/g,'":"') + '"}');
        }
        this.router.navigate([path], {queryParams: queryParams});
      }
    }
  }
}
