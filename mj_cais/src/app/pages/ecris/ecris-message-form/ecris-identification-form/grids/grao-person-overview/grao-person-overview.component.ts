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
  IgxGridComponent,
} from "@infragistics/igniteui-angular";
import { DateFormatService } from "../../../../../../@core/services/common/date-format.service";
import { EcrisMessageService } from "../../../_data/ecris-message.service";
import { EcrisMessageForm } from "../../../_models/ecris-message.form";
import { RemoteGridWithStatePersistance } from "../../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { GraoPersonGridService } from "./_data/grao-person-grid.service";
import { BehaviorSubject } from "rxjs";
import { FormUtils } from "../../../../../../@core/utils/form.utils";

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

  @ViewChild("peopleGrid", {
    read: IgxGridComponent,
  })
  public model: GraoPersonModel[];
  public selectionMode: GridSelectionMode = "single";
  public currentPage = 0;
  public itemsPerPage = 5;

  public selectedItem: any;
  public selectedRows = [];

  @Output() selectRow = new EventEmitter<string>();

  ngOnInit(): void {
    super.ngOnInit();
    let id = this.activatedRoute.snapshot.params["ID"];
    // this.service.getGraoPeople(id).subscribe((response) => {
    //   this.model = response;
    // });
  }

  handleRowSelection(event) {
    this.selectRow.emit(event.newSelection[0]);
  }

  // handleRowSelection(event) {
  //   debugger;
  //   let selectedId = event.newSelection[0];
  //   if (selectedId) {
  //     let grid = this.grid as IgxGridComponent;
  //     this.selectedItem = FormUtils.getGridItemById(grid, selectedId);
  //   } else {
  //     this.selectedItem = undefined;
  //   }

  // }
}
