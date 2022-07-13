import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { NbDialogRef, NbDialogService } from "@nebular/theme";
import { CommonConstants } from "../../../../@core/constants/common.constants";
import { CustomToastrService } from "../../../../@core/services/common/custom-toastr.service";
import { CancelDialogComponent } from "../../application-form/cancel-dialog/cancel-dialog.component";
import { ApplicationService } from "../../application-form/_data/application.service";

@Component({
  selector: "cais-search-by-egn-error-dialog",
  templateUrl: "./search-by-egn-error-dialog.component.html",
  styleUrls: ["./search-by-egn-error-dialog.component.scss"],
})
export class SearchByEgnErrorDialogComponent implements OnInit {
  public title: string;
  public applicationId: string = null;
  protected successMessage = "Успешно анулирано!";

  constructor(
    protected ref: NbDialogRef<SearchByEgnErrorDialogComponent>,
    private dialogService: NbDialogService,
    private router: Router,
    private applicationService: ApplicationService,
    private toastr: CustomToastrService
  ) {}

  ngOnInit(): void {}

  changeStatusToCanceled() {
    this.dialogService
      .open(CancelDialogComponent, CommonConstants.defaultDialogConfig)
      .onClose.subscribe((x) => {
        if (x) {
          this.applicationService
            .cancelApplication(this.applicationId, x)
            .subscribe((result) => {
              this.ref.close();
              let message = "Успешно анулирано";
              this.toastr.showToast("success", message);
            });
        }
      });
  }

  navigateToApplicationCreate() {
    this.router.navigate(["pages", "applications", "edit", this.applicationId]);
    this.ref.close();
  }
}
