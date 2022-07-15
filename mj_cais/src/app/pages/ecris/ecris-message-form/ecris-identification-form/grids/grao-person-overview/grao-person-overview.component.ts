import { GraoPersonModel } from "./_models/grao-person.model";
import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { IgxGridComponent } from "@infragistics/igniteui-angular";
import { DateFormatService } from "../../../../../../@core/services/common/date-format.service";
import { EcrisMessageService } from "../../../_data/ecris-message.service";
import { EcrisMessageForm } from "../../../_models/ecris-message.form";
import { RemoteGridWithStatePersistance } from "../../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { GraoPersonGridService } from "./_data/grao-person-grid.service";

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

  public model: GraoPersonModel[];

  ngOnInit(): void {
    super.ngOnInit();
    let id = this.activatedRoute.snapshot.params["ID"];
    this.service.getGraoPeople(id).subscribe((response) => {
      this.model = response;
    });
  }
}
