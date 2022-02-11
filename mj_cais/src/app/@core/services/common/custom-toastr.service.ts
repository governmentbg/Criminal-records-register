import { Injectable } from "@angular/core";
import {
  NbComponentStatus,
  NbGlobalPhysicalPosition,
  NbToastrConfig,
  NbToastrService,
} from "@nebular/theme";

@Injectable({
  providedIn: "root",
})
export class CustomToastrService extends NbToastrService {
  private defaultConfig = {
    status: null,
    destroyByClick: false,
    duration: 5000,
    position: NbGlobalPhysicalPosition.BOTTOM_RIGHT,
    preventDuplicates: false,
  };

  public showToast(status: NbComponentStatus, title: string) {
    return this.showBodyToast(status, title, "");
  }

  public showBodyToast(status: NbComponentStatus, title: string, body: string) {
    let config = { ...this.defaultConfig }; // Copy default config
    config.status = status;
    this.show(body, title, config);
  }

  public showFullToast(title: string, body: string, config: NbToastrConfig) {
    this.show(body, title, config);
  }
}
