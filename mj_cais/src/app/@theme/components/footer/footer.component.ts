import { Component } from "@angular/core";

@Component({
  selector: "ngx-footer",
  styleUrls: ["./footer.component.scss"],
  template: `
    <span class="created-by">
      <div class="row" style="margin-right: 20px;">
        <p class="footer-text">
          Създаден в рамките на договор № BG05SFOP001-3.001-0010-C01/23.06.2017
          г. по проект: <br />
          „Реализиране на Централизирана автоматизирана информационна система
          „Съдебен статус“, <br />
          финансиран по Оперативна програма „Добро управление“, чрез ЕСФ. <br />
        </p>
        <img src="assets/images/eu.png" alt="eu-logo" class="eu-logo" />
      </div>
    </span>
  `,
})
export class FooterComponent {}
