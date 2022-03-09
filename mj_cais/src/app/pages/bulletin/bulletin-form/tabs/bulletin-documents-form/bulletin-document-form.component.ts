import { Component, Input, ViewChild } from "@angular/core";
import {
  IgxDialogComponent,
  IgxGridComponent,
} from "@infragistics/igniteui-angular";
import { NbDialogService } from "@nebular/theme";
import * as fileSaver from "file-saver";
import { FileItem } from "ng2-file-upload";
import { Observable, ReplaySubject } from "rxjs";
import { ConfirmDialogComponent } from "../../../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonConstants } from "../../../../../@core/constants/common.constants";
import { CustomToastrService } from "../../../../../@core/services/common/custom-toastr.service";
import { CustomFileUploader } from "../../../../../@core/utils/custom-file-uploader";
import { BulletinService } from "../../data/bulletin.service";
import { BulletinDocumentForm } from "../../models/bulletin-document.form";
import { BulletinForm } from "../../models/bulletin.form";

@Component({
  selector: "cais-bulletin-document-form",
  templateUrl: "./bulletin-document-form.component.html",
  styleUrls: ["./bulletin-document-form.component.scss"],
})
export class BulletinDocumentFormComponent {
  @Input() bulletinForm: BulletinForm;
  @Input() documents: any;
  @Input() documentTypes: any;
  @Input() isForPreview: boolean;

  @ViewChild("documentsGrid", {
    read: IgxGridComponent,
  })
  public documentsGrid: IgxGridComponent;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  public bulletinDocumentForm = new BulletinDocumentForm();
  public uploader: CustomFileUploader;
  protected validationMessage = "Грешка при валидациите!";

  constructor(
    public toastr: CustomToastrService,
    public bulletinService: BulletinService,
    private dialogService: NbDialogService
  ) {
    this.initializeUploader();
  }

  onOpenDialog() {
    this.initializeUploader();
  }

  onSubmitBulletineDocument() {
    debugger;
    if (!this.bulletinDocumentForm.group.valid) {
      this.toastr.showToast("danger", this.validationMessage);

      this.bulletinDocumentForm.group.markAllAsTouched();
      return;
    }

    let model = this.bulletinDocumentForm.group.value;
    this.bulletinService
      .saveDocument(this.bulletinForm.id.value, model)
      .subscribe(
        (res) => {
          this.toastr.showToast("success", "Успешно добавен документ");
          this.onAddDocumentRow();
        },

        (error) => {
          this.showErrorMessage(
            error,
            "Възникна грешка при запис на данните: "
          );
        }
      );
  }

  onAddDocumentRow() {
    if (this.bulletinDocumentForm.docTypeId.value) {
      let docTypeName = (this.documentTypes as any).find(
        (x) => x.id === this.bulletinDocumentForm.docTypeId.value
      )?.name;
      this.bulletinDocumentForm.docTypeName.patchValue(docTypeName);
    }

    this.documentsGrid.addRow(this.bulletinDocumentForm.group.value);

    this.onCloseBulletinDocumentDilog();
  }

  onCloseBulletinDocumentDilog() {
    this.bulletinDocumentForm = new BulletinDocumentForm();
    this.dialog.close();
  }

  onRmoveDocument(item: FileItem) {
    this.bulletinDocumentForm.documentContent.setValue(null);
    item.remove();
  }

  openDeleteConfirmationDialog(documentId: string) {
    this.dialogService
      .open(ConfirmDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe((result) => {
        if (result) {
          debugger;
          this.bulletinService
            .deleteDocument(this.bulletinForm.id.value, documentId)
            .subscribe(
              (res) => {
                this.toastr.showToast("success", "Успешно изтрит документ");

                this.documentsGrid.deleteRow(documentId);
                this.documentsGrid.data = this.documentsGrid.data.filter(
                  (d) => d.id != documentId
                );
              },
              (error) => {
                this.showErrorMessage(
                  error,
                  "Възникна грешка по време на изтриване на файл"
                );
              }
            );
        }
      });
  }

  download(id: string) {
    this.bulletinService.downloadDocument(this.bulletinForm.id.value,id).subscribe((response: any) => {
      debugger;
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
        this.showErrorMessage(error, "Грешка при изтегляне на файла: ");
      };
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
      debugger;
      if (fileItem) {
        let file = fileItem.file;
        const blob = new Blob([file.rawFile], { type: file.type });

        this.convertFile(blob).subscribe((base64) => {
          this.bulletinDocumentForm.documentContent.setValue(base64);
          this.bulletinDocumentForm.mimeType.setValue(file.type);
          this.bulletinDocumentForm.name.setValue(file.name);
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

  private showErrorMessage(error, message: string) {
    var errorText = error.status + " " + error.statusText;
    this.toastr.showBodyToast("danger", message, errorText);
  }
}
