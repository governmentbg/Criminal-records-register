import { Component } from '@angular/core';
import { NbTokenService } from '@nebular/auth';
import { NbMenuItem } from '@nebular/theme';
import { takeWhile } from 'rxjs';
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
  menu: NbMenuItem[];
  loggedIn: boolean = true;

  constructor(
    private pagesMenu: PagesMenu,
    private tokenService: NbTokenService
  ) {
    this.initMenu();

    this.tokenService
      .tokenChange()
      .pipe(takeWhile(() => this.loggedIn))
      .subscribe(() => {
        this.initMenu();
      });
  }

  initMenu() {
    this.menu = this.pagesMenu.getMenuItems();
  }
}
