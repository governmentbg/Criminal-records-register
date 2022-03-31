import { Component, OnInit } from "@angular/core";
import {
  ActivatedRoute,
  Router,
} from "@angular/router";
import { GenderConstants } from "../../../@core/constants/gender.constants";
import { IsinDataService } from "./_data/isin-data.service";
import { IsinDataModel } from "./_models/isin-data.model";

@Component({
  selector: "cais-isin-data-form",
  templateUrl: "./isin-data-form.component.html",
  styleUrls: ["./isin-data-form.component.scss"],
})
export class IsinDataFormComponent implements OnInit {
  constructor(
    private service: IsinDataService,
    private route: ActivatedRoute,
    private router: Router
  ) {}
  public model: IsinDataModel;

  ngOnInit(): void {
    let id = this.route.snapshot.params["ID"];
    this.service.get(id).subscribe((response) => {
      this.model = response;

      this.model.sex =
        GenderConstants.allData.find((g) => g.id == response.sex)?.name ?? null;
    });
  }

  public onCancelFunction() {
    let backUrl = "pages/isin-new";
    this.router.navigateByUrl(backUrl);
  }
}
