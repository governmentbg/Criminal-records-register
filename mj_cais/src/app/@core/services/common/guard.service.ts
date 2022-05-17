import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { NbAuthResult, NbAuthService } from '@nebular/auth';
import { LocalStorageService } from '@tl/tl-common';
import { NgxPermissionsService } from 'ngx-permissions';
import { Observable, of, Subject } from 'rxjs';
import { map, switchMap, takeUntil, tap } from 'rxjs/operators';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {

    private destroy$ = new Subject<void>();
    
  constructor(
    private authService: NbAuthService,
    private localStorageService: LocalStorageService,
    private permissionsService: NgxPermissionsService,
    private http: HttpClient,
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
        switchMap( authenticated =>{
          if (authenticated){
            return this.permissionsService.permissions$.pipe(
              switchMap(
                roles =>{
                  const hasRoles = Object.keys(roles).length !== 0;
                  if (!hasRoles){
                      return this.http.get("/auth/connect/userinfo").pipe(map((data: any) => {
                        if (Array.isArray(data?.role)) {
                          this.permissionsService.loadPermissions(data.role);
                        } else if (data?.role) {
                          this.permissionsService.loadPermissions([data.role]);
                        }
                        return true;
                      })
                    );
                  } else {
                    return of(true);
                  }
                }
              )
            );
          }else{
            return of(false);
          }
        }),
        tap(authenticated => {
          if (!authenticated) {
            this.localStorageService.write('redirect-custom', url);
            this.authService.authenticate('eauth')
            .pipe(takeUntil(this.destroy$))
            .subscribe((authResult: NbAuthResult) => {
            });
          }
        })
      );
  }
}