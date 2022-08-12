import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup, Validators } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { NgxSpinnerService } from "ngx-spinner";
import { SelectPidDialogComponent } from "../../../@core/components/dialogs/select-pid-dialog/select-pid-dialog.component";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { InternalRequestResolverData } from "./_data/internal-request.resolver";
import { InternalRequestService } from "./_data/internal-request.service";
import { InternalRequestStatusCodeConstants } from "./_models/internal-request-status-code.constants";
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
  constructor(
    service: InternalRequestService,
    public injector: Injector,
    private dialogService: NbDialogService,
    private loaderService: NgxSpinnerService
  ) {
    super(service, injector);
  }

  public title: string = "Добавяне на заявка";
  public InternalRequestStatusCodeConstants =
    InternalRequestStatusCodeConstants;
  public requestStatusCode;
  public showReplayBtn: boolean = false;

  ngOnInit(): void {
    this.fullForm = new InternalRequestForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.fullForm.regNumberDisplay.patchValue(this.fullForm.regNumber.value);
    this.requestStatusCode = this.fullForm.reqStatusCode.value;

    // this is replay
    if (this.requestStatusCode == InternalRequestStatusCodeConstants.Sent) {
      this.fullForm.group.disable();
      this.fullForm.responseDescr.enable();
      this.fullForm.responseDescr.setValidators(Validators.required);

      if (this.isEdit()) {
        this.showReplayBtn = true;
        this.title = "Отговор на заявка";
      }

      // check before change prop
      if (this.isForPreview) {
        this.showReplayBtn = false;
        this.title = "Преглед на заявка";
      }

      this.formFinishedLoading.emit();

      this.isForPreview = true;
    } else {
      if (this.isEdit()) {
        this.title = "Редакция на заявка";
      }

      if (this.isForPreview) {
        this.title = "Преглед на заявка";
      }
      this.formFinishedLoading.emit();
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

  replay(accepted) {
    debugger;
    if (!this.fullForm.responseDescr.valid) {
      this.fullForm.responseDescr.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      this.scrollToValidationError();
      return;
    }

    let replayObj = {
      accepted: accepted,
      responseDescr: this.fullForm.responseDescr.value,
    };

    this.service.replay(this.fullForm.id.value, replayObj).subscribe(
      (res) => {
        this.loaderService.hide();

        this.toastr.showToast("success", "Успешно изпратена заявка");
        this.router.navigate(["pages/internal-requests"]);
      },
      (error) => {
        this.onServiceError(error);
      }
    );
  }

  public openPidDialog = () => {
    this.dialogService
      .open(SelectPidDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe(this.onSelectPid);
  };

  public onSelectPid = (item) => {
    if (item) {
      this.fullForm.pPersIdId.setValue(item.id, item.pid);
    }
  };

  public send() {
    this.changeStatus(
      this.fullForm.id.value,
      InternalRequestStatusCodeConstants.Sent
    );
  }

  private changeStatus(id: string, status: string) {
    this.service.changeStatus(id, status).subscribe(
      (res) => {
        this.loaderService.hide();

        this.toastr.showToast("success", "Успешно изпратена заявка");
        this.router.navigate(["pages/internal-requests"]);
      },
      (error) => {
        this.onServiceError(error);
      }
    );
  }
}
