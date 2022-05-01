import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import {
  GridSelectionMode,
  IgxGridComponent,
} from "@infragistics/igniteui-angular";
import { NbDialogRef } from "@nebular/theme";
import { NgxSpinnerService } from "ngx-spinner";
import { RemoteGridWithStatePersistance } from "../../../../../directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../services/common/date-format.service";
import { FormUtils } from "../../../../../utils/form.utils";
import { CountryGridService } from "../_data/country-grid.service";
import { CountryGridModel } from "../_models/country-grid.model";

@Component({
  selector: "cais-country-dialog",
  templateUrl: "./country-dialog.component.html",
  styleUrls: ["./country-dialog.component.scss"],
})
export class CountryDialogComponent extends RemoteGridWithStatePersistance<
  CountryGridModel,
  CountryGridService
> {
  @Input() title: string;

  public selectedItem: any;
  public selectionMode: GridSelectionMode = "single";
  public selectedRows = [];
  private intervalFuc;

  constructor(
    protected ref: NbDialogRef<CountryDialogComponent>,
    service: CountryGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    private spinner: NgxSpinnerService
  ) {
    super("country-dialog", service, injector);
    // todo: да е на ниво lookup
    this.spinner.show();
    this.intervalFuc = setInterval(() => {
      let isHiden = this.hideSpinner();
      if(isHiden){
        clearInterval(this.intervalFuc);
      }
    }, 500);
  }

  ngOnDestroy() {
    if (this.intervalFuc) {
      clearInterval(this.intervalFuc);
    }
  }

  hideSpinner():boolean {
    if (!this.remoteService.isLoading) {
       this.spinner.hide();
       return true;
    }
    return false;
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
