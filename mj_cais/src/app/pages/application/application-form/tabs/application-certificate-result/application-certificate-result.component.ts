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
import { UserInfoService } from "../../../../../@core/services/common/user-info.service";
import { NbDialogService } from "@nebular/theme";
import { CommonConstants } from "../../../../../@core/constants/common.constants";
import { ApplicationCertificateDocumentResultComponent } from "../application-certificate-document-result/application-certificate-document-result.component";
import { ApplicationCertificateDocumentModel } from "./_models/application-certificate-document.model";

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
  @Input() applicationCode: string;

  @ViewChild("bulletinsCheckGrid", {
    read: IgxGridComponent,
  })
  public bulletinsCheckGrid: IgxGridComponent;

  public CertificateStatuTypeEnum = CertificateStatuTypeEnum;
  public bulletinsCheckData: BulletinCheckGridModel[] = [];
  public certificateStatus: string;

  constructor(
    service: ApplicationCertificateService,
    public injector: Injector,
    public dateFormatService: DateFormatService,
    private userInfoService: UserInfoService,
    private dialogService: NbDialogService
  ) {
    super(service, injector);
  }

  ngOnInit(): void {
    this.fullForm = new ApplicationCertificateResultForm();
    this.fullForm.group.patchValue(new ApplicationCertificateResultModel(this.model));
    debugger;
    if (this.model) {
      if (
        this.model.statusCode ==
          CertificateStatuTypeEnum.CertificatePaperPrint ||
        this.model.statusCode == CertificateStatuTypeEnum.Delivered
      ) {
        this.fullForm.group.disable();
      }

      if (
        this.model.statusCode == CertificateStatuTypeEnum.BulletinsCheck ||
        this.model.statusCode == CertificateStatuTypeEnum.BulletinsSelection
      ) {
        this.service
          .getBulletinsCheck(
            this.fullForm.id.value,
            this.model.statusCode == CertificateStatuTypeEnum.BulletinsSelection
          )
          .subscribe((response) => {
            this.bulletinsCheckData = response;
          });
      }
      if (this.model.firstSignerId == null) {
        this.model.firstSignerId = this.userInfoService.userId;
        this.fullForm.firstSignerId.setValue(this.userInfoService.userId);
      }
    }

    if (this.isForPreview) {
      this.fullForm.group.disable();
    }
    this.formFinishedLoading.emit();
  }

  updateStatus() {
    this.service.updateStatus(this.fullForm.id.value).subscribe((x) => {
      this.toastr.showBodyToast(
        "success",
        "Успешно връчване(смяна на статус)",
        ""
      );
      this.reloadCurrentRoute();
      debugger;
    }),
      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast(
          "danger",
          "Грешка при смяна на статуса:",
          errorText
        );
      };
  }

  upload() {
    this.dialogService
      .open(
        ApplicationCertificateDocumentResultComponent,
        CommonConstants.defaultDialogConfig
      )
      .onClose.subscribe((x) => {
        if (x) {
          debugger;
          var object = new ApplicationCertificateDocumentModel();
          object.documentContent = x.documentContent;
          this.service
            .uploadSignedCertificate(this.fullForm.id.value, object)
            .subscribe((y) => {
              debugger;
              let res = y;
              this.toastr.showBodyToast(
                "success",
                "Успешно качване на файл",
                ""
              );
            },   (error) => {
              var errorText = error.status + " " + error.statusText;
              this.toastr.showBodyToast(
                "danger",
                "Грешка при качване на файла:",
                errorText
              );
            });
        }
      }),
      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast(
          "danger",
          "Грешка при качване на файла:",
          errorText
        );
      };
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
        this.downloadSertificate(id);
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
      .downloadSertificateContent(this.model.id,this.applicationCode)
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
    this.service
      .sendBulletinsForSelection(this.model.id)
      .subscribe((response: any) => {
        this.model.statusCode = CertificateStatuTypeEnum.BulletinsSelection;
        this.router.navigate(["pages/applications/for-check"]);
      }),
      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast("danger", "Възникна грешка:", errorText);
      };
  }

  sendBulltinsForRehabilitation() {
    var selectedItesm = this.bulletinsCheckGrid.selectedRows;
    this.service
      .sendBulletinsForRehabilitation(this.model.id, selectedItesm)
      .subscribe((response: any) => {
        this.model.statusCode =
          CertificateStatuTypeEnum.BulletinsRehabilitation;
        this.router.navigate(["pages/applications/for-check"]);
      }),
      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast("danger", "Възникна грешка:", errorText);
      };
  }

  generateCertificateByJudge() {
    if (!this.fullForm.group.valid) {
      this.fullForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");

      this.scrollToValidationError();
      return;
    }

    var selectedItesm = this.bulletinsCheckGrid.selectedRows;
    let model = this.fullForm.group.value as ApplicationCertificateResultModel;
    model.selectedBulletinsIds = selectedItesm;
    let id = this.fullForm.id.value;

    this.service.saveSignerDataByJudge(id, model).subscribe((response: any) => {
      this.downloadSertificate(id);
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

  downloadSertificate(id) {
    this.service.downloadSertificate(id).subscribe((response: any) => {
      this.model.statusCode = CertificateStatuTypeEnum.CertificatePaperPrint;
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
}
