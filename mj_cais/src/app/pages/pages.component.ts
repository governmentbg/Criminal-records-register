import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { NbAuthService, NbTokenService } from '@nebular/auth';
import { NbMenuItem } from '@nebular/theme';
import { NgxPermissionsService } from 'ngx-permissions';
import { of, switchMap, takeWhile, tap } from 'rxjs';
import { PagesMenu } from './pages-menu';

@Component({
  selector: 'ngx-pages',
  styleUrls: ['pages.component.scss'],
  template: `
    <ngx-one-column-layout>
      <nb-menu [items]="menu"></nb-menu>
      <router-outlet></router-outlet>
    </ngx-one-column-layout>
  `,
})
export class PagesComponent {
  menu: NbMenuItem[] = [];
  loggedIn: boolean = true;

  constructor(
    private pagesMenu: PagesMenu,
    private tokenService: NbTokenService,
    private permissionsService: NgxPermissionsService
  ) {

    this.tokenService
      .tokenChange()
      .pipe(takeWhile(() => this.loggedIn))
      .subscribe((tkn) => {
        this.initMenu();
      });
  }

  initMenu() {   
    this.permissionsService.permissions$.subscribe( perm =>       
      this.menu = this.pagesMenu.getMenuItems(Object.keys(perm)));   
  }
}
