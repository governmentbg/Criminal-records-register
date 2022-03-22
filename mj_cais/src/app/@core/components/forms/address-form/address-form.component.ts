import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from "@angular/core";
import { Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { forkJoin, of } from "rxjs";
import { CommonConstants } from "../../../constants/common.constants";
import { BaseNomenclatureModel } from "../../../models/nomenclature/base-nomenclature.model";
import { NomenclatureService } from "../../../services/rest/nomenclature.service";
import { AutocompleteComponent } from "../inputs/autocomplete/autocomplete.component";
import { AddressForm } from "./model/address.form";

@Component({
  selector: "cais-address-form",
  templateUrl: "./address-form.component.html",
  styleUrls: ["./address-form.component.scss"],
})
export class AddressFormComponent implements OnInit {
  constructor(
    private nomenclatureService: NomenclatureService,
    private route: ActivatedRoute
  ) {}

  public countries: BaseNomenclatureModel[] = [];
  public districts: BaseNomenclatureModel[] = [];
  public municipalities: BaseNomenclatureModel[] = [];
  public cities: BaseNomenclatureModel[] = [];

  public isForPreview: boolean;

  @Output() onCountrySelectionChanged: EventEmitter<string> =
    new EventEmitter<string>();

  @ViewChild("districtAutocomplete")
  districtAutocomplete: AutocompleteComponent;

  @ViewChild("municipalityAutocomplete")
  municipalictyAutocomplete: AutocompleteComponent;

  @ViewChild("cityAutocomplete")
  cityAutocomplete: AutocompleteComponent;

  @Input() parentForm: AddressForm;

  private bgCountryId = CommonConstants.bgCountryId;

  ngOnInit(): void {
    let districtId = this.parentForm.districtId.value ?? 0;
    let municipalityId = this.parentForm.municipalityId.value ?? 0;

    this.isForPreview = this.route.snapshot.data["preview"];

    let emptyData = of([]);

    let munObservable = emptyData;
    if (districtId != 0) {
      munObservable = this.nomenclatureService.getMunicipalities(districtId);
    }

    let citiesObservable = emptyData;
    if (municipalityId != 0) {
      citiesObservable = this.nomenclatureService.getCities(municipalityId);
    }

    forkJoin([
      this.nomenclatureService.getCountries(),
      this.nomenclatureService.getDistricts(),
      munObservable,
      citiesObservable,
    ]).subscribe(([countries, districts, municipalities, cities]) => {
      this.nomenclatureService.saveCountries(countries);
      this.nomenclatureService.saveDistricts(districts);
      this.countries = countries;
      this.districts = districts;
      this.municipalities = municipalities;
      this.cities = cities;

      // Initial selected value
      if (
        (this.parentForm.group.touched &&
          this.parentForm.countryId.value === null) ||
        this.parentForm.countryId.value === null ||
        this.parentForm.countryId.value == this.bgCountryId
      ) {
        this.parentForm.countryId.setValue(this.bgCountryId);
        this.parentForm.setForNativeAddress();
      } else {
        this.parentForm.setForForeignAddress();
      }

      if (this.isForPreview) {
        this.parentForm.group.disable();
      }
    });
  }

  public onCountryChanged(countryId: string): void {
    if (countryId == this.bgCountryId) {
      this.nomenclatureService.getDistricts().subscribe((districts) => {
        this.districtAutocomplete.autoControl.items = districts;
      });
      this.parentForm.setForNativeAddress();
    } else {
      this.parentForm.setForForeignAddress();
    }

    this.onCountrySelectionChanged.emit(countryId);
  }

  public async onDistrictChanged(value: string) {
    this.municipalities = [];

    if (value) {
      this.setMunicipalities(value);
    } else {
      this.municipalities = [];
      this.municipalictyAutocomplete.autoControl.items = [];
      this.municipalictyAutocomplete.autoControl.writeValue(null);
      this.parentForm.municipalityId.setValue(null);

      this.cities = [];
      this.cityAutocomplete.autoControl.items = [];
      this.cityAutocomplete.autoControl.writeValue(null);
      this.parentForm.cityId.setValue(null);
    }
  }

  public async onMunicipalityChanged(value: string) {
    this.cities = [];

    if (value) {
      this.setCities(value);
    } else {
      this.cities = [];
      this.cityAutocomplete.autoControl.items = [];
      this.cityAutocomplete.autoControl.writeValue(null);
      this.parentForm.cityId.setValue(null);
    }
  }

  private setMunicipalities(districtId: string) {
    this.nomenclatureService
      .getMunicipalities(districtId)
      .subscribe((items) => {
        this.municipalities = items;
        this.parentForm.municipalityId.setValidators([Validators.required]);
      });
  }

  private setCities(municipalityId: string) {
    this.nomenclatureService.getCities(municipalityId).subscribe((items) => {
      this.cities = items;
      this.parentForm.cityId.setValidators([Validators.required]);
    });
  }
}
