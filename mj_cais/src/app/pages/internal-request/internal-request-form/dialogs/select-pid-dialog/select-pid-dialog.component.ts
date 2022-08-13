import { Component, Injector, Input } from "@angular/core";
import {
  GridSelectionMode,
  IgxGridComponent,
} from "@infragistics/igniteui-angular";
import { NbDialogRef } from "@nebular/theme";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { FormUtils } from "../../../../../@core/utils/form.utils";
import { SelectPidGridService } from "./_data/select-pid-grid.service";
import { SelecrPidGridModel } from "./_models/select-pid-grid.model";

@Component({
  selector: "cais-select-pid-dialog",
  templateUrl: "./select-pid-dialog.component.html",
  styleUrls: ["./select-pid-dialog.component.scss"],
})
export class SelectPidDialogComponent extends RemoteGridWithStatePersistance<
  SelecrPidGridModel,
  SelectPidGridService
> {
  @Input() title: string;

  public selectedItem: any;
  public selectionMode: GridSelectionMode = "single";
  public selectedRows = [];

  constructor(
    protected ref: NbDialogRef<SelectPidDialogComponent>,
    service: SelectPidGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    private loaderService: LoaderService
  ) {
    super("pids-dialog", service, injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
    this.loaderService.showSpinner(this.remoteService);
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
