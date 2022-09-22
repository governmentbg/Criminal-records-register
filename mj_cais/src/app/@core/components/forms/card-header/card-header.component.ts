import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "cais-card-header",
  templateUrl: "./card-header.component.html",
  styleUrls: ["./card-header.component.scss"],
})
export class CardHeaderComponent implements OnInit {
  @Input() label: string;
  @Input() setForPreview: boolean;
  @Input() setforDelete: boolean;
  @Input() disabled: boolean;

  @Input() onSaveFunction: () => any;
  @Input() onCancelFunction: () => any;
  @Input() onDeleteFunction: () => any;

  public isForEdit: boolean;
  public isForPreview: boolean;
  public isForDelete: boolean;

  constructor(public activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.isForEdit = this.activatedRoute.snapshot.data["edit"];
    this.isForPreview = this.activatedRoute.snapshot.data["preview"] || this.setForPreview;
    this.isForDelete = this.setforDelete;
  }
}
