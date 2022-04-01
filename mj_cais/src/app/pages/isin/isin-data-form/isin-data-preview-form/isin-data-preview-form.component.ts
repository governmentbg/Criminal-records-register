import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastService } from "@tl/tl-common";
import { ToasterService } from "angular2-toaster";
import { GenderConstants } from "../../../../@core/constants/gender.constants";
import { IsinDataService } from "../_data/isin-data.service";
import { IsinDataPreviewModel } from "../_models/isin-data-preview.model";

@Component({
  selector: "cais-isin-data-preview-form",
  templateUrl: "./isin-data-preview-form.component.html",
  styleUrls: ["./isin-data-preview-form.component.scss"],
})
export class IsinDataPreviewFormComponent implements OnInit {
  constructor(
    private service: IsinDataService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastService
  ) {}
  public model: IsinDataPreviewModel;

  ngOnInit(): void {
    let id = this.route.snapshot.params["ID"];
    this.service.getForPreview(id).subscribe((response) => {
      this.model = response;

      this.model.sex =
        GenderConstants.allData.find((g) => g.id == response.sex)?.name ?? null;
    });
  }

  public onCancelFunction() {
    let backUrl = "pages/isin-new";
    this.router.navigateByUrl(backUrl);
  }

  public onMarkAsClosed() {
    let id = this.route.snapshot.params["ID"];
    this.service.markAsClosed(id).subscribe(
      (res) => {
        this.toastr.showMessage("Успешно обработено съобщение");
        this.router.navigateByUrl("isin-data");
      },

      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showMessage(
          "Възникна грешка при обработка на съобщението:" + errorText
        );
      }
    );
  }
}
