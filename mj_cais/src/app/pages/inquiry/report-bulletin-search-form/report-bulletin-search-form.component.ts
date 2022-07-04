import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { OffenceCategoryDialogComponent } from "../../../@core/components/dialogs/offence-category-dialog/offence-category-dialog.component";
import { OffenceCategoryGridModel } from "../../../@core/components/dialogs/offence-category-dialog/_models/offence-category-grid.model";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { ReportBulletinSearchOverviewComponent } from "./grids/report-bulletin-search-overview/report-bulletin-search-overview.component";
import { ReportBulletinResolverData } from "./_data/report-bulletin.resolver";
import { ReportBulletinService } from "./_data/report-bulletin.service";
import { ReportBulletinSearchForm } from "./_models/report-bulletin-search.form";
import { ReportBulletinSearchModel } from "./_models/report-bulletin-search.model";

@Component({
  selector: "cais-report-bulletin-search-form",
  templateUrl: "./report-bulletin-search-form.component.html",
  styleUrls: ["./report-bulletin-search-form.component.scss"],
})
export class ReportBulletinSearchFormComponent
  extends CrudForm<
    ReportBulletinSearchModel,
    ReportBulletinSearchForm,
    ReportBulletinResolverData,
    ReportBulletinService
  >
  implements OnInit
{
  constructor(
    service: ReportBulletinService,
    public injector: Injector,
    private dialogService: NbDialogService,
  ) {
    super(service, injector);
  }

  @Input() title: string = "Справка по характеристики на бюлетини";

  @ViewChild("bulletinReportOverview")
  bulletinReportOverview: ReportBulletinSearchOverviewComponent;

  ngOnInit(): void {
    this.fullForm = new ReportBulletinSearchForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ReportBulletinSearchModel) {
    return new ReportBulletinSearchModel(object);
  }

  public onSearch = () => {
    this.bulletinReportOverview.onSearch();
  };

  public openOffenceCategoryDialog = () => {
    this.dialogService
      .open(OffenceCategoryDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe(this.onSelectOffenceCategory);
  };

  public onSelectOffenceCategory = (item: OffenceCategoryGridModel) => {
    if (item) {
      this.fullForm.offenceCategory.setValue(
        item.id,
        item.name + ", " + item.code
      );
    }
  };
}
