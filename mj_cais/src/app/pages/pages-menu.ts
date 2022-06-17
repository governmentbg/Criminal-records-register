import { NbIconLibraries, NbMenuItem } from "@nebular/theme";
import { Injectable } from "@angular/core";
import { RoleNameEnum } from "../@core/constants/role-name.enum";

@Injectable()
export class PagesMenu {

  constructor(iconsLibrary: NbIconLibraries) {
    iconsLibrary.registerFontPack("fa", {
      packClass: "fa",
      iconClassPrefix: "fa",
    });
    iconsLibrary.registerFontPack("far", {
      packClass: "far",
      iconClassPrefix: "fa",
    });
    iconsLibrary.registerFontPack("fas", {
      packClass: "fas",
      iconClassPrefix: "fa",
    });
  }

  hasNoRole(roles: string[], role: string): boolean {
    return roles.indexOf(role) === -1;
  }

  getMenuItems(roles: string[]): NbMenuItem[] {
    const dashboardMenu: NbMenuItem[] = [
      {
        title: "",
        link: "/pages/home",
        home: true,
        hidden: true,
      },
      {
        title: "Лица",
        icon: "people-outline",
        link: "/pages/people",
      },
      {
        title: "Бюлетини",
        icon: { icon: "file-alt", pack: "fa" },
        hidden: this.hasNoRole(roles, RoleNameEnum.Normal),
        children: [
          {
            title: "Актуални бюлетини",
            link: "/pages/bulletins/active",
          },
          {
            title: "Нови бюлетини",
            link: "/pages/bulletins/new-office",
          },
          {
            title: "Нови бюлетини от ЕИСС",
            link: "/pages/bulletins/new-eiss",
          },
          {
            title: "Данни за изтърпени наказания",
            link: "/pages/isin/identified",
          },
          {
            title: "За унищожаване",
            link: "/pages/bulletins/for-destruction",
          },
          {
            title: "Подлежащи на реабилитация",
            link: "/pages/bulletins/for-rehabilitation",
          },
          {
            title: "Настъпили обстоятелства",
            link: "/pages/bulletins/events",
          },
        ],
      },
      {
        title: "Свидетелства",
        icon: { icon: "file-alt", pack: "fa" },
        hidden: this.hasNoRole(roles, RoleNameEnum.Normal) ,
        children: [
          {
            title: "Нови заявления",
            link: "/pages/applications",
          },
          {
            title: "Потвърждение за плащане",
            link: "/pages/applications/waiting-payment",
          },       
          {
            title: "За обработка",
            link: "/pages/applications/for-check",
          },
          {
            title: "За подпис",
            link: "/pages/applications/for-signing",
          },
        ],
      },
      {
        title: "За решение от съдия/юрист",
        icon: "message-circle-outline",
        hidden: this.hasNoRole(roles, RoleNameEnum.Normal) && this.hasNoRole(roles, RoleNameEnum.Judge),
        children: [
          {
            title: "Заявки за реабилитация",
            link: "/pages/internal-requests",
          },
          {
            title: "Oсвободени от плащане",
            link: "/pages/applications/tax-free",
          },
          {
            title: "За подпис от съдия/юрист",
            link: "/pages/applications/for-signing-by-judge",
          },
          {
            title: "За избор на бюлетини",
            link: "/pages/applications/bulletin-selection",
          },
        ],
      },
      {
        title: "Осъдени в чужбина",
        icon: { icon: "file-alt", pack: "fa" },
        hidden: this.hasNoRole(roles, RoleNameEnum.Judge) && this.hasNoRole(roles, RoleNameEnum.Admin),
        children: [
          {
            title: "Актуални сведения",
            link: "/pages/fbbcs",
          },
          {
            title: "Подлежащи на заличаване",
            link: "/pages/fbbcs/for-destruction",
          },
          {
            title: "Заличени",
            link: "/pages/fbbcs/destructed",
          },
        ],
      },
      {
        title: "ECRIS",
        icon: "layout-outline",
        hidden: this.hasNoRole(roles, RoleNameEnum.CentralAuth),
        children: [
          {
            title: "За идентификация",
            link: "/pages/ecris/identification",
          },
          {
            title: "Запитвания",
            link: "/pages/ecris/req-waiting",
          },
          {
            title: "ECRIS-TCN",
            link: "/pages/ecris-tcn",
          },
        ],
      },
      {
        title: "Изтърпени наказания",
        icon: "shuffle-2-outline",
        link: "/pages/isin/new",
        hidden: this.hasNoRole(roles, RoleNameEnum.CentralAuth),
      },
      {
        title: "Администрация",
        icon: { icon: "cog", pack: "fa" },
        expanded: true,
        hidden: this.hasNoRole(roles, 'Admin') && this.hasNoRole(roles, 'GlobalAdmin'),
        children: [
          {
            title: "Потребители",
            link: "/pages/users",
          },
          {
            title: "Външни потребители",
            link: "/pages/users-external",
            hidden: this.hasNoRole(roles, 'GlobalAdmin')
          },
          {
            title: "Външни администрации",
            link: "/pages/administrations-ext",
            hidden: this.hasNoRole(roles, 'GlobalAdmin')
          },
          {
            title: "Публични потребители",
            link: "/pages/users-public",
            hidden: this.hasNoRole(roles, 'GlobalAdmin')
          },
          {
            title: "Управление на бюлетини",
            link: "/pages/bulletin-administrations",    
            hidden: this.hasNoRole(roles, 'GlobalAdmin')
          }
        ],
      },   
    ];

    return dashboardMenu;
  }
}
