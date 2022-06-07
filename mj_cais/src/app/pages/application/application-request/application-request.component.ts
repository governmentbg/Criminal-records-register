import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { NbDialogService } from "@nebular/theme";
import { SearchByEgnDialogComponent } from "./search-by-egn-dialog/search-by-egn-dialog.component";
import { SearchByIdentifierService } from "./_data/search-by-Identifier.service";

@Component({
  selector: "cais-application-request",
  templateUrl: "./application-request.component.html",
  styleUrls: ["./application-request.component.scss"],
})
export class ApplicationRequestComponent implements OnInit {
  constructor(
    private dialogService: NbDialogService,
    private searchByIdentifierService: SearchByIdentifierService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  public searchByEGN() {
    this.dialogService
      .open(SearchByEgnDialogComponent, {
        context: {
          title: "Търсене по ЕГН",
        },
        closeOnBackdropClick: true,
      })
      .onClose.subscribe((result) => {
        if (result) {
          //TODO: Get Application
          this.searchByIdentifierService
            .searchByIdentifier(result)
            .subscribe((result: any) => {
              debugger;
              this.router.navigate([
                "pages",
                "applications",
                "edit",
                result.id,
              ]);
            });
        }
      });
  }

  public searchByLNCH() {
    this.dialogService
      .open(SearchByEgnDialogComponent, {
        context: {
          title: "Търсене по ЛНЧ",
        },
        closeOnBackdropClick: true,
      })
      .onClose.subscribe((result) => {
        if (result) {
          this.searchByIdentifierService
            .searchByIdentifierLNCH(result)
            .subscribe((result: any) => {
              debugger;
              this.router.navigate([
                "pages",
                "applications",
                "edit",
                result.id,
              ]);
            });
        }
      });
  }

  public searchForForeigner() {
    this.router.navigate(["pages", "applications", "create"]);
  }
}
