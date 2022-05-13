import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { NbAuthResult, NbAuthService } from '@nebular/auth';
import { LocalStorageService } from '@tl/tl-common';
import { Observable, Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {

    private destroy$ = new Subject<void>();
    
  constructor(
    private authService: NbAuthService,
    private localStorageService: LocalStorageService,
    private router: Router) {
  }
  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    return this.canActivate();
  }

  canActivate() {
    const navigation = this.router.getCurrentNavigation();
    let url = '/';
    if (navigation) {
      url = navigation.extractedUrl.toString();
    }
    return this.authService.isAuthenticatedOrRefresh()
      .pipe(
        tap(authenticated => {
          if (!authenticated) {
            this.localStorageService.write('redirect-custom', url);
            this.authService.authenticate('eauth')
            .pipe(takeUntil(this.destroy$))
            .subscribe((authResult: NbAuthResult) => {
            });
          }
        }),
      );
  }
}