import { Component, Injector} from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { PersonFbbcGridService } from "./_data/person-fbbc-grid.service";
import { PersonFbbcGridModel } from "./_models/person-bulletin-grid";

@Component({
  selector: "cais-person-fbbc-overview",
  templateUrl: "./person-fbbc-overview.component.html",
  styleUrls: ["./person-fbbc-overview.component.scss"],
})
export class PersonFbbcOverviewComponent extends RemoteGridWithStatePersistance<
  PersonFbbcGridModel,
  PersonFbbcGridService
> {
  constructor(
    public service: PersonFbbcGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("person-fbbc-search", service, injector);
    let personId = this.activatedRoute.snapshot.params["ID"];
    this.service.setPersonId(personId);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
