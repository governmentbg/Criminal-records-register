import { Injectable } from "@angular/core";
import {
  FileUploader,
  FileUploaderOptions,
  FilterFunction,
} from "ng2-file-upload";

@Injectable({
  providedIn: "root",
})
export class CustomFileUploader extends FileUploader {
  constructor(options: FileUploaderOptions = {}) {
    super(options);
  }

  addToQueue(
    files: File[],
    options?: FileUploaderOptions,
    filters?: FilterFunction[] | string
  ): void {
    // When user select 'Cancel' on dialog file select, the array of selected files is empty
    if (files.length > 0) {
      // Clear queue every time, so that the selected file is only one
      this.clearQueue();
    }

    super.addToQueue(files, options, filters);
  }

  static createUploader(): CustomFileUploader {
    let defaultOptions: FileUploaderOptions = {
      isHTML5: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
      removeAfterUpload: true,
      //allowedMimeType: [
      //  "text/plain",
      //  "application/pdf",
      //  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      //  "application/vnd.ms-excel",
      //], // TODO: remove??
      //authToken: 'Bearer ' + token //TODO: Maybe this isnt needed at all?
    };
    let result = new CustomFileUploader(defaultOptions);
    return result;
  }

  static createUploaderPDF(): CustomFileUploader {
    let defaultOptions: FileUploaderOptions = {
      isHTML5: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
      removeAfterUpload: true,
      allowedMimeType: [
       "application/pdf",
      ], // TODO: remove??
      //authToken: 'Bearer ' + token //TODO: Maybe this isnt needed at all?
    };
    let result = new CustomFileUploader(defaultOptions);
    return result;
  }
}
