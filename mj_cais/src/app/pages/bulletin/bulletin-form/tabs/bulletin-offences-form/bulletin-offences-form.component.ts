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
import { OffenceCategoryDialogComponent } from "./dialog/offence-category-dialog/offence-category-dialog.component";
import { OffenceCategoryGridModel } from "./dialog/_models/offence-category-grid.model";

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

  constructor(
    public dateFormatService: DateFormatService,
    private nomenclatureService: NomenclatureService,
    private dialogService: NbDialogService
  ) {}

  ngOnInit(): void {}

  public onOpenEditBulletinOffence(event: IgxGridRowComponent) {
    this.updateOffPlaceObj(event);
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

    this.bulletinOffenceForm.offPlace.setForNativeAddress();
  }
}
