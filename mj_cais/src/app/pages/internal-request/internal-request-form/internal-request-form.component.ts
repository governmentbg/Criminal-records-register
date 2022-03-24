import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup, Validators } from "@angular/forms";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
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
    this.backUrl = "pages/internal-requests";
    this.setDisplayTitle("Заявка към бюлетин");
  }

  public showStatus: boolean = false;

  ngOnInit(): void {
    this.fullForm = new InternalRequestForm();
    this.fullForm.group.patchValue(this.dbData.element);

    if (this.isEdit()) {
      let isPeviewOfNewReq =
        this.isEdit() &&
        this.isForPreview &&
        this.fullForm.reqStatusCode.value != null;

      let isEdit = this.isEdit() && !this.isForPreview;
      this.showStatus = isPeviewOfNewReq || isEdit;

      this.fullForm.reqStatusCode.setValidators([Validators.required]);
    } else {
      var bulletinId = this.activatedRoute.snapshot.params["ID"];
      this.fullForm.bulletinId.patchValue(bulletinId);
      this.backUrl = `pages/bulletins-for-rehabilitation`;
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
