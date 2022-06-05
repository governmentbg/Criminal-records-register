import { Component, Injector, Input, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { CrudForm } from "../../../../../@core/directives/crud-form.directive";
import { ApplicationCertificateService } from "./_data/application-certificate.service";
import { ApplicationCertificateResultForm } from "./_models/application-certificate-result.form";
import { ApplicationCertificateResultModel } from "./_models/application-certificate-result.model";
import * as fileSaver from "file-saver";
import { BaseNomenclatureModel } from "../../../../../@core/models/nomenclature/base-nomenclature.model";
import { CertificateStatuTypeEnum } from "./_models/certificate-status-type.enum";
import { BulletinCheckGridModel } from "./_models/bulletin-check-grid.model";
import { IgxGridComponent } from "@infragistics/igniteui-angular";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";

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

  @ViewChild("bulletinsCheckGrid", {
    read: IgxGridComponent,
  })
  public bulletinsCheckGrid: IgxGridComponent;

  public CertificateStatuTypeEnum = CertificateStatuTypeEnum;
  public showCertContentReady: boolean;
  public bulletinsCheckData: BulletinCheckGridModel[] = [];
  public certificateStatus: string;

  constructor(
    service: ApplicationCertificateService,
    public injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super(service, injector);
  }

  ngOnInit(): void {
    this.fullForm = new ApplicationCertificateResultForm();
    this.fullForm.group.patchValue(this.model);
    this.model.statusCode = this.model.statusCode;
    if (this.model) {
      if (
        this.model.statusCode ==
        CertificateStatuTypeEnum.CertificateContentReady
      ) {
        this.showCertContentReady = true;
      } else if (
        this.model.statusCode ==
          CertificateStatuTypeEnum.CertificatePaperPrint ||
          this.model.statusCode == CertificateStatuTypeEnum.Delivered
      ) {
        this.showCertContentReady = true;
        this.fullForm.group.disable();
      }

      if (this.model.statusCode == CertificateStatuTypeEnum.BulletinsCheck
        ||this.model.statusCode == CertificateStatuTypeEnum.BulletinsSelection) {
        this.service
          .getBulletinsCheck(this.fullForm.id.value)
          .subscribe((response) => {
            this.bulletinsCheckData = response;
          });
      }
    }

    if (this.isForPreview) {
      this.fullForm.group.disable();
    }
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
      let model = this.fullForm.group.value;
      let id = this.fullForm.id.value;
      this.service.saveSignerData(id, model).subscribe((response: any) => {
        this.service.downloadSertificate(id).subscribe((response: any) => {
          this.model.statusCode =
            CertificateStatuTypeEnum.CertificatePaperPrint;
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

  printCertificate() {
    this.service
      .downloadSertificate(this.model.statusCode)
      .subscribe((response: any) => {
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
          "Грешка при печат на свидетелство:",
          errorText
        );
      };
  }

  sendBulltinsForSelection() {
    debugger;
    var selectedItesm = this.bulletinsCheckGrid.selectedRows;
    this.service
      .sendBulletinsForSelection(this.model.id, selectedItesm)
      .subscribe((response: any) => {
        this.model.statusCode =
            CertificateStatuTypeEnum.BulletinsSelection;
      }),
      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast("danger", "Възникна грешка:", errorText);
      };
  }
}
