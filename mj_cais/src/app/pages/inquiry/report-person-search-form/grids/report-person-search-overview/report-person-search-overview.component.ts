import { Component, Injector, OnInit } from '@angular/core';
import { RemoteGridWithStatePersistance } from '../../../../../@core/directives/remote-grid-with-state-persistance.directive';
import { DateFormatService } from '../../../../../@core/services/common/date-format.service';
import { ReportBulletinByPersonGridService } from './_data/report-bulletin-by-person-grid.service';
import { ReportBulletinByPersonGridModel } from './_models/report-bulletin-by-person.model';

@Component({
  selector: 'cais-report-person-search-overview',
  templateUrl: './report-person-search-overview.component.html',
  styleUrls: ['./report-person-search-overview.component.scss']
})
export class ReportPersonSearchOverviewComponent extends RemoteGridWithStatePersistance<
ReportBulletinByPersonGridModel,
ReportBulletinByPersonGridService
> {
constructor(
  service: ReportBulletinByPersonGridService,
  injector: Injector,
  public dateFormatService: DateFormatService
) {
  super("report-bulletin-by-person-search", service, injector);
}

ngOnInit() {

}

public onSearch = (filterQuery: string) => {
  this.service.updateUrl(`inquiry/search-bulletins-by-person?${filterQuery}`);
  super.ngOnInit();
};
}
