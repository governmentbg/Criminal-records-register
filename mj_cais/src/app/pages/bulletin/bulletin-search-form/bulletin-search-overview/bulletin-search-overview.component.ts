import { Component, Injector, Input, OnInit } from "@angular/core";
import { IgxExcelExporterOptions } from "@infragistics/igniteui-angular";
import { map } from "rxjs";
import { CommonConstants } from "../../../../@core/constants/common.constants";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { BulletinSearchForm } from "../_models/bulletin-search.form";
import { BulletinSearchGridService } from "./_data/bulletin-search-grid.service";
import { BulletinSearchGridModel } from "./_models/bulletin-search-grid.model";

@Component({
  selector: "cais-bulletin-search-overview",
  templateUrl: "./bulletin-search-overview.component.html",
  styleUrls: ["./bulletin-search-overview.component.scss"],
})
export class BulletinSearchOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinSearchGridModel,
  BulletinSearchGridService
> {
  constructor(
    service: BulletinSearchGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("bulletins-search", service, injector);
  }

  @Input() searchForm: BulletinSearchForm;

  ngOnInit() {
    this.onSearch();
  }

  public onSearch = () => {
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }
    this.service.updateUrl(`bulletins/search?${this.getFilterQuery()}`);
    super.ngOnInit();
  };

  // Overriding default behaviour
  protected configureExcelExportService() {
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.service
      .excelExportBulletins(this.getFilterQuery())
      .pipe(
        map((items: []) => {
          return items.map((item) => {
            return this.excelExportMapItem(item);
          });
        })
      )
      .subscribe((data) => {
        let title = this.title ?? "Бюлетини";
        let options = new IgxExcelExporterOptions(title);
        options.columnWidth = 30;
        this.exportService.exportData(data, options);
      });
  }

  protected excelExportMapItem(item: BulletinSearchGridModel) {
    let result = {};
    result["Номер на бюлетин"] = item.registrationNumber;
    result["Съд изготвил бюлетина"] = item.bulletinAuthorityName;
    result["Тип"] = item.bulletinType;
    result["Дело номер/година"] = item.caseData;
    result["Статус"] = item.statusName;
    result["Имена"] = item.fullName;
    result["ЕГН/ЛНЧ"] = item.identifier;
    result["Дата на раждане"] = new Date(item.birthDate).toLocaleDateString(
      CommonConstants.bgLocale
    );

    return result;
  }

  private getFilterQuery(): string {
    let formObj = this.searchForm.group.getRawValue();
    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");
    return filterQuery;
  }
}
