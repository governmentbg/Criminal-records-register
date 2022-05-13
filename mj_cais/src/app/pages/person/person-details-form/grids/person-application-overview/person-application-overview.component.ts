import { Component, Injector} from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { PersonApplicationGridService } from "./_data/person-application-grid.service";
import { PersonApplicationGridModel } from "./_models/person-bulletin-grid.model";

@Component({
  selector: "cais-person-application-overview",
  templateUrl: "./person-application-overview.component.html",
  styleUrls: ["./person-application-overview.component.scss"],
})
export class PersonApplicationOverviewComponent extends RemoteGridWithStatePersistance<
  PersonApplicationGridModel,
  PersonApplicationGridService
> {
  constructor(
    public service: PersonApplicationGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("application-bulletins-search", service, injector);
    let personId = this.activatedRoute.snapshot.params["ID"];
    this.service.setPersonId(personId);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}