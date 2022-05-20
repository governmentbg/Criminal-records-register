import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { ApplicationResolverData } from "./data/application.resolver";
import { ApplicationService } from "./data/application.service";
import { ApplicationForm } from "./models/application.form";
import { ApplicationModel } from "./models/application.model";

@Component({
  selector: "cais-application-form",
  templateUrl: "./application-form.component.html",
  styleUrls: ["./application-form.component.scss"],
})
export class ApplicationFormComponent
  extends CrudForm<
    ApplicationModel,
    ApplicationForm,
    ApplicationResolverData,
    ApplicationService
  >
  implements OnInit
{
  constructor(
    service: ApplicationService,
    public injector: Injector,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
    this.backUrl = "pages/applications";
    this.setDisplayTitle("Свидетелство");
  }

  ngOnInit(): void {
    this.fullForm = new ApplicationForm();
    this.fullForm.group.patchValue(this.dbData.element);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ApplicationModel) {
    return new ApplicationModel(object);
  }

  submitFunction = () => {
    this.validateAndSave(this.fullForm);
  };
}
