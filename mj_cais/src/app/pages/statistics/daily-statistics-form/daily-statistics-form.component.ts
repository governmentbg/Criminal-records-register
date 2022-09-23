import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { BaseNomenclatureModel } from "../../../@core/models/nomenclature/base-nomenclature.model";

import { DailyStatisticsSearchForm } from "./_models/daily-statistics-search.form";
import { DailyStatisticsResolverData } from "./_data/daily-statistics.resolver";
import { DailyStatisticsService } from "./_data/daily-statistics.service";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { FormGroup } from "@angular/forms";
import { DailyStatisticsSearchModel } from "./_models/daily-statistics-search.model";
import { DailyStatisticsConstants } from "./_models/daily-statistics-constants";
import { filter } from "rxjs";

@Component({
  selector: "cais-daily-statistics-form",
  templateUrl: "./daily-statistics-form.component.html",
  styleUrls: ["./daily-statistics-form.component.scss"],
})
export class DailyStatisticsFormComponent extends CrudForm<
  DailyStatisticsSearchModel,
  DailyStatisticsSearchForm,
  DailyStatisticsResolverData,
  DailyStatisticsService
> {
  //@Input() title: string;
  //@Input() apiMethodName: string;

  constructor(service: DailyStatisticsService, public injector: Injector) {
    super(service, injector);
  }

  // public data: DailyStatisticsSearchModel[];
  // public item: string = "";
  // @ViewChild("dataGrid", { read: IgxGridComponent, static: true })
  // public showChart: boolean = false;
  // public showGrid: boolean = false;
  public buletinStatuses: BaseNomenclatureModel[];
  public aplicationStatuses: BaseNomenclatureModel[];
  public certificateStatuses: BaseNomenclatureModel[];
  public reportAplicationStatuses: BaseNomenclatureModel[];
  public reportStatuses: BaseNomenclatureModel[];
  public currentStatuses: BaseNomenclatureModel[] = [];

  ngOnInit(): void {
    this.fullForm = new DailyStatisticsSearchForm();
    this.fullForm.group.patchValue(this.dbData.element);
    //бюлетини
    this.buletinStatuses = this.dbData.buletinStatuses as any;
    //заявления
    this.aplicationStatuses = (this.dbData.aplicationStatuses as any).filter(
      (s) => s.type == "APP"
    );
    //свидетелства
    this.certificateStatuses = (this.dbData.aplicationStatuses as any).filter(
      (s) => s.type == "CERT"
    );
    //искания
    this.reportAplicationStatuses = (this.dbData.reportStatuses as any).filter(
      (s) => s.type == "APP"
    );
    //справки
    this.reportStatuses = (this.dbData.reportStatuses as any).filter(
      (s) => s.type == "REP"
    );
  }

  onSearch() {
    //todo: pass values to an API
    //return PDF document?
    if (!this.fullForm.group.valid) {
      this.fullForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      this.scrollToValidationError();

      return;
    }

    let formObj = this.fullForm.group.getRawValue();
    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");

    this.service.getReport(filterQuery).subscribe({
      next: (response) => {
        this.downloadFile(response);
      },
      error: (errorResponse) => {
        this.errorHandler(errorResponse);
      },
    });
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: DailyStatisticsSearchModel) {
    return new DailyStatisticsSearchModel(object);
  }

  onStatisticsTypesChanged() {
    let currentDropdownValue = this.fullForm.statisticsType.value;

    switch (currentDropdownValue) {
      case DailyStatisticsConstants.Bulletin.id: {
        this.currentStatuses = this.buletinStatuses;
        break;
      }
      case DailyStatisticsConstants.Application.id: {
        this.currentStatuses = this.aplicationStatuses;
        break;
      }
      case DailyStatisticsConstants.Certificate.id: {
        this.currentStatuses = this.certificateStatuses;
        break;
      }
      case DailyStatisticsConstants.ReportApplication.id: {
        this.currentStatuses = this.reportAplicationStatuses;
        break;
      }
      case DailyStatisticsConstants.Report.id: {
        this.currentStatuses = this.reportStatuses;
        break;
      }
      default: {
        this.currentStatuses = [];
        break;
      }
    }
  }
}
