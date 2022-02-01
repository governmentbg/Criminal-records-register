import { Component, OnInit } from "@angular/core";
import {
  ColumnPinningPosition,
  IPinningConfig,
  NoopFilteringStrategy,
  NoopSortingStrategy,
} from "@infragistics/igniteui-angular";

@Component({
  selector: "ngx-bulletin-overview",
  templateUrl: "./bulletin-overview.component.html",
  styleUrls: ["./bulletin-overview.component.scss"],
})
export class BulletinOverviewComponent implements OnInit {
  public noopFilterStrategy = NoopFilteringStrategy.instance();
  public noopSortStrategy = NoopSortingStrategy.instance();

  public gridId: string;
  public stateKey: string;
  public isForPreview: boolean;
  public title: string;

  protected successMessage = "Успешно запазени данни!";
  protected dangerMessage = "Грешка при запазване на данните: ";
  protected validationMessage = "Грешка при валидациите!";

  public items = [
    { id: "test-1111", name: "Иван", identifier: "9701010102" },
    { id: "test-2222", name: "Петър", identifier: "9701010103" },
    { id: "test-3333", name: "Асен", identifier: "9701010104" },
  ];

  public pinningConfig: IPinningConfig = { columns: ColumnPinningPosition.End };

  constructor() {}

  ngOnInit(): void {}
}
