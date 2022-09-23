import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { PersonHistoryGridService } from "./_data/person-history-grid.service";
import { PersonHistoryGridModel } from "./_models/person-history-grid.model";

@Component({
  selector: "cais-person-history-overview",
  templateUrl: "./person-history-overview.component.html",
  styleUrls: ["./person-history-overview.component.scss"],
})
export class PersonHistoryOverviewComponent extends RemoteGridWithStatePersistance<
  PersonHistoryGridModel,
  PersonHistoryGridService
> {
  public personId: string;

  constructor(
    public service: PersonHistoryGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
  ) {
    super("person-history-grid", service, injector);
    let personIdParams = this.activatedRoute.snapshot.params["ID"];
    this.personId = personIdParams;
    this.service.setPersonId(personIdParams);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
