import { NbIconLibraries, NbMenuItem } from "@nebular/theme";
import { Injectable } from "@angular/core";
import { RoleNameEnum } from "../@core/constants/role-name.enum";

@Injectable()
export class PagesMenu {
  constructor(iconsLibrary: NbIconLibraries) {

    iconsLibrary.registerFontPack("material-icons", {
      packClass: "material-icons",
      ligature: true
    });
    iconsLibrary.setDefaultPack("material-icons");

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
    iconsLibrary.registerFontPack("fas", {
      packClass: "fas",
      iconClassPrefix: "fa",
    });


    iconsLibrary.registerFontPack('material-icons', {
      packClass: "material-icons",
      ligature: true,
    });
  }

  hasNoRole(roles: string[], role: string): boolean {
    return roles.indexOf(role) === -1;
  }

  getMenuItems(roles: string[]): NbMenuItem[] {
    const dashboardMenu: NbMenuItem[] = [
      {
        title: "Табло",
        link: "/pages/home",
        home: true,
        icon: { icon: "home", pack: "fa" }
      },
      {
        title: "Лица",
        icon: "people-outline",
        link: "/pages/people",
        hidden:
          this.hasNoRole(roles, RoleNameEnum.Normal) &&
          this.hasNoRole(roles, RoleNameEnum.Judge),
      },
      {
        title: "Свидетелства",
        icon: { icon: "file-text-outline", pack: "eva" },
        hidden: this.hasNoRole(roles, RoleNameEnum.Normal),
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
          {
            title: "Търсене на свидетелства",
            link: "/pages/applications/search",
          },
        ],
      },
      {
        title: "E-Свидетелства",
        icon: { icon: "file-add-outline", pack: "eva" },
        hidden: this.hasNoRole(roles, RoleNameEnum.CentralAuth),
        children: [
          {
            title: "Потвърждение за плащане",
            link: "/pages/e-applicaiton/check-payment",
          },
          {
            title: "Oсвободени от плащане",
            link: "/pages/e-applicaiton/check-tax-free",
          },
        ],
      },
      {
        title: "Заявки",
        icon: { icon: "mail-bulk", pack: "fa" },
        link: "/pages/internal-requests",
        hidden:
          this.hasNoRole(roles, RoleNameEnum.Normal) &&
          this.hasNoRole(roles, RoleNameEnum.Judge),
      },
      {
        title: "Справка за съдимост",
        icon: { icon: "id-card", pack: "fas" },
        hidden: this.hasNoRole(roles, RoleNameEnum.Normal),
        children: [
          {
            title: "Нови искания",
            link: "/pages/report-applications",
          },
          {
            title: "В процес на обработка",
            link: "/pages/report-applications/approved",
          },
          {
            title: "Обработени искания",
            link: "/pages/report-applications/delivered",
          },
          {
            title: "Обработени справки",
            link: "/pages/report-applications/all-generated-reports",
          },
          {
            title: "Търсене на справки",
            link: "/pages/report-applications/search",
          },
        ],
      },
      {
        title: "Бюлетини",
        icon: "pie_chart",
        hidden:
          this.hasNoRole(roles, RoleNameEnum.Normal) &&
          this.hasNoRole(roles, RoleNameEnum.Judge),
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
        title: "За решение от съдия/юрист",
        icon: { icon: "balance-scale", pack: "fa" },
        hidden: this.hasNoRole(roles, RoleNameEnum.Judge),
        children: [
          {
            title: "Заявки за реабилитация",
            link: "/pages/internal-requests/for-judge",
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
        icon: { icon: "globe-2-outline", pack: "eva" },
        hidden: this.hasNoRole(roles, RoleNameEnum.CentralAuth),
        children: [
          {
            title: "Актуални сведения",
            link: "/pages/fbbcs",
          },
          {
            title: "Подлежащи на заличаване",
            link: "/pages/fbbcs/for-destruction",
          },
          // {
          //   title: "Заличени",
          //   link: "/pages/fbbcs/destructed",
          // },
        ],
      },
      {
        title: "ECRIS",
        icon: "bookmark",
        hidden: this.hasNoRole(roles, RoleNameEnum.CentralAuth),
        children: [
          {
            title: "За идентификация",
            link: "/pages/ecris/identification",
          },
          // {
          //   title: "Запитвания",
          //   link: "/pages/ecris/req-waiting",
          // },
          {
            title: "Входящи съобщения",
            link: "/pages/ecris/inbox",
          },
          {
            title: "Изходящи съобщения",
            link: "/pages/ecris/outbox",
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
        title: "Справки",
        icon: { icon: "tasks", pack: "fas" },
        children: [
          {
            title: "Xарактеристики на бюлетини",
            link: "/pages/inquiry/search-bulletins",
          },
          {
            title: "Xарактеристики на лице",
            link: "/pages/inquiry/search-people",
          },
        ],
      },
      {
        title: "Статистика",
        icon: { icon: "chart-bar", pack: "fas" },
        children: [
          {
            title: "Бюлетини",
            link: "/pages/statistics/bulletins",
          },
          {
            title: "Свидетелства",
            link: "/pages/statistics/applications",
          },
        ],
      },
      {
        title: "Журнал на Е-справки",
        icon: { icon: "file-alt", pack: "fa" },
        hidden: this.hasNoRole(roles, RoleNameEnum.CentralAuth),
        children: [
          {
            title: "Справка за съдимост",
            link: "/pages/e-applicaiton-report/reports/overview",
          },
          {
            title: "Справка за идентификатори",
            link: "/pages/e-applicaiton-report/search-pers/overview",
          },
        ],
      },
      {
        title: "Администрация",
        icon: { icon: "cog", pack: "fa" },
        hidden:
          this.hasNoRole(roles, RoleNameEnum.Admin) &&
          this.hasNoRole(roles, RoleNameEnum.GlobalAdmin),
        children: [
          {
            title: "Потребители",
            link: "/pages/users",
          },
          {
            title: "Външни потребители",
            link: "/pages/users-external",
            hidden: this.hasNoRole(roles, RoleNameEnum.GlobalAdmin),
          },
          {
            title: "Външни администрации",
            link: "/pages/administrations-ext",
            hidden: this.hasNoRole(roles, RoleNameEnum.GlobalAdmin),
          },
          {
            title: "Публични потребители",
            link: "/pages/users-public",
            hidden: this.hasNoRole(roles, RoleNameEnum.GlobalAdmin),
          },
          {
            title: "Управление на бюлетини",
            link: "/pages/bulletin-administrations",
          },
        ],
      },
    ];

    return dashboardMenu;
  }
}
