import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup, Validators } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { IRStatusTypeEnum } from "../internal-request-overview/_models/internal-request-status-type.constants";
import { InternalRequestResolverData } from "./_data/internal-request.resolver";
import { InternalRequestService } from "./_data/internal-request.service";
import { InternalRequestForm } from "./_models/internal-request.form";
import { InternalRequestModel } from "./_models/internal-request.model";

@Component({
  selector: "cais-internal-request-form",
  templateUrl: "./internal-request-form.component.html",
  styleUrls: ["./internal-request-form.component.scss"],
})
export class InternalRequestFormComponent
  extends CrudForm<
    InternalRequestModel,
    InternalRequestForm,
    InternalRequestResolverData,
    InternalRequestService
  >
  implements OnInit
{
  constructor(service: InternalRequestService, public injector: Injector) {
    super(service, injector);
    this.setDisplayTitle("Заявка към бюлетин");
  }

  ngOnInit(): void {
    this.fullForm = new InternalRequestForm();
    this.fullForm.group.patchValue(this.dbData.element);

    if (this.isEdit()) {
      this.isForPreview =
        this.fullForm.reqStatusCode.value != IRStatusTypeEnum.New;
      this.fullForm.reqStatusCode.patchValue(null);
      this.fullForm.reqStatusCode.setValidators([Validators.required]);
    } else {
      var bulletinId = this.activatedRoute.snapshot.params["ID"];
      this.fullForm.bulletinId.patchValue(bulletinId);
    }

    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: InternalRequestModel) {
    return new InternalRequestModel(object);
  }

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };
}