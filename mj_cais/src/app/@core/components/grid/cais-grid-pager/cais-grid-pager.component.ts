import { Component, Input } from "@angular/core";
import { GridPagerComponent, ITLRouteReference } from "@tl/tl-common";

@Component({
  selector: "cais-grid-pager",
  templateUrl: "./cais-grid-pager.component.html",
  styleUrls: ["./cais-grid-pager.component.scss"],
})
export class CaisGridPagerComponent extends GridPagerComponent {
  @Input()
  public excelReference: ITLRouteReference;

  constructor() {
    super();
  }

  public selectChange(event) {
    super.selectChange(event);
  }
}
