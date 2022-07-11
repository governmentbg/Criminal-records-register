import { Component, Injector} from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { PersonPidGridService } from "./_data/person-pid-grid.service";
import { PersonPidGridModel } from "./_models/person-pid-grid.model";

@Component({
  selector: "cais-person-pid-overview",
  templateUrl: "./person-pid-overview.component.html",
  styleUrls: ["./person-pid-overview.component.scss"],
})
export class PersonPidOverviewComponent extends RemoteGridWithStatePersistance<
  PersonPidGridModel,
  PersonPidGridService
> {
  public personId: string;

  constructor(
    public service: PersonPidGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
    public loaderService: LoaderService
  ) {
    super("person-pids-search", service, injector);
  }

  ngOnInit() {
    let personIdParams = this.activatedRoute.snapshot.params["ID"];
    this.personId = personIdParams;
    this.service.setPersonId(personIdParams);
    this.loaderService.showSpinner(this.service);
    super.ngOnInit();
  }
}
