import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  ViewChild,
} from "@angular/core";
import {
  IgxDialogComponent,
  IgxGridComponent,
  IgxGridRowComponent,
} from "@infragistics/igniteui-angular";
import { FileItem, FileLikeObject } from "ng2-file-upload";
import { Observable, ReplaySubject } from "rxjs";
import { CustomToastrService } from "../../../../../@core/services/common/custom-toastr.service";
import { CustomFileUploader } from "../../../../../@core/utils/custom-file-uploader";
import { BulletinDocumentForm } from "../../models/bulletin-document.form";

@Component({
  selector: "cais-bulletin-document-form",
  templateUrl: "./bulletin-document-form.component.html",
  styleUrls: ["./bulletin-document-form.component.scss"],
})
export class BulletinDocumentFormComponent implements OnInit {
  @Input() bulletinDocumentsTransactions: string;
  @Input() dbData: any;
  @Input() isForPreview: boolean;

  @ViewChild("documentsGrid", {
    read: IgxGridComponent,
  })
  public documentsGrid: IgxGridComponent;

  @ViewChild("dialogAdd", { read: IgxDialogComponent })
  public dialog: IgxDialogComponent;

  public bulletinDocumentForm = new BulletinDocumentForm();
  public uploader: CustomFileUploader;
  constructor(public toastr: CustomToastrService) {
    this.initializeUploader();
  }

  ngOnInit(): void {}

  onAddOrUpdateBulletineDocumentRow() {
    if (!this.bulletinDocumentForm.group.valid) {
      this.bulletinDocumentForm.group.markAllAsTouched();
      return;
    }

    if (this.bulletinDocumentForm.docTypeId.value) {
      let docTypeName = (this.dbData.getDocumentTypes as any).find(
        (x) => x.id === this.bulletinDocumentForm.docTypeId.value
      )?.name;
      this.bulletinDocumentForm.docTypeName.patchValue(docTypeName);
    }

    debugger;
    if (this.bulletinDocumentForm.documentContent) {
      this.bulletinDocumentForm.documentContent.value;
    }

    let currentRow = this.documentsGrid.getRowByKey(
      this.bulletinDocumentForm.id.value
    );

    if (currentRow) {
      currentRow.update(this.bulletinDocumentForm.group.value);
    } else {
      this.documentsGrid.addRow(this.bulletinDocumentForm.group.value);
    }

    this.onCloseBulletinDocumentDilog();
  }

  onCloseBulletinDocumentDilog() {
    this.bulletinDocumentForm = new BulletinDocumentForm();
    this.dialog.close();
    this.uploader = CustomFileUploader.createUploader();
  }

  initializeUploader() {
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
        const blob = new Blob([file.rawFile], { type: file.type }) ;

        this.convertFile(blob).subscribe(base64 => {
          this.bulletinDocumentForm.documentContent.setValue(base64);
          this.bulletinDocumentForm.mimeType.setValue(file.type);
        });
      }
    };
  }

  convertFile(file : Blob) : Observable<string> {
    const result = new ReplaySubject<string>(1);
    const reader = new FileReader();
    reader.readAsBinaryString(file);
    reader.onload = (event) => result.next(btoa(event.target.result.toString()));
    return result;
  }
  
  onRmoveDocument(item: FileItem) {
    this.bulletinDocumentForm.documentContent.setValue(null);
    item.remove();
  }
  
  public onDeleteBulletinDocument(event: IgxGridRowComponent) {
    this.documentsGrid.deleteRow(event.rowData.id);
    this.documentsGrid.data = this.documentsGrid.data.filter(
      (d) => d.id != event.rowData.id
    );
  }
}
