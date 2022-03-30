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
        title: "IoT Dashboard",
        icon: "home-outline",
        link: "/pages/iot-dashboard",
        home: true,
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
            link: "/pages/bulletins-new-eiss",
          },
          {
            title: "За унищожаване",
            link: "/pages/bulletins-for-destruction",
          },
          {
            title: "Подлежащи на реабилитация",
            link: "/pages/bulletins-for-rehabilitation",
          },
          {
            title: "Бюлетини за ECRIS",
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
      {
        title: "FEATURES",
        group: true,
      },
      {
        title: "Layout",
        icon: "layout-outline",
        children: [
          {
            title: "Stepper",
            link: "/pages/layout/stepper",
          },
          {
            title: "List",
            link: "/pages/layout/list",
          },
          {
            title: "Infinite List",
            link: "/pages/layout/infinite-list",
          },
          {
            title: "Accordion",
            link: "/pages/layout/accordion",
          },
          {
            title: "Tabs",
            pathMatch: "prefix",
            link: "/pages/layout/tabs",
          },
        ],
      },
      {
        title: "Forms",
        icon: "edit-2-outline",
        children: [
          {
            title: "Form Inputs",
            link: "/pages/forms/inputs",
          },
          {
            title: "Form Layouts",
            link: "/pages/forms/layouts",
          },
          {
            title: "Buttons",
            link: "/pages/forms/buttons",
          },
          {
            title: "Datepicker",
            link: "/pages/forms/datepicker",
          },
        ],
      },
      {
        title: "UI Features",
        icon: "keypad-outline",
        link: "/pages/ui-features",
        children: [
          {
            title: "Grid",
            link: "/pages/ui-features/grid",
          },
          {
            title: "Icons",
            link: "/pages/ui-features/icons",
          },
          {
            title: "Typography",
            link: "/pages/ui-features/typography",
          },
          {
            title: "Animated Searches",
            link: "/pages/ui-features/search-fields",
          },
        ],
      },
      {
        title: "Modal & Overlays",
        icon: "browser-outline",
        children: [
          {
            title: "Dialog",
            link: "/pages/modal-overlays/dialog",
          },
          {
            title: "Window",
            link: "/pages/modal-overlays/window",
          },
          {
            title: "Popover",
            link: "/pages/modal-overlays/popover",
          },
          {
            title: "Toastr",
            link: "/pages/modal-overlays/toastr",
          },
          {
            title: "Tooltip",
            link: "/pages/modal-overlays/tooltip",
          },
        ],
      },
      {
        title: "Extra Components",
        icon: "message-circle-outline",
        children: [
          {
            title: "Calendar",
            link: "/pages/extra-components/calendar",
          },
          {
            title: "Progress Bar",
            link: "/pages/extra-components/progress-bar",
          },
          {
            title: "Spinner",
            link: "/pages/extra-components/spinner",
          },
          {
            title: "Alert",
            link: "/pages/extra-components/alert",
          },
          {
            title: "Calendar Kit",
            link: "/pages/extra-components/calendar-kit",
          },
          {
            title: "Chat",
            link: "/pages/extra-components/chat",
          },
        ],
      },
      {
        title: "Editors",
        icon: "text-outline",
        children: [
          {
            title: "TinyMCE",
            link: "/pages/editors/tinymce",
          },
          {
            title: "CKEditor",
            link: "/pages/editors/ckeditor",
          },
        ],
      },
      {
        title: "Tables & Data",
        icon: "grid-outline",
        children: [
          {
            title: "Smart Table",
            link: "/pages/tables/smart-table",
          },
          {
            title: "Tree Grid",
            link: "/pages/tables/tree-grid",
          },
        ],
      },
      {
        title: "Miscellaneous",
        icon: "shuffle-2-outline",
        children: [
          {
            title: "404",
            link: "/pages/miscellaneous/404",
          },
        ],
      },
      {
        title: "Auth",
        icon: "lock-outline",
        children: [
          {
            title: "Login",
            link: "/auth/login",
          },
          {
            title: "Register",
            link: "/auth/register",
          },
          {
            title: "Request Password",
            link: "/auth/request-password",
          },
          {
            title: "Reset Password",
            link: "/auth/reset-password",
          },
        ],
      },
    ];

    return dashboardMenu;
  }
}
