import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { NbDialogRef } from "@nebular/theme";
import { CustomToastrService } from "../../../../@core/services/common/custom-toastr.service";
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
    private router: Router,
    private applicationService: ApplicationService,
    private toastr: CustomToastrService
  ) {}

  ngOnInit(): void {}

  changeStatusToCanceled() {
    this.applicationService
      .cancelApplication(this.applicationId)
      .subscribe((result) => {
        this.ref.close();
        this.toastr.showToast("success", this.successMessage);
      });
  }

  navigateToApplicationCreate() {
    this.router.navigate(["pages", "applications", "edit", this.applicationId]);
    this.ref.close();
  }
}
