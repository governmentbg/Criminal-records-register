import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { NbDialogRef } from "@nebular/theme";
import { PersonSearchFormComponent } from "../../forms/person-search-form/person-search-form.component";
import { PersonSearchModel } from "../../forms/person-search-form/_models/person-search.model";

@Component({
  selector: "cais-person-search-dialog",
  templateUrl: "./person-search-dialog.component.html",
  styleUrls: ["./person-search-dialog.component.scss"],
})
export class PersonSearchDialogComponent implements OnInit {
  constructor(protected ref: NbDialogRef<PersonSearchDialogComponent>) {}

  @Input() personData: PersonSearchModel;


  @ViewChild("searchForm", { read: PersonSearchFormComponent })
  public searchForm: PersonSearchFormComponent;
  
  ngOnInit(): void {
   
  }

  success() {
    this.ref.close(this.searchForm.selectedItem);
  }

  dismiss() {
    this.ref.close();
  }
}
