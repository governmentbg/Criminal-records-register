import { Component, OnInit, Injector, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { GenderConstants } from "../../../../@core/constants/gender.constants";
import { CrudForm } from "../../../../@core/directives/crud-form.directive";
import { IsinDataStatusConstants } from "../../../isin/isin-data-form/_models/isin-data-status.constants";
import { EcrisMessageService } from "../_data/ecris-message.service";
import { EcrisMessageForm } from "../_models/ecris-message.form";
import { EcrisMessageModel } from "../_models/ecris-message.model";
import { EcrisIdentificationResolverData } from "./_data/ecris-identification.resolver";

@Component({
  selector: "cais-ecris-identification-form",
  templateUrl: "./ecris-identification-form.component.html",
  styleUrls: ["./ecris-identification-form.component.scss"],
})
export class EcrisIdentificationFormComponent
  extends CrudForm<
    EcrisMessageModel,
    EcrisMessageForm,
    EcrisIdentificationResolverData,
    EcrisMessageService
  >
  implements OnInit
{
  // @ViewChild("ecrisMsgNames", {
  //   read: EcrisMsgNamesOverviewComponent,
  // })
  
  constructor(service: EcrisMessageService, public injector: Injector) {
    super(service, injector);
    this.backUrl = "pages/ecris/identification";
    this.setDisplayTitle("запитване за идентификация");
  }

  public model: EcrisMessageModel;

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: EcrisMessageModel) {
    return new EcrisMessageModel(object);
  }

  ngOnInit(): void {
    this.fullForm = new EcrisMessageForm();
    this.fullForm.group.disable();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();

    let id = this.activatedRoute.snapshot.params["ID"];
    this.service.get(id).subscribe((response) => {
      this.model = response;

      this.model.sex =
        GenderConstants.allData.find((g) => g.id == response.sex)?.name ?? null;
    });

    this.service.getNationalities(id).subscribe((response) => {
      let countries = response;
      countries.map((val) => {
        return val.name;
      });
      this.model.nationalities = countries.join(",");
    });
  }

  updateFunction = () => {
    this.submitFunction();
  };

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };
}
