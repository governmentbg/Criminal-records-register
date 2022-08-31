import { Component } from "@angular/core";

@Component({
  selector: "ngx-footer",
  styleUrls: ["./footer.component.scss"],
  template: `<div class="d-flex mt-1 flex-fill justify-content-center gap-5">
  <p class="text-center mx-5">
  Създаден в рамките на договор № BG05SFOP001-3.001-0010-C01/23.06.2017
  г. по проект: „Реализиране на Централизирана автоматизирана информационна система „Съдебен статус“, <br>
  финансиран по Оперативна програма „Добро управление“, чрез ЕСФ.
</p>
<img src="assets/images/eu.png" alt="eu-logo" class="eu-logo img-fluid" /></div>
  `,
})
export class FooterComponent { }
