import { Component, Injector, Input } from "@angular/core";
import {
  GridSelectionMode,
  IgxGridComponent,
} from "@infragistics/igniteui-angular";
import { NbDialogRef } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { FormUtils } from "../../../../../../../@core/utils/form.utils";
import { OffenceCategoryGridService } from "../_data/offence-category-grid.service";
import { OffenceCategoryGridModel } from "../_models/offence-category-grid.model";

@Component({
  selector: "cais-offence-category-dialog",
  templateUrl: "./offence-category-dialog.component.html",
  styleUrls: ["./offence-category-dialog.component.scss"],
})
export class OffenceCategoryDialogComponent extends RemoteGridWithStatePersistance<
  OffenceCategoryGridModel,
  OffenceCategoryGridService
> {
  @Input() title: string;

  public selectedItem: any;
  public selectionMode: GridSelectionMode = "single";
  public selectedRows = [];

  constructor(
    protected ref: NbDialogRef<OffenceCategoryDialogComponent>,
    service: OffenceCategoryGridService,
    injector: Injector
  ) {
    super("offence-categories-dialog", service, injector);
  }

  handleRowSelection(event) {
    let selectedId = event.newSelection[0];
    if (selectedId) {
      let grid = this.grid as IgxGridComponent;
      this.selectedItem = FormUtils.getGridItemById(grid, selectedId);
    } else {
      this.selectedItem = undefined;
    }
  }

  success() {
    this.ref.close(this.selectedItem);
  }

  dismiss() {
    this.ref.close();
  }
}