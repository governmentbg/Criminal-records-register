import { Component, Input, OnInit, ViewChild } from "@angular/core";
import {
  IgxDialogComponent,
  IgxGridComponent,
  IgxGridRowComponent,
} from "@infragistics/igniteui-angular";
import { BulletinOffenceForm } from "./_models/bulletin-offance.form";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { InputTypeConstants } from "../../../../../@core/constants/input-type.constants";
import { CommonConstants } from "../../../../../@core/constants/common.constants";
import { AddressFormComponent } from "../../../../../@core/components/forms/address-form/address-form.component";
import { forkJoin, of } from "rxjs";
import { NomenclatureService } from "../../../../../@core/services/rest/nomenclature.service";
import { NbDialogService } from "@nebular/theme";
import { OffenceCategoryDialogComponent } from "../../../../../@core/components/dialogs/offence-category-dialog/offence-category-dialog.component";
import { OffenceCategoryGridModel } from "../../../../../@core/components/dialogs/offence-category-dialog/_models/offence-category-grid.model";
import { BulletinService } from "../../_data/bulletin.service";
import { ActivatedRoute, ActivatedRouteSnapshot } from "@angular/router";
import { BulletinOffenceModel } from "./_models/bulletin-offence.model";
import { EActions } from "@tl/tl-common";
import { CommonErrorHandlerService } from "../../../../../@core/services/common/common-error-handler.service";

@Component({
  selector: "cais-bulletin-offences-form",
  templateUrl: "./bulletin-offences-form.component.html",
  styleUrls: ["./bulletin-offences-form.component.scss"],
})
export class BulletinOffencesFormComponent implements OnInit {
  @Input() bulletinOffenceTransactions: string;
  @Input() dbData: any;

  @Input() isForPreview: boolean;
  public InputTypeConstants = InputTypeConstants;

  @ViewChild("offencesGrid", {
    read: IgxGridComponent,
  })
  public offencesGrid: IgxGridComponent;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  public bulletinOffenceForm = new BulletinOffenceForm();

  @ViewChild("offPlace", {
    read: AddressFormComponent,
  })
  public offPlaceComponent: AddressFormComponent;

  public offences: BulletinOffenceModel[];

  constructor(
    public dateFormatService: DateFormatService,
    private nomenclatureService: NomenclatureService,
    private dialogService: NbDialogService,
    private bulletinService: BulletinService,
    private errorService: CommonErrorHandlerService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    if (
      this.activatedRoute.snapshot.routeConfig.path.toUpperCase() ===
      EActions.CREATE
    ) {
      this.offences = [];
      return;
    }

    let bulletinId = this.activatedRoute.snapshot.params["ID"];
    this.bulletinService.getOffences(bulletinId).subscribe({
      next: (response) => {
        this.offences = response;
      },
      error: (errorResponse) => {
        this.errorService.errorHandler(errorResponse);
      },
    });
  }

  public onOpenEditBulletinOffence(event: IgxGridRowComponent) {
    this.updateOffPlaceObj(event);
    this.dateFormatService.parseDatesFromGridRow(event, [
      "offStartDate",
      "offEndDate",
    ]);
    this.bulletinOffenceForm.group.patchValue(event.rowData);
    this.dialog.open();
  }

  public onDeleteBulletinOffence(event: IgxGridRowComponent) {
    this.offencesGrid.deleteRow(event.rowData.id);
    this.offencesGrid.data = this.offencesGrid.data.filter(
      (d) => d.id != event.rowData.id
    );
  }

  onAddOrUpdateBulletineOffenceRow() {
    if (!this.bulletinOffenceForm.group.valid) {
      this.bulletinOffenceForm.group.markAllAsTouched();
      return;
    }

    if (this.bulletinOffenceForm.ecrisOffCatId.value) {
      let ecrisOffCatName = (this.dbData.ecrisOffCategories as any).find(
        (x) => x.id === this.bulletinOffenceForm.ecrisOffCatId.value
      ).name;

      this.bulletinOffenceForm.ecrisOffCatName.patchValue(ecrisOffCatName);
    }

    if (this.bulletinOffenceForm.formOfGuiltId.value) {
      let formOfGuiltName = (this.dbData.formOfGuilts as any).find(
        (x) => x.id === this.bulletinOffenceForm.formOfGuiltId.value
      ).name;

      this.bulletinOffenceForm.formOfGuiltName.patchValue(formOfGuiltName);
    }

    let currentRow = this.offencesGrid.getRowByKey(
      this.bulletinOffenceForm.id.value
    );

    if (currentRow) {
      currentRow.update(this.bulletinOffenceForm.group.value);
    } else {
      this.offencesGrid.addRow(this.bulletinOffenceForm.group.value);
    }

    this.onCloseBulletinOffenceDilog();
  }

  onCloseBulletinOffenceDilog() {
    this.bulletinOffenceForm = new BulletinOffenceForm();
    this.dialog.close();
  }

  //#region Offance Category

  public openOffenceCategoryDialog = () => {
    this.dialogService
      .open(OffenceCategoryDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe(this.onSelectOffenceCategory);
  };

  public onSelectOffenceCategory = (item: OffenceCategoryGridModel) => {
    if (item) {
      this.bulletinOffenceForm.offenceCategory.setValue(
        item.id,
        item.name + ", " + item.code
      );
    }
  };

  //#endregion

  private updateOffPlaceObj(event) {
    var selectedCountryId = event.rowData.offPlace.country.id;

    var isBgCountry = selectedCountryId == CommonConstants.bgCountryId;
    if (!isBgCountry) {
      this.bulletinOffenceForm.offPlace.setForForeignAddress();
      return;
    }

    // update data for dropdowns
    let emptyData = of([]);

    let munObservable = emptyData;
    let mustUpdateMun = event.rowData.offPlace.districtId;

    if (mustUpdateMun) {
      munObservable = this.nomenclatureService.getMunicipalities(
        event.rowData.offPlace.districtId
      );
    }

    let citiesObservable = emptyData;
    var mustUpdateCities = event.rowData.offPlace.cityId;

    if (mustUpdateCities) {
      citiesObservable = this.nomenclatureService.getCities(
        event.rowData.offPlace.municipalityId
      );
    }

    if (mustUpdateCities || mustUpdateMun) {
      forkJoin([munObservable, citiesObservable]).subscribe(
        ([municipalities, cities]) => {
          this.offPlaceComponent.municipalities = municipalities;
          this.offPlaceComponent.cities = cities;
        }
      );
    }

    // show description in address
    this.bulletinOffenceForm.offPlace.setForNativeAddress(true);
  }
}
