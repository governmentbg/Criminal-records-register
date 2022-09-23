import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { PersonGeneratedReportGridService } from "./_data/person-generated-report-grid.service";
import { PersonGeneratedReportGridModel } from "./_models/person-generated-report-grid.model";

@Component({
  selector: "cais-person-generated-report-overview",
  templateUrl: "./person-generated-report-overview.component.html",
  styleUrls: ["./person-generated-report-overview.component.scss"],
})
export class PersonGeneratedReportOverviewComponent extends RemoteGridWithStatePersistance<
  PersonGeneratedReportGridModel,
  PersonGeneratedReportGridService
> {
  public personId: string;

  constructor(
    public service: PersonGeneratedReportGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
  ) {
    super("person-generated-report-search", service, injector);
    let personIdParams = this.activatedRoute.snapshot.params["ID"];
    this.personId = personIdParams;
    this.service.setPersonId(personIdParams);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
