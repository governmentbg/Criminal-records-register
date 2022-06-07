import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NbDialogService } from "@nebular/theme";
import { Observable } from "rxjs";
import { PersonContextEnum } from "../../../@core/components/forms/person-form/_models/person-context-enum";
import { CrudForm } from "../../../@core/directives/crud-form.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { ApplicationTypeStatusConstants } from "../../application/application-overview/_models/application-type-status.constants";
import { ApplicationReportResolverData } from "./_data/application-report.resolver";
import { ApplicationReportService } from "./_data/application-report.service";
import { ApplicationReportForm } from "./_models/application-report.form";
import { ApplicationReportModel } from "./_models/application-report.model";
import * as fileSaver from "file-saver";

@Component({
  selector: "cais-application-report-form",
  templateUrl: "./application-report-form.component.html",
  styleUrls: ["./application-report-form.component.scss"],
})
export class ApplicationReportFormComponent
  extends CrudForm<
    ApplicationReportModel,
    ApplicationReportForm,
    ApplicationReportResolverData,
    ApplicationReportService
  >
  implements OnInit
{
  public PersonContextEnum = PersonContextEnum;
  public applicationStatus: string;
  public ApplicationTypeStatusConstants = ApplicationTypeStatusConstants;
  private isFinalEdit: boolean;
  public reportStatus: string;
  constructor(
    service: ApplicationReportService,
    public injector: Injector,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
    this.backUrl = "pages/applications";
     this.setDisplayTitle("Справка за съдимост");
  }

  ngOnInit(): void {
    debugger;
    this.fullForm = new ApplicationReportForm();
    this.fullForm.group.patchValue(this.dbData.element);
    if (!this.isEdit()) {
      this.fullForm.person.nationalities.selectedForeignKeys.patchValue([
        "CO-00-100-BGR",
      ]);
      this.fullForm.person.nationalities.isChanged.patchValue(true);
    }

    this.applicationStatus = this.fullForm.statusCode.value;
    if (
      this.fullForm.statusCode.value ==
      ApplicationTypeStatusConstants.ApprovedApplication
    ) {
      this.fullForm.group.disable();
    }
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ApplicationReportModel) {
    return object;
  }

  submitFunction = () => {
    debugger;
    // this.fullForm.applicationTypeId.setValue('6'); //Взима се от контекста
    // this.fullForm.csAuthorityId.setValue('562');  //Взима се от контекста
    this.isFinalEdit = false;
    this.validateAndSave(this.fullForm);
  };

  generateReport() {
    let id = this.fullForm.id.value;
    this.downloadSertificate(id)

    // this.service.generateReport(id).subscribe((response: any) => {
    //   var reportId = response;
    //   this.downloadSertificate(reportId);
    // }),
    //   (error) => {
    //     var errorText = error.status + " " + error.statusText;
    //     this.toastr.showBodyToast(
    //       "danger",
    //       "Грешка при генериране на свидетелство:",
    //       errorText
    //     );
    //   };
  }

  downloadSertificate(id) {
    debugger;
    this.service.downloadSertificate(id).subscribe((response: any) => {
      debugger;
      this.fullForm.group.disable();
      let blob = new Blob([response.body]);
      window.URL.createObjectURL(blob);

      let header = response.headers.get("Content-Disposition");
      let filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;

      let fileName = "download";

      var matches = filenameRegex.exec(header);
      if (matches != null && matches[1]) {
        fileName = matches[1].replace(/['"]/g, "");
      }

      fileSaver.saveAs(blob, fileName);
    }),
      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast(
          "danger",
          "Грешка при генериране на свидетелство:",
          errorText
        );
      };
  }

  protected saveAndNavigate() {
    let model = this.formObject;
    let submitAction: Observable<ApplicationReportModel>;
    if (this.isEdit()) {
      submitAction = this.service.update(this.formObject.id, model);
    } else {
      submitAction = this.service.save(model);
    }

    submitAction.subscribe({
      next: (data) => {
        debugger;
        this.toastr.showToast("success", this.successMessage);

        setTimeout(() => {
          this.onSubmitSuccess(data);
        }, this.navigateTimeout);
      },
      error: (errorResponse) => {
        let title = this.dangerMessage;
        let errorText = errorResponse.status + " " + errorResponse.statusText;

        if (errorResponse.error && errorResponse.error.customMessage) {
          title = errorResponse.error.customMessage;
          errorText = "";
        }

        this.toastr.showBodyToast("danger", title, errorText);

        // if has server side validation errors add them to the form control
        if (errorResponse.error.errors) {
          Object.keys(errorResponse.error.errors).forEach((prop) => {
            var propName = prop[0].toLocaleLowerCase() + prop.slice(1);
            const formControl = this.fullForm.group.get(propName);
            if (formControl) {
              // activate the error message
              formControl.setErrors({
                serverErrors: errorResponse.error.errors[prop],
              });
            }
          });
        }

        this.scrollToValidationError();
      },
    });
  }
}
