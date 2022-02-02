import { Directive, ViewChild, Injector } from "@angular/core";
import {
  IgxGridStateDirective,
  IFilteringExpressionsTree,
  IGridState,
  IPinningConfig,
  ColumnPinningPosition,
  IgxTreeGridComponent,
  NoopFilteringStrategy,
  NoopSortingStrategy,
  IgxExcelExporterOptions,
} from "@infragistics/igniteui-angular";
import {
  CrudService,
  GridRemoteFilteringService,
  RemoteComponentWithForm,
} from "@tl/tl-common";
import { map } from "rxjs/operators";

@Directive()
export class RemoteGridWithStatePersistance<
  T extends { id: number },
  CS extends CrudService<T, number>
> extends RemoteComponentWithForm<T, CS> {
  @ViewChild(IgxGridStateDirective, { static: true })
  public state: IgxGridStateDirective;

  public noopFilterStrategy = NoopFilteringStrategy.instance();
  public noopSortStrategy = NoopSortingStrategy.instance();

  public gridId: string;
  public stateKey: string;
  public isForPreview: boolean;
  public title: string;

  protected successMessage = "Успешно запазени данни!";
  protected dangerMessage = "Грешка при запазване на данните: ";
  protected validationMessage = "Грешка при валидациите!";

  public pinningConfig: IPinningConfig = { columns: ColumnPinningPosition.End };

  constructor(gridId: string, service: CS, injector: Injector) {
    super(service, injector);
    this.gridId = gridId;
    this.isForPreview = this.activatedRoute.snapshot.data["preview"];
    this.stateKey = this.gridId + "-state";
  }

  public ngAfterViewInit() {
    super.ngAfterViewInit();
    //this.restoreGridState();
  }

  public filteringDone(event: IFilteringExpressionsTree): void {
    super.filteringDone(event);
    // let gridState: IGridState = this.getIgxGridState();
    // gridState.paging.metadata.countPages = 0; // Reset page when filtering
    // this.saveGridState();
  }

  public pagerChange(event: any) {
    super.pagerChange(event);
    // let gridState: IGridState = this.getIgxGridState();
    // gridState.paging.recordsPerPage = event.perPage;
    // gridState.paging.metadata.countPages = event.page;
    // this.saveGridState();
  }

  public sortingDone(event: any): void {
    super.sortingDone(event);

    // let gridState: IGridState = this.getIgxGridState();
    // const state = this.getSavedGridState();
    // if (state) {
    //   let stateObject: IGridState = JSON.parse(state);
    //   // Sorting event resets the current page, so change it from saved
    //   gridState.paging.metadata.countPages =
    //     stateObject.paging.metadata.countPages;
    // }

    // this.saveGridState();
  }

  public getSavedGridState(): string {
    let result = window.localStorage.getItem(this.stateKey);
    return result;
  }

  public restoreGridState() {
    const state = this.getSavedGridState();
    if (state) {
      let stateObject: IGridState = JSON.parse(state);

      // Update paging dropdown
      this.remoteService.pagerParams.perPage =
        stateObject.paging.recordsPerPage;
      this.remoteService.pagerParams.page =
        stateObject.paging.metadata.countPages;

      // Update grid state with the one saved in local storage
      this.state.setState(state);

      this.remoteService.processData(null);
    } else if (this.grid instanceof IgxTreeGridComponent) {
      let params = this.remoteService.httpParams;
      this.readData(params);
    }
  }

  public saveGridState() {
    const state = this.getIgxGridState();
    if (typeof state === "string") {
      window.localStorage.setItem(this.stateKey, state);
    } else {
      window.localStorage.setItem(this.stateKey, JSON.stringify(state));
    }
  }

  protected getIgxGridState(shouldSerialize?: boolean): IGridState {
    let serialize = shouldSerialize ?? false;
    return this.state.getState(serialize, [
      "sorting",
      "filtering",
      "paging",
    ]) as IGridState;
  }

  protected createRemoteService() {
    this.remoteService = new GridRemoteFilteringService(
      {},
      this.service,
      this.grid,
      this.injector
    );

    //Dirty fix for incorrect child property seperator in OData. What can you do.
    //Replaces all property accessor dots (.) with "/" as it's the official seperator.
    (this.remoteService as any).getGridOperandFieldName = (
      fieldName: string
    ) => {
      const finalFieldName = this.service.gridFilteringFields()[fieldName]
        ? this.service.gridFilteringFields()[fieldName]
        : fieldName;

      return finalFieldName.replace(".", "/");
    };
  }

  //   protected errorHandler = (error, toastr: CustomToastrService) => {
  //     // TODO: injector.get<CustomToastrService>
  //     // TODO: change message to extract info from exception: customMessage ??
  //     var errorObj = error.error;
  //     var errorText = error.status + ": " + errorObj.error;
  //     toastr.showBodyToast("danger", this.dangerMessage, errorText);
  //   };

  public getIDField(): string {
    // DO NOT REMOVE, if it has value it applies filter in ODATA that can break queries
    return "";
  }

  // Overriding default behaviour
  protected configureExcelExportService() {
    let httpParams = this.excelExportGetHttpParams();
    this.excelExportGetAllNoWrap(httpParams)
      .pipe(
        map((items: T[]) => {
          return items.map((item) => {
            return this.excelExportMapItem(item);
          });
        })
      )
      .subscribe((data) => {
        // Here are the changes
        let title = this.title ?? "export";
        let options = new IgxExcelExporterOptions(title);
        options.columnWidth = 30;
        this.exportService.exportData(data, options);
      });
  }
}
