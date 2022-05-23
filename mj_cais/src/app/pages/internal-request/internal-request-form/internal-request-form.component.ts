import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup, Validators } from "@angular/forms";
import { EActions } from "@tl/tl-common";
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
  private readonly PREVIEW_ACTION = "preview";
  
  private readonly BULLETIN_OVERVIEW_URL =
    "/pages/bulletins-for-rehabilitation";
    private readonly INTERNAL_REQUEST_OVERVIEW_URL =
    "/pages/internal-requests";

  public isCreate: boolean;

  constructor(service: InternalRequestService, public injector: Injector) {
    super(service, injector);
    this.setDisplayTitle("заявка към бюлетин");
  }

  ngOnInit(): void {
    this.fullForm = new InternalRequestForm();
    this.fullForm.group.patchValue(this.dbData.element);
    let currentUrl = this.router.url.toLocaleLowerCase();
    let index = currentUrl.indexOf(this.PREVIEW_ACTION);
    if (index > -1) {
      this.isForPreview = true;
    }

    if (this.isEdit()) {
      this.fullForm.reqStatusCode.patchValue(null);
      this.fullForm.reqStatusCode.setValidators([Validators.required]);
    } else {
      var bulletinId = this.activatedRoute.snapshot.params["ID"];
      this.fullForm.bulletinId.patchValue(bulletinId);
    }
    
    this.isCreate = this.currentAction != EActions.EDIT;
    this.formFinishedLoading.emit();
  }

  //override submit function
  onSubmitSuccess(data: any) {
    if(!this.isEdit()){
      this.router.navigateByUrl(this.BULLETIN_OVERVIEW_URL);
    }else{
      this.router.navigateByUrl(this.INTERNAL_REQUEST_OVERVIEW_URL);
    }
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
