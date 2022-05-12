import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
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
  constructor(
    public service: PersonBulletinGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("person-bulletins-search", service, injector);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
