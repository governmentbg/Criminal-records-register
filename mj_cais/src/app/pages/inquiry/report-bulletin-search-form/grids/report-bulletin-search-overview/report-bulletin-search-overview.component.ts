import { Component, Injector, Input, OnInit } from "@angular/core";
import { IgxExcelExporterOptions } from "@infragistics/igniteui-angular";
import { map } from "rxjs/operators";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { InquirySharedService } from "../../../_data/inquiry-shared.service";
import { ReportBulletinSearchForm } from "../../_models/report-bulletin-search.form";
import { ReportBulletinGridService } from "./_data/report-bulletin-grid.service";
import { ExportBulletinModel } from "../../../_models/export-bulletin.model";

@Component({
  selector: "cais-report-bulletin-search-overview",
  templateUrl: "./report-bulletin-search-overview.component.html",
  styleUrls: ["./report-bulletin-search-overview.component.scss"],
})
export class ReportBulletinSearchOverviewComponent extends RemoteGridWithStatePersistance<
  ExportBulletinModel,
  ReportBulletinGridService
> {
  constructor(
    service: ReportBulletinGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    public sharedService: InquirySharedService
  ) {
    super("report-bulletin-search", service, injector);
  }

  @Input() searchForm: ReportBulletinSearchForm;

  ngOnInit() {}

  ngOnDestroy() {
    this.sharedService.clearInterval();
  }

  public onSearch = () => {
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.sharedService.showSpinner(this.service);
    let filterQuery = this.getFilterQuery();

    this.service.updateUrl(`inquiry/search-bulletins?${filterQuery}`);
    super.ngOnInit();
    this.sharedService.hideSpinner(this.service);
  };

  // Overriding default behaviour
  protected configureExcelExportService() {
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.sharedService.showSpinner(this.service);

    let filterQuery = this.getFilterQuery();

    this.service
      .excelExportBulletins(filterQuery)
      .pipe(
        map((items: []) => {
          return items.map((item) => {
            return this.excelExportMapItem(item);
          });
        })
      )
      .subscribe((data) => {
        // Here are the changes
        let title = this.title ?? "Справка по характеристики на бюлетини";
        let options = new IgxExcelExporterOptions(title);
        options.columnWidth = 30;
        this.exportService.exportData(data, options);
      });
  }

  // Overriding default behaviour
  protected excelExportMapItem(item: ExportBulletinModel) {
    return this.sharedService.excelExportBulletinMapItem(item);
  }

  private getFilterQuery(): string {
    let formObj = this.searchForm.group.getRawValue();
    let offenceCategory = {};
    offenceCategory["offenceCategory"] =
      this.searchForm.offenceCategory.id.value;
    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");
    filterQuery = this.service.constructQueryParamsByFilters(
      offenceCategory,
      filterQuery
    );

    //when has default value date is not of type moment
    if (this.searchForm.fromDate.value && filterQuery.indexOf("fromDate") < 0) {
      filterQuery += `&fromDate=${this.searchForm.fromDate.value.toISOString()}`;
    }
    if (this.searchForm.toDate.value && filterQuery.indexOf("toDate") < 0) {
      filterQuery += `&toDate=${this.searchForm.toDate.value.toISOString()}`;
    }
    return filterQuery;
  }
}
