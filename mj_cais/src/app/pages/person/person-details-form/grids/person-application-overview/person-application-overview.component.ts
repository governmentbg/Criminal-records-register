import { Component, Injector} from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { PersonApplicationGridService } from "./_data/person-application-grid.service";
import { PersonApplicationGridModel } from "./_models/person-application-grid.model";

@Component({
  selector: "cais-person-application-overview",
  templateUrl: "./person-application-overview.component.html",
  styleUrls: ["./person-application-overview.component.scss"],
})
export class PersonApplicationOverviewComponent extends RemoteGridWithStatePersistance<
  PersonApplicationGridModel,
  PersonApplicationGridService
> {
  public personId: string;

  constructor(
    public service: PersonApplicationGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
  ) {
    super("person-application-search", service, injector);
  }

  ngOnInit() {
    let personIdParams = this.activatedRoute.snapshot.params["ID"];
    this.personId = personIdParams;
    this.service.setPersonId(personIdParams);
    super.ngOnInit();
  }
}
