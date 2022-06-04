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
  public showBulletinsCheck: boolean;
  public bulletinsCheckData: BulletinCheckGridModel[] = [];

  constructor(
    service: ApplicationCertificateService,
    public injector: Injector
  ) {
    super(service, injector);
  }

  ngOnInit(): void {
    this.fullForm = new ApplicationCertificateResultForm();
    this.fullForm.group.patchValue(this.model);
    if(this.model){
      this.showCertContentReady = this.model.statusCode == CertificateStatuTypeEnum.CertificateContentReady;
      this.showBulletinsCheck = this.model.statusCode == CertificateStatuTypeEnum.BulletinsCheck;
  
      if(this.showBulletinsCheck){
        this.service.getBulletinsCheck(this.fullForm.id.value)
        .subscribe(response=>{
          this.bulletinsCheckData = response;
        });
      }
    }
   
    if(this.isForPreview){
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

  sendRequestToJudge(){
    debugger;
    var selectedItesm =  this.bulletinsCheckGrid.selectedRows
  }

}
