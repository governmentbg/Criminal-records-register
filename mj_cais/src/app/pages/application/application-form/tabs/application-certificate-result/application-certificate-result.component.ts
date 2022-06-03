import { Component, Injector, Input, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { Observable } from "rxjs";
import { CrudForm } from "../../../../../@core/directives/crud-form.directive";
import { ApplicationCertificateService } from "./_data/application-certificate.service";
import { ApplicationCertificateResultForm } from "./_models/application-certificate-result.form";
import { ApplicationCertificateResultModel } from "./_models/application-certificate-result.model";
import * as fileSaver from "file-saver";
import { BaseNomenclatureModel } from "../../../../../@core/models/nomenclature/base-nomenclature.model";

@Component({
  selector: "cais-application-certificate-result",
  templateUrl: "./application-certificate-result.component.html",
  styleUrls: ["./application-certificate-result.component.scss"],
})
export class ApplicationCertificateResultComponent
  extends CrudForm<
    ApplicationCertificateResultModel,
    ApplicationCertificateResultForm,
    null,
    ApplicationCertificateService
  >
  implements OnInit
{
  @Input() model: ApplicationCertificateResultModel;
  @Input() users: BaseNomenclatureModel[];
  
  constructor(
    service: ApplicationCertificateService,
    public injector: Injector
  ) {
    super(service, injector);
  }

  ngOnInit(): void {
    this.fullForm = new ApplicationCertificateResultForm();
    this.fullForm.group.patchValue(this.model);
    this.formFinishedLoading.emit();
  }

  buildFormImpl(): FormGroup {
    return this.fullForm.group;
  }

  createInputObject(object: ApplicationCertificateResultModel) {
    return object;
  }

  generateCertificate() {
    if (!this.fullForm.group.valid) {
      this.fullForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");

      this.scrollToValidationError();
    } else {
      debugger;
      let model = this.fullForm.group.value;
      let id = "5aacead1-8495-47be-8c24-7e26f2833343"; // this.formObject.id
      this.service.saveSignerData(id, model).subscribe((response: any) => {
        this.service.downloadSertificate(id).subscribe((response: any) => {
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
  }
}
