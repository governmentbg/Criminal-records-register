import { GraoPersonModel } from "./_models/grao-person.model";
import {
  Component,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from "@angular/core";
import {
  GridSelectionMode,
  IgxDialogComponent,
  IgxGridComponent,
} from "@infragistics/igniteui-angular";
import { DateFormatService } from "../../../../../../@core/services/common/date-format.service";
import { SearchPersonForm } from "./_models/search-person.form";
import { ResultFromSearchOverviewComponent } from "../result-from-search-overview/result-from-search-overview.component";
import { FormUtils } from "../../../../../../@core/utils/form.utils";

@Component({
  selector: "cais-grao-person-overview",
  templateUrl: "./grao-person-overview.component.html",
  styleUrls: ["./grao-person-overview.component.scss"],
})
export class GraoPersonOverviewComponent {
  constructor(public dateFormatService: DateFormatService) {}

  @Input() people: any;
  @Input() genderTypes: any;
  @Input() isForPreview: boolean;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  @ViewChild("peopleGrid", {
    read: IgxGridComponent,
  })
  public peopleGrid: IgxGridComponent;

  @ViewChild("resultGrid")
  resultFromSearchPersonGridForm: ResultFromSearchOverviewComponent;

  public model: GraoPersonModel[];
  public selectionMode: GridSelectionMode = GridSelectionMode.single;
  public currentPage = 0;
  public itemsPerPage = 5;

  public selectedItem: any;
  public selectedRows = [];

  @Output() selectRow = new EventEmitter<string>();

  public searchPersonForm = new SearchPersonForm();

  ngOnInit(): void {
    this.selectionMode = this.isForPreview
      ? GridSelectionMode.none
      : GridSelectionMode.single;
  }

  handleRowSelection(event) {
    this.selectRow.emit(event.newSelection[0]);
  }

  onCloseDialog() {
    this.searchPersonForm = new SearchPersonForm();
    this.dialog.close();
  }

  handleSelectedRow(event) {
    if (event) {
      var items = this.resultFromSearchPersonGridForm.searchPersonGridResult;
      var result = items.filter((item) => {
        return item["identifier"] == event;
      });

      if (result.length) {
        this.selectedItem = result[0];
      } else {
        this.selectedItem = null;
      }

      if(this.selectedItem){
        this.peopleGrid.addRow(this.selectedItem);
        //this.peopleGrid.selectRows(this.selectedItem.id);
        this.onCloseDialog();
      }
    } else {
      this.selectedItem = null;
    }
  }

  onSearch = () => {
    this.resultFromSearchPersonGridForm.onSearch();
  };
}
