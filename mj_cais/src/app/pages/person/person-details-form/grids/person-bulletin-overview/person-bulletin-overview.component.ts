import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { PersonBulletinGridService } from "./_data/person-bulletin-grid.service";
import { PersonBulletinGridModel } from "./_models/person-bulletin-grid.model";

@Component({
  selector: "cais-person-bulletin-overview",
  templateUrl: "./person-bulletin-overview.component.html",
  styleUrls: ["./person-bulletin-overview.component.scss"],
})
export class PersonBulletinOverviewComponent extends RemoteGridWithStatePersistance<
  PersonBulletinGridModel,
  PersonBulletinGridService
> {
  public personId: string;
  constructor(
    public service: PersonBulletinGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
    public loaderService: LoaderService,
  ) {
    super("person-bulletins-search", service, injector);
    let personIdParams = this.activatedRoute.snapshot.params["ID"];
    this.personId = personIdParams;
    this.service.setPersonId(personIdParams);
    this.loaderService.showSpinner(this.service);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
