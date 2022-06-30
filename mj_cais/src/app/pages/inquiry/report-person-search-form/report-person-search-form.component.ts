import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { NgxSpinnerService } from "ngx-spinner";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { ReportPersonSearchOverviewComponent } from "./grids/report-person-search-overview/report-person-search-overview.component";
import { ReportPersonResolverData } from "./_data/report-person.resolver";
import { ReportPersonService } from "./_data/report-person.service";
import { ReportPersonSearchForm } from "./_models/report-person-search.form";
import { ReportPersonSearchModel } from "./_models/report-person-search.model";

@Component({
  selector: "cais-report-person-search-form",
  templateUrl: "./report-person-search-form.component.html",
  styleUrls: ["./report-person-search-form.component.scss"],
})
export class ReportPersonSearchFormComponent
  extends CrudForm<
    ReportPersonSearchModel,
    ReportPersonSearchForm,
    ReportPersonResolverData,
    ReportPersonService
  >
  implements OnInit
{
  constructor(
    service: ReportPersonService,
    public injector: Injector,
    private spinner: NgxSpinnerService,
    private dialogService: NbDialogService
  ) {
    super(service, injector);
  }

  @Input() title: string = "Справка по характеристики на лице";

  @ViewChild("bulletinByPersonReportOverview")
  bulletinByPersonReportOverview: ReportPersonSearchOverviewComponent;

  ngOnInit(): void {
    this.fullForm = new ReportPersonSearchForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ReportPersonSearchModel) {
    return new ReportPersonSearchModel(object);
  }

  public onSearch = () => {
    if (!this.fullForm.group.valid) {
      this.fullForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.spinner.show();
    setTimeout(() => {
      /** spinner ends after 5 seconds */
      this.spinner.hide();
    }, 500);

    debugger;
    let formObj = this.fullForm.group.getRawValue();

    let birthPlaceObj= {}
    birthPlaceObj['birthPlaceCountryId'] = this.fullForm.birthPlace.country.id.value;
    birthPlaceObj['birthPlaceMunicipalityId'] = this.fullForm.birthPlace.municipalityId.value;
    birthPlaceObj['birthPlaceDistrictId'] = this.fullForm.birthPlace.districtId.value;
    birthPlaceObj['birthPlaceCityId'] = this.fullForm.birthPlace.cityId.value;
    birthPlaceObj['birthPlaceDesc'] = this.fullForm.birthPlace.foreignCountryAddress.value;

    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");
    filterQuery = this.service.constructQueryParamsByFilters(birthPlaceObj, filterQuery);

    this.bulletinByPersonReportOverview.onSearch(filterQuery);
  };

  
}
