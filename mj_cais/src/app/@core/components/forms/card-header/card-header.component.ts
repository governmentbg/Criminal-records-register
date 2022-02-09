import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "cais-card-header",
  templateUrl: "./card-header.component.html",
  styleUrls: ["./card-header.component.scss"],
})
export class CardHeaderComponent implements OnInit {
  @Input() label: string;
  @Input() onSaveFunction: () => any;
  @Input() onCancelFunction: () => any;

  public isForEdit: boolean;
  public isForPreview: boolean;

  constructor(public activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.isForEdit = this.activatedRoute.snapshot.data["edit"];
    this.isForPreview = this.activatedRoute.snapshot.data["preview"];
  }
}
