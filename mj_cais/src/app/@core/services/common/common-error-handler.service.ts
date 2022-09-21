import { Injectable, Injector } from "@angular/core";
import { Router } from "@angular/router";
import { CustomToastrService } from "./custom-toastr.service";

@Injectable({
  providedIn: "root",
})
export class CommonErrorHandlerService {
  protected dangerMessage = "Възникна грешка: ";

  constructor(private router: Router, private injector: Injector) {}

  errorHandler = (errorResponse) => {
    if (errorResponse.status == "401") {
      window.location.reload();
      return;
    }

    let toastr = this.injector.get<CustomToastrService>(CustomToastrService);

    let title = this.dangerMessage;
    let errorText = errorResponse.status + " " + errorResponse.statusText;
    if (errorResponse.error && errorResponse.error.customMessage) {
      title = errorResponse.error.customMessage;
      errorText = "";
    }

    toastr.showBodyToast("danger", title, errorText);
  };
}
