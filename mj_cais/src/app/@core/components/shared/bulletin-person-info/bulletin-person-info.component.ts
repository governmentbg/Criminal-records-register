import { Component, Input, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { GenderConstants } from "../../../constants/gender.constants";
import { BulletinPersonInfoModel } from "./_models/bulletin-person-info.model";

@Component({
  selector: "cais-bulletin-person-info",
  templateUrl: "./bulletin-person-info.component.html",
  styleUrls: ["./bulletin-person-info.component.scss"],
})
export class BulletinPersonInfoComponent implements OnInit {
  @Input() model: BulletinPersonInfoModel;
  constructor(private router: Router) {}

  ngOnInit(): void {
    this.model.sex =
      GenderConstants.allData.find((g) => g.id == this.model.sex)?.name ?? null;
  }

  editBulletin(bulletinId: string) {
    this.router.navigate([`pages/bulletins/edit/${bulletinId}`]);
  }

  
  previewPerson(personId: string) {
    this.router.navigate([`pages/people/preview/${personId}`]);
  }
}
