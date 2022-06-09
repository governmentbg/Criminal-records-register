import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { NbDialogRef } from "@nebular/theme";

@Component({
  selector: "cais-search-by-egn-error-dialog",
  templateUrl: "./search-by-egn-error-dialog.component.html",
  styleUrls: ["./search-by-egn-error-dialog.component.scss"],
})
export class SearchByEgnErrorDialogComponent implements OnInit {
  public title: string;
  public applicationId: string = null;

  constructor(
    protected ref: NbDialogRef<SearchByEgnErrorDialogComponent>,
    private router: Router
  ) {}

  ngOnInit(): void {}

  changeStatusToCanceled() {}

  navigateToApplicationCreate() {
    this.router.navigate(["pages", "applications", "edit", this.applicationId]);
    this.ref.close();
  }
}
