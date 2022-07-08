import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { CountryDialogComponent } from "../../../@core/components/forms/address-form/dialog/country-dialog/country-dialog.component";
import { CountryGridModel } from "../../../@core/components/forms/address-form/dialog/_models/country-grid.model";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { NationalityTypeConstants } from "../../../@core/constants/nationality-type.constants";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { UserAuthorityService } from "../../../@core/services/common/user-authority.service";
import { ExportBulletinModel } from "../_models/export-bulletin.model";
import { ReportPersonSearchOverviewComponent } from "./grids/report-person-search-overview/report-person-search-overview.component";
import { ReportPersonResolverData } from "./_data/report-person.resolver";
import { ReportPersonService } from "./_data/report-person.service";
import { ReportPersonSearchForm } from "./_models/report-person-search.form";

@Component({
  selector: "cais-report-person-search-form",
  templateUrl: "./report-person-search-form.component.html",
  styleUrls: ["./report-person-search-form.component.scss"],
})
export class ReportPersonSearchFormComponent
  extends CrudForm<
    ExportBulletinModel,
    ReportPersonSearchForm,
    ReportPersonResolverData,
    ReportPersonService
  >
  implements OnInit
{
  constructor(
    service: ReportPersonService,
    public injector: Injector,
    private dialogService: NbDialogService,
    private userAuthService: UserAuthorityService
  ) {
    super(service, injector);
  }

  @Input() title: string = "Справка по характеристики на лице";

  @ViewChild("bulletinByPersonReportOverview")
  bulletinByPersonReportOverview: ReportPersonSearchOverviewComponent;

  public showCountryLookup: boolean = false;

  ngOnInit(): void {
    this.fullForm = new ReportPersonSearchForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.fullForm.authorityId.patchValue(this.userAuthService.csAuthorityId);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ExportBulletinModel) {
    return new ExportBulletinModel(object);
  }

  public onSearch = () => {
    this.bulletinByPersonReportOverview.onSearch();
  };

  public openCountryDialog = () => {
    this.dialogService
      .open(CountryDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe(this.onSelectCountry);
  };

  public onSelectCountry = (item: CountryGridModel) => {
    if (item) {
      this.fullForm.nationalityCountry.setValue(item.id, item.name);
      this.fullForm.nationalityCountryId.setValue(item.id);
    }
  };

  public onNationalityTypeChanged(nationalityTypeCode) {
    this.showCountryLookup =
      nationalityTypeCode == NationalityTypeConstants.currentCountry.code;
  }

  public onNationalityTypeCleared(event) {
    debugger;
    this.fullForm.nationalityCountryId.setValue(null);
  }
}
