import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { EcrisMessageResolverData } from "./_data/ecris-message.resolver";
import { EcrisMessageService } from "./_data/ecris-message.service";
import { EcrisMessageForm } from "./_models/ecris-message.form";
import { EcrisMessageModel } from "./_models/ecris-message.model";

@Component({
  selector: "cais-ecris-message-form",
  templateUrl: "./ecris-message-form.component.html",
  styleUrls: ["./ecris-message-form.component.scss"],
})
export class EcrisMessageFormComponent
  extends CrudForm<
    EcrisMessageModel,
    EcrisMessageForm,
    EcrisMessageResolverData,
    EcrisMessageService
  >
  implements OnInit
{
  constructor(service: EcrisMessageService, public injector: Injector) {
    super(service, injector);
    this.backUrl = "pages/ecris-req-waiting";
    this.setDisplayTitle("запитване");
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
