import { Component, OnInit, Injector } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../../@core/directives/crud-form.directive";
import { EcrisMessageService } from "../_data/ecris-message.service";
import { EcrisMessageForm } from "../_models/ecris-message.form";
import { EcrisMessageModel } from "../_models/ecris-message.model";
import { EcrisReqWaitingResolverData } from "./_data/ecris-req-waiting.resolver";

@Component({
  selector: "cais-ecris-req-waiting-form",
  templateUrl: "./ecris-req-waiting-form.component.html",
  styleUrls: ["./ecris-req-waiting-form.component.scss"],
})
export class EcrisReqWaitingFormComponent
  extends CrudForm<
    EcrisMessageModel,
    EcrisMessageForm,
    EcrisReqWaitingResolverData,
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
    // TODO: custom only in tab "Отговор на запитване"
  };
}
