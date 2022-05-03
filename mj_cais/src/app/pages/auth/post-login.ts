import { Component, OnDestroy } from "@angular/core";
import { Router } from "@angular/router";
import { NbAuthResult, NbAuthService } from "@nebular/auth";
import { LocalStorageService } from "@tl/tl-common";
import { Subject, takeUntil } from "rxjs";

@Component({
    selector: 'nb-playground-oauth2-callback',
    template: `Authenticating...`,
  })
  export class PostLoginComponent implements OnDestroy {
  
    private destroy$ = new Subject<void>();
  
    constructor(
      private authService: NbAuthService,
      private router: Router,
      private localStorageService: LocalStorageService,) {
      this.authService.authenticate('eauth')
        .pipe(takeUntil(this.destroy$))
        .subscribe((authResult: NbAuthResult) => {
          if (authResult.isSuccess()) {
            let path = this.localStorageService.read('redirect-custom');
            this.localStorageService.remove('redirect-custom');
            this.router.navigateByUrl(path ?? '/');
          }
        });
    }
  
    ngOnDestroy(): void {
      this.destroy$.next();
      this.destroy$.complete();
    }
  }