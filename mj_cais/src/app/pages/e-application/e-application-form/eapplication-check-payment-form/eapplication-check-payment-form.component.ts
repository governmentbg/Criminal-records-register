import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { PersonContextEnum } from "../../../../@core/components/forms/person-form/_models/person-context-enum";
import { CrudForm } from "../../../../@core/directives/crud-form.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { EApplicationResolverData } from "../_data/eapplication.resolver";
import { EApplicationService } from "../_data/eapplication.service";
import { EApplicationForm } from "../_models/eapplication.form";
import { EApplicationModel } from "../_models/eapplication.model";

@Component({
  selector: "cais-eapplication-check-payment-form",
  templateUrl: "./eapplication-check-payment-form.component.html",
  styleUrls: ["./eapplication-check-payment-form.component.scss"],
})
export class EapplicationCheckPaymentFormComponent
  extends CrudForm<
    EApplicationModel,
    EApplicationForm,
    EApplicationResolverData,
    EApplicationService
  >
  implements OnInit
{
  public PersonContextEnum = PersonContextEnum;
  displayTitle: string = "Преглед";

  constructor(
    service: EApplicationService,
    public injector: Injector,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
  }

  ngOnInit(): void {
    this.fullForm = new EApplicationForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: EApplicationModel) {
    return object;
  }

  protected validateAndSave(form: any) {
    console.log(form.group);
    if (!form.group.valid) {
      form.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");

      this.scrollToValidationError();
    } else {
      this.formObject = form.group.getRawValue();
      this.saveAndNavigate();
    }
  }
}
