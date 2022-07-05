import { Component, Injector, Input } from "@angular/core";
import { IgxExcelExporterOptions } from "@infragistics/igniteui-angular";
import { map } from "rxjs";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { InquirySharedService } from "../../../_data/inquiry-shared.service";
import { ExportBulletinModel } from "../../../_models/export-bulletin.model";
import { ReportPersonSearchForm } from "../../_models/report-person-search.form";
import { ReportBulletinByPersonGridService } from "./_data/report-bulletin-by-person-grid.service";

@Component({
  selector: "cais-report-person-search-overview",
  templateUrl: "./report-person-search-overview.component.html",
  styleUrls: ["./report-person-search-overview.component.scss"],
})
export class ReportPersonSearchOverviewComponent extends RemoteGridWithStatePersistance<
  ExportBulletinModel,
  ReportBulletinByPersonGridService
> {
  constructor(
    service: ReportBulletinByPersonGridService,
    injector: Injector,
    public dateFormatService: DateFormatService,
    public sharedService: InquirySharedService
  ) {
    super("report-bulletin-by-person-search", service, injector);
  }

  @Input() searchForm: ReportPersonSearchForm;

  ngOnInit() {}

  public onSearch = () => {
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.sharedService.showSpinner(this.service);
    let filterQuery = this.getFilterQuery();
    this.service.updateUrl(`inquiry/search-bulletins-by-person?${filterQuery}`);
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
      .excelExportBulletinsByPersonData(filterQuery)
      .pipe(
        map((items: []) => {
          return items.map((item) => {
            return this.excelExportMapItem(item);
          });
        })
      )
      .subscribe((data) => {
        // Here are the changes
        let title = this.title ?? "Справка по характеристики на лице";
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

    let birthPlaceObj = {};
    birthPlaceObj["birthPlaceCountryId"] =
      this.searchForm.birthPlace.country.id.value;
    birthPlaceObj["birthPlaceMunicipalityId"] =
      this.searchForm.birthPlace.municipalityId.value;
    birthPlaceObj["birthPlaceDistrictId"] =
      this.searchForm.birthPlace.districtId.value;
    birthPlaceObj["birthPlaceCityId"] = this.searchForm.birthPlace.cityId.value;
    birthPlaceObj["birthPlaceDesc"] =
      this.searchForm.birthPlace.foreignCountryAddress.value;

    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");
    filterQuery = this.service.constructQueryParamsByFilters(
      birthPlaceObj,
      filterQuery
    );

    return filterQuery;
  }
}
