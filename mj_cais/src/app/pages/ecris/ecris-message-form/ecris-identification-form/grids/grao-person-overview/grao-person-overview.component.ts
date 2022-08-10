import { GraoPersonModel } from "./_models/grao-person.model";
import {
  Component,
  EventEmitter,
  Injector,
  Input,
  OnInit,
  Output,
  ViewChild,
} from "@angular/core";
import {
  GridSelectionMode,
  IgxDialogComponent,
  IgxGridComponent,
} from "@infragistics/igniteui-angular";
import { DateFormatService } from "../../../../../../@core/services/common/date-format.service";
import { RemoteGridWithStatePersistance } from "../../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { GraoPersonGridService } from "./_data/grao-person-grid.service";
import { SearchPersonForm } from "./_models/search-person.form";
import { BaseNomenclatureModel } from "../../../../../../@core/models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../../../../@core/services/rest/nomenclature.service";
import { ResultFromSearchOverviewComponent } from "../result-from-search-overview/result-from-search-overview.component";

@Component({
  selector: "cais-grao-person-overview",
  templateUrl: "./grao-person-overview.component.html",
  styleUrls: ["./grao-person-overview.component.scss"],
})
export class GraoPersonOverviewComponent extends RemoteGridWithStatePersistance<
  GraoPersonModel,
  GraoPersonGridService
> {
  constructor(
    public service: GraoPersonGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("grao-people-search", service, injector);
  }

  @Input() people: any;
  @Input() dbData: any;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  @ViewChild("peopleGrid", {
    read: IgxGridComponent,
  })
  public peopleGrid: IgxGridComponent;

  @ViewChild("resultGrid")
  resultFromSearchPersonGridForm: ResultFromSearchOverviewComponent;

  public model: GraoPersonModel[];
  public selectionMode: GridSelectionMode = "single";
  public currentPage = 0;
  public itemsPerPage = 5;

  public selectedItem: any;
  public selectedRows = [];
  private toAdd: any;

  @Output() selectRow = new EventEmitter<string>();

  public searchPersonForm = new SearchPersonForm();

  ngOnInit(): void {
    super.ngOnInit();
  }

  handleRowSelection(event) {
    this.selectRow.emit(event.newSelection[0]);
  }

  onCloseDialog() {
    this.searchPersonForm = new SearchPersonForm();
    this.dialog.close();
  }

  handleSelectedRow(event) {
    this.toAdd = event;
  }

  addInGrid() {
    this.peopleGrid.addRow(this.toAdd);
    this.onCloseDialog();
  }

  onSearch = () => {
    this.resultFromSearchPersonGridForm.onSearch();
  };
}
