import { Component, OnInit } from "@angular/core";
import { Observable, ReplaySubject } from "rxjs";
import { CustomToastrService } from "../../../../../@core/services/common/custom-toastr.service";
import { CustomFileUploader } from "../../../../../@core/utils/custom-file-uploader";
import { ApplicationDocumentForm } from "../../_models/application-document.form";

@Component({
  selector: "cais-application-certificate-document-result",
  templateUrl: "./application-certificate-document-result.component.html",
  styleUrls: ["./application-certificate-document-result.component.scss"],
})
export class ApplicationCertificateDocumentResultComponent implements OnInit {
  public appDocumentForm = new ApplicationDocumentForm();
  public uploader: CustomFileUploader;
  public hasDropZoneOver: boolean = false;
  protected validationMessage = "Грешка при валидациите!";

  constructor(public toastr: CustomToastrService) {
    this.initializeUploader();
  }

  ngOnInit(): void {}

  onSubmitAppDocument() {
    if (!this.appDocumentForm.group.valid) {
      this.toastr.showToast("danger", this.validationMessage);

      this.appDocumentForm.group.markAllAsTouched();
      return;
    }

    let model = this.appDocumentForm.group.value;
    // this.appService.saveDocument(this.appForm.id.value, model).subscribe(
    //   (res) => {
    //     this.toastr.showToast("success", "Успешно добавен документ");

    //   },

    //   (error) => {
    //     this.showErrorMessage(error, "Възникна грешка при запис на данните: ");
    //   }
    // );
  }

  onCloseAppDocumentDilog() {
    this.appDocumentForm = new ApplicationDocumentForm();
    //this.dialog.close();
  }

  private initializeUploader() {
    this.uploader = CustomFileUploader.createUploader();

    this.uploader.onWhenAddingFileFailed = (fileItem, response, status) => {
      this.toastr.showBodyToast(
        "danger",
        "Грешка при прикачване",
        fileItem.name
      );
    };

    this.uploader.onAfterAddingFile = (fileItem) => {
      if (fileItem) {
        let file = fileItem.file;
        const blob = new Blob([file.rawFile], { type: file.type });

        this.convertFile(blob).subscribe((base64) => {
          this.appDocumentForm.documentContent.setValue(base64);
          this.appDocumentForm.mimeType.setValue(file.type);
          this.appDocumentForm.name.setValue(file.name);
        });
      }
    };
  }

  private convertFile(file: Blob): Observable<string> {
    const result = new ReplaySubject<string>(1);
    const reader = new FileReader();
    reader.readAsBinaryString(file);
    reader.onload = (event) =>
      result.next(btoa(event.target.result.toString()));
    return result;
  }
}
