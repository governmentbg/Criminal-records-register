import {
  Component,
  EventEmitter,
  Injector,
  Input,
  OnInit,
  Output,
} from "@angular/core";
import { GridSelectionMode } from "@infragistics/igniteui-angular";
import { RemoteGridWithStatePersistance } from "../../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { CustomToastrService } from "../../../../../../@core/services/common/custom-toastr.service";
import { DateFormatService } from "../../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../../@core/services/common/loader.service";
import { SearchPersonForm } from "../grao-person-overview/_models/search-person.form";
import { ResultGridService } from "./_data/result-grid.service";
import { ResultGridModel } from "./_models/result-grid.model";

@Component({
  selector: "cais-result-from-search-overview",
  templateUrl: "./result-from-search-overview.component.html",
  styleUrls: ["./result-from-search-overview.component.scss"],
})
export class ResultFromSearchOverviewComponent {
  constructor(
    public service: ResultGridService,
    public dateFormatService: DateFormatService,
    private loader: LoaderService,
    public toastr: CustomToastrService
  ) {}

  @Input() searchPersonForm: SearchPersonForm;
  @Output() selectRow = new EventEmitter<string>();
  public searchPersonGridResult: ResultGridModel[] = [];

  public selectionMode: GridSelectionMode = GridSelectionMode.single;
  public currentPage = 0;
  public itemsPerPage = 5;

  public selectedItem: any;
  public selectedRows = [];

  ngOnInit(): void {}

  public onSearch = () => {
    if (!this.searchPersonForm.group.valid) {
      this.searchPersonForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.loader.show();
    let formObj = this.searchPersonForm.group.getRawValue();
    let filterQuery = this.service.constructQueryParamsByFiltersForLocalGrid(
      formObj,
      ""
    );

    this.service.personSearch(filterQuery).subscribe(
      (response: any) => {
        this.searchPersonGridResult = response;
        this.loader.hide();
      },
      (error) => {
        this.loader.hide();
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast("danger", "Грешка при търсене:", errorText);
      }
    );
  };

  handleRowSelection(event) {
    this.selectRow.emit(event.newSelection[0]);
  }
}
