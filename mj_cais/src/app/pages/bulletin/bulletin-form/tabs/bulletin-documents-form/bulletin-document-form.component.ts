import { Component, Input, ViewChild } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import {
  IgxDialogComponent,
  IgxGridComponent,
} from "@infragistics/igniteui-angular";
import { NbDialogService } from "@nebular/theme";
import { EActions } from "@tl/tl-common";
import * as fileSaver from "file-saver";
import { FileItem } from "ng2-file-upload";
import { Observable, ReplaySubject } from "rxjs";
import { ConfirmDialogComponent } from "../../../../../@core/components/dialogs/confirm-dialog-component/confirm-dialog-component.component";
import { CommonErrorHandlerService } from "../../../../../@core/services/common/common-error-handler.service";
import { CustomToastrService } from "../../../../../@core/services/common/custom-toastr.service";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { CustomFileUploader } from "../../../../../@core/utils/custom-file-uploader";
import { BulletinService } from "../../_data/bulletin.service";
import { BulletinForm } from "../../_models/bulletin.form";
import { BulletinDocumentForm } from "./_models/bulletin-document.form";
import { BulletinDocumentModel } from "./_models/bulletin-document.model";

@Component({
  selector: "cais-bulletin-document-form",
  templateUrl: "./bulletin-document-form.component.html",
  styleUrls: ["./bulletin-document-form.component.scss"],
})
export class BulletinDocumentFormComponent {
  @Input() bulletinForm: BulletinForm;
  @Input() dbData: any;
  @Input() isForPreview: boolean;
  @Input() showAddDeleteButton: boolean;

  @ViewChild("documentsGrid", {
    read: IgxGridComponent,
  })
  public documentsGrid: IgxGridComponent;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  public bulletinDocumentForm = new BulletinDocumentForm();
  public uploader: CustomFileUploader;
  public hasDropZoneOver: boolean = false;
  public documents: BulletinDocumentModel[];
  public isLoading = false;

  protected validationMessage = "Грешка при валидациите!";

  constructor(
    public toastr: CustomToastrService,
    public bulletinService: BulletinService,
    private dialogService: NbDialogService,
    public dateFormatService: DateFormatService,
    private activatedRoute: ActivatedRoute,
    private errorService: CommonErrorHandlerService
  ) {
    this.initializeUploader();
  }

  ngOnInit(): void {
    if (
      this.activatedRoute.snapshot.routeConfig.path.toUpperCase() ===
      EActions.CREATE
    ) {
      this.documents = [];
      return;
    }

    let bulletinId = this.activatedRoute.snapshot.params["ID"];
    this.isLoading = true;
    this.bulletinService.getDocuments(bulletinId).subscribe({
      next: (response) => {
        this.isLoading = false;
        this.documents = response;
      },
      error: (errorResponse) => {
        this.isLoading = false;
        this.errorService.errorHandler(errorResponse);
      },
    });
  }

  public fileOverAnother(e: any): void {
    this.hasDropZoneOver = e;
  }

  onOpenDialog() {
    this.bulletinDocumentForm.docTypeId.disable();
    this.initializeUploader();
  }

  onSubmitBulletineDocument() {
    if (!this.bulletinDocumentForm.group.valid) {
      this.toastr.showToast("danger", this.validationMessage);

      this.bulletinDocumentForm.group.markAllAsTouched();
      return;
    }

    this.bulletinDocumentForm.createdOn.patchValue(new Date());
    let model = this.bulletinDocumentForm.group.value;
    model.docTypeId = this.bulletinDocumentForm.docTypeId.value;

    this.isLoading = true;
    this.dialog.close();
    this.bulletinService
      .saveDocument(this.bulletinForm.id.value, model)
      .subscribe({
        next: (response) => {
          this.isLoading = false;
          this.toastr.showToast("success", "Успешно добавен документ");
          this.onAddDocumentRow();
        },
        error: (errorResponse) => {
          this.isLoading = false;
          this.errorService.errorHandler(errorResponse);
        },
      });
  }

  onAddDocumentRow() {
    if (this.bulletinDocumentForm.docTypeId.value) {
      let docTypeName = (this.dbData.documentTypes as any).find(
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
      .open(ConfirmDialogComponent, {
        context: {
          color: "danger",
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((result) => {       
        if (result) {
          this.isLoading = true;
          this.bulletinService
            .deleteDocument(this.bulletinForm.id.value, documentId)
            .subscribe({
              next: (response) => {
                this.isLoading = false;
                this.toastr.showToast("success", "Успешно изтрит документ");
                this.documentsGrid.deleteRow(documentId);
                this.documentsGrid.data = this.documentsGrid.data.filter(
                  (d) => d.id != documentId
                );
              },
              error: (errorResponse) => {
                this.isLoading = false;
                this.errorService.errorHandler(errorResponse);
              },
            });
        }
      });
  }

  download(id: string) {
    this.isLoading = true;
    this.bulletinService
      .downloadDocument(this.bulletinForm.id.value, id)
      .subscribe({
        next: (response) => {
          this.isLoading = false;
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
        },
        error: (errorResponse) => {
          this.isLoading = false;
          this.errorService.errorHandler(errorResponse);
        },
      });
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
}
