import { NbIconLibraries, NbMenuItem } from "@nebular/theme";
import { Injectable } from "@angular/core";

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

  getMenuItems(): NbMenuItem[] {
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
        children: [
          {
            title: "Актуални бюлетини",
            link: "/pages/bulletins",
          },
          {
            title: "Нови бюлетини",
            link: "/pages/bulletins-new-office",
          },
          {
            title: "Нови бюлетини от ЕИСС",
            link: "/pages/bulletins-new-eiss",
          },
          {
            title: "За отразяване от ИСИН",
            link: "/pages/isin-identified",
          },
          {
            title: "За унищожаване",
            link: "/pages/bulletins-for-destruction",
          },
          {
            title: "Подлежащи на реабилитация",
            link: "/pages/bulletins-for-rehabilitation",
          },
        ],
      },
      {
        title: "За решение от съдия",
        icon: "message-circle-outline",
        children: [
          {
            title: "Заявки за реабилитация",
            link: "/pages/internal-requests",
          },
        ],
      },
      {
        title: "Осъдени в чужбина",
        icon: { icon: "file-alt", pack: "fa" },
        children: [
          {
            title: "Актуални сведения",
            link: "/pages/fbbcs",
          },
          {
            title: "Подлежащи на заличаване",
            link: "/pages/fbbcs-for-destruction",
          },
          {
            title: "Заличени",
            link: "/pages/fbbcs-destructed",
          },
        ],
      },
      {
        title: "ECRIS",
        icon: "layout-outline",
        expanded: true,
        children: [
          {
            title: "За идентификация",
            link: "/pages/ecris-identification",
          },
          {
            title: "Запитвания",
            link: "/pages/ecris-req-waiting",
          },
        ],
      },
      {
        title: "Изтърпени наказания",
        icon: "shuffle-2-outline",
        link: "/pages/isin-new",
      },
    ];

    return dashboardMenu;
  }
}
