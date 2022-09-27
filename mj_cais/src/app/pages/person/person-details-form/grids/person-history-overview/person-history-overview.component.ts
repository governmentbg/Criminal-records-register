import { Component, Injector, OnInit } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../../../@core/services/common/loader.service";
import { PersonHistoryGridService } from "./_data/person-history-grid.service";
import { ContextTableEnum } from "./_models/context-table.enum";
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
    public dateFormatService: DateFormatService
  ) {
    super("person-history-grid", service, injector);
    let personIdParams = this.activatedRoute.snapshot.params["ID"];
    this.personId = personIdParams;
    this.service.setPersonId(personIdParams);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  onPreview(tableName, tableId) {
    if (tableName && tableId) {
      if (tableName == ContextTableEnum.Bulletins) {
        this.router.navigateByUrl(`/pages/bulletins/preview/${tableId}`);
      } else if (tableName == ContextTableEnum.Application) {
        this.router.navigateByUrl(`/pages/applications/preview/${tableId}`);
      } else if (tableName == ContextTableEnum.Fbbc) {
        this.router.navigateByUrl(`/pages/fbbcs/preview/${tableId}`);
      } else if (tableName == ContextTableEnum.Report) {
        this.router.navigateByUrl(`/pages/report-applications/preview/${tableId}`);
      } else if (tableName == ContextTableEnum.WApplication) {
        this.router.navigateByUrl(`/pages/applications/preview/${tableId}`);
      }
    }
  }
}
