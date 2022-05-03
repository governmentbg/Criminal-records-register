import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { NbAuthResult, NbAuthService } from '@nebular/auth';
import { LocalStorageService } from '@tl/tl-common';
import { Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';

@Injectable()
export class AuthGuard implements CanActivate {

    private destroy$ = new Subject<void>();
    
  constructor(
    private authService: NbAuthService,
    private localStorageService: LocalStorageService,
    private router: Router) {
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