import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { PersonEApplicationGridService } from "./_data/person-eapplication-grid.service";
import { PersonEApplicationGridModel } from "./_models/person-eapplication-grid.model";

@Component({
  selector: "cais-person-eapplication-overview",
  templateUrl: "./person-eapplication-overview.component.html",
  styleUrls: ["./person-eapplication-overview.component.scss"],
})
export class PersonEApplicationOverviewComponent extends RemoteGridWithStatePersistance<
  PersonEApplicationGridModel,
  PersonEApplicationGridService
> {
  public personId: string;

  constructor(
    public service: PersonEApplicationGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
    public loaderService: LoaderService
  ) {
    super("person-e-application-search", service, injector);
  }

  ngOnInit() {
    let personIdParams = this.activatedRoute.snapshot.params["ID"];
    this.personId = personIdParams;
    this.service.setPersonId(personIdParams);
    this.loaderService.showSpinner(this.service);
    super.ngOnInit();
  }
}
