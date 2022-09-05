import { Component, Input, OnInit, ViewChild } from "@angular/core";
import {
  IgxDialogComponent,
  IgxGridComponent,
} from "@infragistics/igniteui-angular";
import { NbDialogService } from "@nebular/theme";
import { FileItem } from "ng2-file-upload";
import { Observable, ReplaySubject } from "rxjs";
import { ConfirmDialogComponent } from "../../../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonConstants } from "../../../../../@core/constants/common.constants";
import { CustomToastrService } from "../../../../../@core/services/common/custom-toastr.service";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { CustomFileUploader } from "../../../../../@core/utils/custom-file-uploader";
import { ApplicationService } from "../../_data/application.service";
import { ApplicationDocumentForm } from "../../_models/application-document.form";
import { ApplicationForm } from "../../_models/application.form";
import * as fileSaver from "file-saver";

@Component({
  selector: "cais-application-document-form",
  templateUrl: "./application-document-form.component.html",
  styleUrls: ["./application-document-form.component.scss"],
})
export class ApplicationDocumentFormComponent implements OnInit {
  @Input() appForm: ApplicationForm;
  @Input() documents: any;
  @Input() dbData: any;
  @Input() isForPreview: boolean;
  @Input() showAddDeleteButton: boolean;

  @ViewChild("documentsGrid", {
    read: IgxGridComponent,
  })
  public documentsGrid: IgxGridComponent;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  public appDocumentForm = new ApplicationDocumentForm();
  public uploader: CustomFileUploader;
  public hasDropZoneOver: boolean = false;

  protected validationMessage = "Грешка при валидациите!";

  constructor(
    public toastr: CustomToastrService,
    public appService: ApplicationService,
    private dialogService: NbDialogService,
    private dateFormatService: DateFormatService
  ) {
    this.initializeUploader();
  }

  ngOnInit(): void {}

  public fileOverAnother(e: any): void {
    this.hasDropZoneOver = e;
  }

  onOpenDialog() {
    this.initializeUploader();
  }

  onSubmitAppDocument() {
    if (!this.appDocumentForm.group.valid) {
      this.toastr.showToast("danger", this.validationMessage);

      this.appDocumentForm.group.markAllAsTouched();
      return;
    }

    let model = this.appDocumentForm.group.value;
    this.appService.saveDocument(this.appForm.id.value, model).subscribe(
      (res) => {
        this.toastr.showToast("success", "Успешно добавен документ");
        this.onAddDocumentRow();
      },

      (error) => {
        this.showErrorMessage(error, "Възникна грешка при запис на данните: ");
      }
    );
  }

  onAddDocumentRow() {
    if (this.appDocumentForm.docTypeId.value) {
      let docTypeName = (this.dbData.documentTypes as any).find(
        (x) => x.id === this.appDocumentForm.docTypeId.value
      )?.name;
      this.appDocumentForm.docTypeName.patchValue(docTypeName);
    }

    this.documentsGrid.addRow(this.appDocumentForm.group.value);

    this.onCloseAppDocumentDilog();
  }

  onCloseAppDocumentDilog() {
    this.appDocumentForm = new ApplicationDocumentForm();
    this.dialog.close();
  }

  onRmoveDocument(item: FileItem) {
    this.appDocumentForm.documentContent.setValue(null);
    item.remove();
  }

  openDeleteConfirmationDialog(documentId: string) {
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          color: "danger",
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {
        if (result) {
          this.appService
            .deleteDocument(this.appForm.id.value, documentId)
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
    this.appService
      .downloadDocument(this.appForm.id.value, id)
      .subscribe((response: any) => {
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

  private showErrorMessage(error, message: string) {
    var errorText = error.status + " " + error.statusText;
    this.toastr.showBodyToast("danger", message, errorText);
  }
}
