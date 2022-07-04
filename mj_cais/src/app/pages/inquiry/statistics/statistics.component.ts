import { ChangeDetectorRef, Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { UserAuthorityService } from "../../../@core/services/common/user-authority.service";
import { InquirySharedService } from "../_data/inquiry-shared.service";
import { StatisticsService } from "./_data/statistics.service";
import { StatisticsResolverData } from "./_data/statistics.resolver";
import { StatisticsSearchForm } from "./_model/statistics-search.form";
import { StatisticsSearchModel } from "./_model/statistics-search.model";
import { map } from "rxjs";
import { StatisticsResultModel } from "./_model/statistics-result.model";
import { IgxGridComponent } from "@infragistics/igniteui-angular";
// import { IgxItemLegendComponent, IgxPieChartComponent } from "igniteui-angular-charts";

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
    private sharedService: InquirySharedService,
    private _detector: ChangeDetectorRef
  ) {
    super(service, injector);
  }


  // @ViewChild("legend", { static: true } )
  // private legend: IgxItemLegendComponent
  // @ViewChild("chart", { static: true } )
  // private chart: IgxPieChartComponent

  private _energyGlobalDemand: EnergyGlobalDemand = null;
  public get energyGlobalDemand(): EnergyGlobalDemand {
      if (this._energyGlobalDemand == null)
      {
          this._energyGlobalDemand = new EnergyGlobalDemand();
      }
      return this._energyGlobalDemand;
  }
  

  @Input() title: string = "Статистика";
  public data: StatisticsResultModel[];
  public item: string = "";
  @ViewChild("dataGrid", { read: IgxGridComponent, static: true })
  public grid: IgxGridComponent;
  public showChart: boolean = false;

  ngOnInit(): void {
    this.fullForm = new StatisticsSearchForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.fullForm.authorityId.patchValue(this.userAuthService.csAuthorityId);
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
    debugger;
    let filterQuery = this.service.constructQueryParamsByFilters(
      this.fullForm.group.getRawValue()
    );
    this.service.getStatistics(filterQuery).subscribe((data) => {
      debugger;
      this.data = data;
      this.service.isLoading = false;
      this.sharedService.hideSpinner(this.service);
      this.showChart = true;
    });
  };

  private selectCondition = (rowData: any, columnKey: any): boolean => {
    return rowData["Company"] === this.item;
  };

  private deselectCondition = (rowData: any, columnKey: any): boolean => {
    return rowData["Company"] !== this.item;
  };

  public condClasses = {
    deselect: this.deselectCondition,
    select: this.selectCondition,
  };

  public itemSelection(event: any) {
    debugger;
    if (this.item === event.args.dataContext.ObjectType) {
      this.item = "";
    } else {
      this.item = event.args.dataContext.ObjectType;
    }

    this.condClasses = {
      deselect: this.deselectCondition,
      select: this.selectCondition,
    };
    this.grid.reflow();
  }
}

export class EnergyGlobalDemandItem {
  public constructor(init: Partial<EnergyGlobalDemandItem>) {
      Object.assign(this, init);
  }
  
  public value: number;
  public category: string;
  public summary: string;

}
export class EnergyGlobalDemand extends Array<EnergyGlobalDemandItem> {
  public constructor() {
      super();
      this.push(new EnergyGlobalDemandItem(
      {
          value: 37,
          category: `Cooling`,
          summary: `Cooling 37%`
      }));
      this.push(new EnergyGlobalDemandItem(
      {
          value: 25,
          category: `Residential`,
          summary: `Residential 25%`
      }));
      this.push(new EnergyGlobalDemandItem(
      {
          value: 12,
          category: `Heating`,
          summary: `Heating 12%`
      }));
      this.push(new EnergyGlobalDemandItem(
      {
          value: 11,
          category: `Lighting`,
          summary: `Lighting 11%`
      }));
      this.push(new EnergyGlobalDemandItem(
      {
          value: 15,
          category: `Other`,
          summary: `Other 15%`
      }));
  }
}
