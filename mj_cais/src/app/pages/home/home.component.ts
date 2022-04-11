import { Component, Injector, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { HomeService } from "./_data/home.service";
import { ObjectCountModel } from "./_models/object-count.model";

@Component({
  selector: "cais-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  constructor(private service: HomeService) {}
  public model: ObjectCountModel;

  ngOnInit(): void {
    this.service.get().subscribe((response) => {
      this.model = response;
    });
  }

  buildFormImpl(): FormGroup {
    return null;
  }

  createInputObject(object: ObjectCountModel) {
    return null;
  }
}
