import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { NbDialogService } from "@nebular/theme";
import { CustomToastrService } from "../../../@core/services/common/custom-toastr.service";
import { EgnUtils } from "../../../@core/utils/egn.utils";
import { LnchUtils } from "../../../@core/utils/lnch.utils";
import { SearchByEgnDialogComponent } from "./search-by-egn-dialog/search-by-egn-dialog.component";
import { SearchByEgnErrorDialogComponent } from "./search-by-egn-error-dialog/search-by-egn-error-dialog.component";
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
    private router: Router,
    private toastr: CustomToastrService
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
          var isValidEgn = EgnUtils.isValid(result);
          if(!isValidEgn){
            this.toastr.showToast("danger", "Невалидно ЕГН!");
            return;
          }
          //TODO: Get Application
          this.searchByIdentifierService.searchByIdentifier(result).subscribe(
            (result: any) => {
              this.router.navigate([
                "pages",
                "applications",
                "edit",
                result.id,
              ]);
            },
            (error) => {
              var parser = new DOMParser();
              var htmlDoc = parser.parseFromString(error.error, "text/html");
              let errMsgElement = htmlDoc.getElementById("err-message");
              let errMsg = (errMsgElement.firstChild as any).data;
              let errTittle = (errMsg as string).split(":")[0];
              let errBody = (errMsg as string).split(":")[1];

              this.dialogService
                .open(SearchByEgnErrorDialogComponent, {
                  context: {
                    title: errTittle,
                    applicationId: errBody,
                  },
                  closeOnBackdropClick: false,
                })
                .onClose.subscribe((result) => {
                  if (result) {
                    //TODO: Get Application
                  }
                });
            }
          );
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
          var isValidLnch = LnchUtils.isValid(result);
          if(!isValidLnch){
            this.toastr.showToast("danger", "Невалидно ЛНЧ!");
            return;
          }
          this.searchByIdentifierService
            .searchByIdentifierLNCH(result)
            .subscribe(
              (result: any) => {
                this.router.navigate([
                  "pages",
                  "applications",
                  "edit",
                  result.id,
                ]);
              },
              (error) => {
                var parser = new DOMParser();
                var htmlDoc = parser.parseFromString(error.error, "text/html");
                let errMsgElement = htmlDoc.getElementById("err-message");
                let errMsg = (errMsgElement.firstChild as any).data;
                let errTittle = (errMsg as string).split(":")[0];
                let errBody = (errMsg as string).split(":")[1];

                this.dialogService
                  .open(SearchByEgnErrorDialogComponent, {
                    context: {
                      title: errTittle,
                      applicationId: errBody,
                    },
                    closeOnBackdropClick: false,
                  })
                  .onClose.subscribe((result) => {
                    if (result) {
                      //TODO: Get Application
                    }
                  });
              }
            );
        }
      });
  }

  public searchForForeigner() {
    this.router.navigate(["pages/applications/create"]);
  }
}
