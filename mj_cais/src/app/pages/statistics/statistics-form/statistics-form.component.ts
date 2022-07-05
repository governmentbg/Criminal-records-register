import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { IgxGridComponent } from "@infragistics/igniteui-angular";
import { BaseNomenclatureModel } from "../../../@core/models/nomenclature/base-nomenclature.model";
import { CustomToastrService } from "../../../@core/services/common/custom-toastr.service";
import { LoaderService } from "../../../@core/services/common/loader.service";
import { StatisticsService } from "../_data/statistics.service";
import { StatisticsResultModel } from "../_models/statistics-result.model";
import { StatisticsSearchForm } from "../_models/statistics-search.form";

@Component({
  selector: "cais-statistics-form",
  templateUrl: "./statistics-form.component.html",
  styleUrls: ["./statistics-form.component.scss"],
})
export class StatisticsFormComponent implements OnInit {
  @Input() form: StatisticsSearchForm;
  @Input() authorities: BaseNomenclatureModel[];
  @Input() title: string;
  @Input() apiMethodName: string;

  constructor(
    private loaderService: LoaderService,
    private service: StatisticsService,
    private toastr: CustomToastrService
  ) {}

  public data: StatisticsResultModel[];
  public item: string = "";
  @ViewChild("dataGrid", { read: IgxGridComponent, static: true })
  public showChart: boolean = false;
  public showGrid: boolean = false;

  ngOnInit(): void {}

  public onSearch = () => {
    debugger;
    if (!this.form.group.valid) {
      this.form.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.loaderService.showSpinner(this.service);
    let filterQuery = `${this.apiMethodName}?`;

    filterQuery = this.service.constructQueryParamsByFilters(
      this.form.group.getRawValue(),
      filterQuery
    );

    this.service.getStatistics(filterQuery).subscribe(
      (data) => {
        this.data = data;
        this.service.isLoading = false;
        this.loaderService.hideSpinner(this.service);
        let elements = this.data.filter((x) => x.count > 0);
        this.showChart = elements.length > 0;
        this.showGrid = true;
      },
      (error) => {
        this.service.isLoading = false;
        this.loaderService.hideSpinner(this.service);
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast("danger", 'Възникна грешка', errorText);
      }
    );
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
