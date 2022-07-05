import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { UserAuthorityService } from "../../../@core/services/common/user-authority.service";
import { InquirySharedService } from "../_data/inquiry-shared.service";
import { StatisticsService } from "./_data/statistics.service";
import { StatisticsResolverData } from "./_data/statistics.resolver";
import { StatisticsSearchForm } from "./_model/statistics-search.form";
import { StatisticsSearchModel } from "./_model/statistics-search.model";
import { StatisticsResultModel } from "./_model/statistics-result.model";
import { IgxGridComponent } from "@infragistics/igniteui-angular";

@Component({
  selector: "cais-statistics",
  templateUrl: "./statistics.component.html",
  styleUrls: ["./statistics.component.scss"],
})
export class StatisticsComponent
  extends CrudForm<
    StatisticsSearchModel,
    StatisticsSearchForm,
    StatisticsResolverData,
    StatisticsService
  >
  implements OnInit
{
  constructor(
    service: StatisticsService,
    public injector: Injector,
    private userAuthService: UserAuthorityService,
    private sharedService: InquirySharedService
  ) {
    super(service, injector);
  }

  @Input() title: string = "Статистика";
  public data: StatisticsResultModel[];
  public item: string = "";
  @ViewChild("dataGrid", { read: IgxGridComponent, static: true })
  public showChart: boolean = false;
  public showGrid: boolean = false;

  ngOnInit(): void {
    this.fullForm = new StatisticsSearchForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.fullForm.authority.patchValue(this.userAuthService.csAuthorityId);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: StatisticsSearchModel) {
    return new StatisticsSearchModel(object);
  }

  public onSearch = () => {
    if (!this.fullForm.group.valid) {
      this.fullForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.sharedService.showSpinner(this.service);
    let filterQuery = this.service.constructQueryParamsByFilters(
      this.fullForm.group.getRawValue()
    );
    this.service.getStatistics(filterQuery).subscribe((data) => {
      this.data = data;
      this.service.isLoading = false;
      this.sharedService.hideSpinner(this.service);
      let elements = this.data.filter((x) => x.count > 0);
      this.showChart = elements.length > 0;
      this.showGrid = true;
    });
  };

  private selectCondition = (rowData: any, columnKey: any): boolean => {
    return rowData["objectType"] === this.item;
  };

  private deselectCondition = (rowData: any, columnKey: any): boolean => {
    return rowData["objectType"] !== this.item;
  };

  public condClasses = {
    deselect: this.deselectCondition,
    select: this.selectCondition,
  };

  public itemSelection(event: any) {
    if (this.item === event.args.dataContext.objectType) {
      this.item = "";
    } else {
      this.item = event.args.dataContext.objectType;
    }

    this.condClasses = {
      deselect: this.deselectCondition,
      select: this.selectCondition,
    };
  }
}
