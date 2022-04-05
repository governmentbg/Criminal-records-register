import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { GenderConstants } from "../../../../@core/constants/gender.constants";
import { CrudForm } from "../../../../@core/directives/crud-form.directive";
import { IsinDataService } from "../_data/isin-data.service";
import { IsinDataPreviewModel } from "../_models/isin-data-preview.model";
import { IsinDataStatusConstants } from "../_models/isin-data-status.constants";
import { IsinDataModel } from "../_models/isin-data.model";

@Component({
  selector: "cais-isin-data-preview-form",
  templateUrl: "./isin-data-preview-form.component.html",
  styleUrls: ["./isin-data-preview-form.component.scss"],
})
export class IsinDataPreviewFormComponent
  extends CrudForm<IsinDataPreviewModel, null, null, IsinDataService>
  implements OnInit
{
  constructor(service: IsinDataService, public injector: Injector) {
    super(service, injector);
  }
  public model: IsinDataPreviewModel;

  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.params["ID"];
    this.service.getForPreview(id).subscribe((response) => {
      this.model = response;

      this.model.sex =
        GenderConstants.allData.find((g) => g.id == response.sex)?.name ?? null;

      this.model.status =
        IsinDataStatusConstants.allData.find((g) => g.code == response.status)
          ?.name ?? null;
    });
  }

  public onMarkAsClosed() {
    let id = this.activatedRoute.snapshot.params["ID"];
    this.service.markAsClosed(id).subscribe(
      (res) => {
        this.toastService.showMessage("Успешно обработено съобщение");
        this.router.navigateByUrl("isin-data");
      },

      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastService.showMessage(
          "Възникна грешка при обработка на съобщението:" + errorText
        );
      }
    );
  }

  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: IsinDataModel) {
    return null;
  }
}
