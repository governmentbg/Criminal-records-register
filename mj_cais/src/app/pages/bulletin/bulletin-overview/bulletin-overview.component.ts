import {
  ChangeDetectorRef,
  Component,
  Injector,
  OnInit,
  ViewChild,
} from "@angular/core";
import { IgxGridComponent } from "@infragistics/igniteui-angular";
import { NbDialogService } from "@nebular/theme";
import { debounceTime, Subject, takeUntil } from "rxjs";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { BulletinGridModel } from "./data/bulletin-grid.model";
import { BulletinService } from "./data/bulletin.service";

@Component({
  selector: "cais-bulletin-overview",
  templateUrl: "./bulletin-overview.component.html",
  styleUrls: ["./bulletin-overview.component.scss"],
})
export class BulletinOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinService
> {
  constructor(
    private dialogService: NbDialogService,
    service: BulletinService,
    injector: Injector,
    public cdr: ChangeDetectorRef
  ) {
    super("bulletins-search", service, injector);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
