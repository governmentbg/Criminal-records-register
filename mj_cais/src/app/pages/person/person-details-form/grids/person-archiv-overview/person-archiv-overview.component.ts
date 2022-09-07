import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { PersonArchiveGridService } from "./_data/person-archive-grid.service";
import { PersonArchiveGridModel } from "./_models/person-archive-grid.model";

@Component({
  selector: "cais-person-archiv-overview",
  templateUrl: "./person-archiv-overview.component.html",
  styleUrls: ["./person-archiv-overview.component.scss"],
})
export class PersonArchivOverviewComponent extends RemoteGridWithStatePersistance<
  PersonArchiveGridModel,
  PersonArchiveGridService
> {
  public personId: string;

  constructor(
    public service: PersonArchiveGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
    public loaderService: LoaderService
  ) {
    super("person-archive-search", service, injector);
  }

  ngOnInit() {
    let personIdParams = this.activatedRoute.snapshot.params["ID"];
    this.personId = personIdParams;
    this.service.setPersonId(personIdParams);
    this.loaderService.showSpinner(this.service);
    super.ngOnInit();
  }
}
