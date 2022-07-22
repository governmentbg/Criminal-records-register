import { Component, OnInit } from "@angular/core";
import { NbDialogRef } from "@nebular/theme";

@Component({
  selector: "cais-search-by-egn-dialog",
  templateUrl: "./search-by-egn-dialog.component.html",
  styleUrls: ["./search-by-egn-dialog.component.scss"],
})
export class SearchByEgnDialogComponent implements OnInit {
  
  public title: string;
  public searchValue: string = null;

  constructor(protected ref: NbDialogRef<SearchByEgnDialogComponent>) {}

  ngOnInit(): void {}

 
  onSubmit() {
    this.ref.close(this.searchValue);
  }

  onCancel() {
    this.ref.close();
  }
}
