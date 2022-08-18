import { Component, OnInit, Injector, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { IgxDialogComponent } from "@infragistics/igniteui-angular";
import { NbDialogService } from "@nebular/theme";
import { GenderConstants } from "../../../../@core/constants/gender.constants";
import { CrudForm } from "../../../../@core/directives/crud-form.directive";
import { LoaderService } from "../../../../@core/services/common/loader.service";
import { NomenclatureService } from "../../../../@core/services/rest/nomenclature.service";
import { EcrisMessageStatusConstants } from "../../ecris-message-overivew/_models/ecris-message-status.constants";
import { EcrisNotPreviewComponent } from "../ecris-not-preview/ecris-not-preview.component";
import { EcrisReqPreviewComponent } from "../ecris-req-preview/ecris-req-preview.component";
import { EcrisResponsePreviewComponent } from "../ecris-response-preview/ecris-response-preview.component";
import { EcrisMessageService } from "../_data/ecris-message.service";
import { EcrisMessageForm } from "../_models/ecris-message.form";
import { EcrisMessageModel } from "../_models/ecris-message.model";
import { EcrisMsgTypeConstants } from "../_models/ecris-msg-type.constants";
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
  @ViewChild("cancelIdentificationDialog", { read: IgxDialogComponent })
  public cancelIdentificationDialog: IgxDialogComponent;
  public cancelIdentificationFormGroup: FormGroup;
  public model: EcrisMessageModel;
  private graoPersonId: string;
  public showBtnRecreate: boolean = false;

  constructor(
    service: EcrisMessageService,
    public injector: Injector,
    private nomenclatureService: NomenclatureService,
    private dialogService: NbDialogService,
    private formBuilder: FormBuilder,
    public loaderService: LoaderService
  ) {
    super(service, injector);
    this.backUrl = "pages/ecris/identification";
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

    let id = this.activatedRoute.snapshot.params["ID"];
    this.isForPreview = this.isForPreview || this.fullForm.ecrisMsgStatus.value != EcrisMessageStatusConstants.ForIdentification;
    this.service.get(id).subscribe((response) => {
      this.model = response;

      this.model.sex =
        GenderConstants.allData.find((g) => g.id == response.sex)?.name ?? null;

      this.nomenclatureService.getCountries().subscribe((resp) => {
        this.model.birthCountry = resp.find(
          (c) => c.id == response.birthCountry
        )?.name;
      });

      this.service.getNationalities(id).subscribe((response) => {
        let countries = response;

        let result = countries.map((val) => {
          return val.name;
        });
        if (this.model) {
          this.model.nationalities = result.join(", ");
        }
      });
    });

    this.cancelIdentificationFormGroup = this.formBuilder.group({
      reasonId: [{ value: "", disabled: false }, Validators.required],
    });

    this.showBtnRecreate = this.fullForm.ecrisMsgStatus.value == EcrisMessageStatusConstants.ReplyCreated &&
    this.fullForm.msgTypeId.value ==  EcrisMsgTypeConstants.EcrisRequest;

    this.formFinishedLoading.emit();
  }

  updateFunction = () => {
    this.submitFunction();
  };

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };

  identifyFunction = () => {
    if (this.graoPersonId) {
      let id = this.activatedRoute.snapshot.params["ID"];
      this.service.identify(id, this.graoPersonId).subscribe((res) => {
        this.reloadCurrentRoute();
      });
    } else {
      this.toastr.showBodyToast("danger", "Не е избрано лице", "");
    }
  };

  onOpenCancelIdentificationDialog = () => {
    this.cancelIdentificationDialog.open();
  };

  onCancelIdentification = () => {
    if (!this.cancelIdentificationFormGroup.valid) {
      this.cancelIdentificationFormGroup.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      this.scrollToValidationError();
      return;
    } 

    this.loaderService.show();

    this.service
      .cancelIdentification(
        this.fullForm.id.value,
        this.cancelIdentificationFormGroup.controls.reasonId.value
      )
      .subscribe(
        (response: any) => {
          this.loaderService.hide();

          this.router.navigate(["pages/ecris/requests/for-identification"]);
        },
        (error) => {
          this.loaderService.hide();

          var errorText = error.status + " " + error.statusText;
          this.toastr.showBodyToast("danger", "Възникна грешка:", errorText);
        }
      );
  };

  getDocument() {
    //ecrisMsg.msgType = "EcrisReqResp";
    //ecrisMsg.msgType = "EcrisNot";
    //ecrisMsg.msgType = "EcrisRequest";
    debugger;
    if (this.model.msgTypeId == "EcrisNot") {
      this.dialogService
        .open(EcrisNotPreviewComponent, {
          context: {
            ecrisId: this.model.id,
            ecrisType: this.model.msgTypeId,
          },
          closeOnBackdropClick: false,
        })
        .onClose.subscribe((x) => {});
    }
    if (this.model.msgTypeId == "EcrisRequest") {
      this.dialogService
        .open(EcrisReqPreviewComponent, {
          context: {
            ecrisId: this.model.id,
            ecrisType: this.model.msgTypeId,
          },
          closeOnBackdropClick: false,
        })
        .onClose.subscribe((x) => {});
    }
    if (this.model.msgTypeId == "EcrisReqResp") {
      this.dialogService
        .open(EcrisResponsePreviewComponent, {
          context: {
            ecrisId: this.model.id,
            ecrisType: this.model.msgTypeId,
          },
          closeOnBackdropClick: false,
        })
        .onClose.subscribe((x) => {});
    }
  }

  handleSelectedRow(event) {
    this.graoPersonId = event;
  }

  onRecreateMsg(){
    this.service.recreateMessage(this.fullForm.id.value)
    .subscribe(
      (response: any) => {
        this.loaderService.hide();
        this.router.navigate(["pages/ecris/identification"]);
      },
      (error) => {
        this.loaderService.hide();

        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast("danger", "Възникна грешка:", errorText);
      }
    );
  }
}
