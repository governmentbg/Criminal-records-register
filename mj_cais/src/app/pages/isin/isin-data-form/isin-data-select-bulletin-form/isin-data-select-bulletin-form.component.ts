import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { GenderConstants } from "../../../../@core/constants/gender.constants";
import { CrudForm } from "../../../../@core/directives/crud-form.directive";
import { IsinDataService } from "../_data/isin-data.service";
import { IsinDataStatusConstants } from "../_models/isin-data-status.constants";
import { IsinDataModel } from "../_models/isin-data.model";

@Component({
  selector: "cais-isin-data-select-bulletin-form",
  templateUrl: "./isin-data-select-bulletin-form.component.html",
  styleUrls: ["./isin-data-select-bulletin-form.component.scss"],
})
export class IsinDataSelectBulletinFormComponent
  extends CrudForm<IsinDataModel, null, null, IsinDataService>
  implements OnInit
{
  constructor(service: IsinDataService, public injector: Injector) {
    super(service, injector);
  }
  public model: IsinDataModel;

  ngOnInit(): void {
    let id = this.activatedRoute.snapshot.params["ID"];
    this.service.get(id).subscribe((response) => {
      this.model = response;

      this.model.sex =
        GenderConstants.allData.find((g) => g.id == response.sex)?.name ?? null;

      this.model.status =
        IsinDataStatusConstants.allData.find((g) => g.code == response.status)
          ?.name ?? null;
    });
  }
  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: IsinDataModel) {
    return null;
  }
}
