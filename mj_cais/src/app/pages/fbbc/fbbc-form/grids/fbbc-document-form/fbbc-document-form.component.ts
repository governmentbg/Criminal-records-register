import { Component, Input, OnInit, ViewChild } from "@angular/core";
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
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { CustomFileUploader } from "../../../../../@core/utils/custom-file-uploader";
import { FbbcService } from "../../data/fbbc.service";
import { FbbcDocumentForm } from "../../models/fbbc-document.form";
import { FbbcForm } from "../../models/fbbc.form";

@Component({
  selector: "cais-fbbc-document-form",
  templateUrl: "./fbbc-document-form.component.html",
  styleUrls: ["./fbbc-document-form.component.scss"],
})
export class FbbcDocumentFormComponent implements OnInit {
  @Input() fbbcForm: FbbcForm;
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

  public fbbcDocumentForm = new FbbcDocumentForm();
  public uploader: CustomFileUploader;
  public hasDropZoneOver: boolean = false;

  protected validationMessage = "Грешка при валидациите!";

  constructor(
    public toastr: CustomToastrService,
    public fbbcService: FbbcService,
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

  onSubmitFbbcDocument() {
    if (!this.fbbcDocumentForm.group.valid) {
      this.toastr.showToast("danger", this.validationMessage);

      this.fbbcDocumentForm.group.markAllAsTouched();
      return;
    }

    let model = this.fbbcDocumentForm.group.value;
    this.fbbcService.saveDocument(this.fbbcForm.id.value, model).subscribe(
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
    if (this.fbbcDocumentForm.docTypeId.value) {
      let docTypeName = (this.dbData.documentTypes as any).find(
        (x) => x.id === this.fbbcDocumentForm.docTypeId.value
      )?.name;
      this.fbbcDocumentForm.docTypeName.patchValue(docTypeName);
    }

    this.documentsGrid.addRow(this.fbbcDocumentForm.group.value);

    this.onCloseFbbcDocumentDilog();
  }

  onCloseFbbcDocumentDilog() {
    this.fbbcDocumentForm = new FbbcDocumentForm();
    this.dialog.close();
  }

  onRmoveDocument(item: FileItem) {
    this.fbbcDocumentForm.documentContent.setValue(null);
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
          this.fbbcService
            .deleteDocument(this.fbbcForm.id.value, documentId)
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
    this.fbbcService
      .downloadDocument(this.fbbcForm.id.value, id)
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
          this.fbbcDocumentForm.documentContent.setValue(base64);
          this.fbbcDocumentForm.mimeType.setValue(file.type);
          this.fbbcDocumentForm.name.setValue(file.name);
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
