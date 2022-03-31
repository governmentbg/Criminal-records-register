import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../../@core/directives/crud-form.directive";
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
  constructor(service: EcrisMessageService, public injector: Injector) {
    super(service, injector);
    this.backUrl = "pages/ecris-identification";
    this.setDisplayTitle("запитване за идентификация");
  }

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
  }

  updateFunction = () => {
    this.submitFunction();
  };

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };
}
