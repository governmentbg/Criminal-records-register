import { Injectable } from "@angular/core";
import { CanLoad, Route, Router, UrlSegment } from "@angular/router";
import { AuthenticatedResult, OidcSecurityService } from "angular-auth-oidc-client";
import { NGXLogger } from "ngx-logger";
import { map, Observable, Subject, take } from "rxjs";
import { LocalStorageService } from "../services/local-storage.service";

@Injectable({
  providedIn: 'root'
})
export class AuthorizationGuard implements CanLoad {

    private loader$ = new Subject<boolean>();
    public loader = false;

    constructor(
      private router: Router,
      private localStorageService: LocalStorageService,
      private oidcSecurityService: OidcSecurityService,
      private logger: NGXLogger) {
      this.loader$.subscribe(loader => {
          this.loader = loader;
        }
      );
    }
    canLoad(
      route: Route,
      segments: UrlSegment[]): boolean | Observable<boolean> | Promise<boolean> {
        const navigation = this.router.getCurrentNavigation();
        let url = '/';
        if (navigation) {
          url = navigation.extractedUrl.toString();
        }
        return this.canLoadImpl(url);
    }

    canLoadImpl(url: string): Observable<boolean> {
      this.loader$.next(true);
      return this.oidcSecurityService.isAuthenticated$.pipe(
          take(1),
          map((isAuthorized: AuthenticatedResult) => {
            this.loader$.next(false);
            if (!isAuthorized.isAuthenticated) {
              this.logger.debug(`write redirect url (${url}) in auth.guard`);
              this.localStorageService.write('redirect-custom', url);
              this.router.navigate(['/', 'login']);
            }
            return isAuthorized.isAuthenticated;
          })
        );
    }
  }
