import { Component, Injector } from "@angular/core";
import * as fileSaver from "file-saver";
import { RemoteGridWithStatePersistance } from "../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../@core/services/common/date-format.service";
import { LoaderService } from "../../../@core/services/common/loader.service";
import { EcrisInboxGridService } from "./_data/ecris-inbox-grid.service";
import { EcrisInboxGridModel } from "./_models/ecris-inbox-grid.model";
import { EcrisInboxStatusConstants } from "./_models/ecris-inbox-status.constants";

@Component({
  selector: "cais-ecris-inbox-overview",
  templateUrl: "./ecris-inbox-overview.component.html",
  styleUrls: ["./ecris-inbox-overview.component.scss"],
})
export class EcrisInboxOverviewComponent extends RemoteGridWithStatePersistance<
  EcrisInboxGridModel,
  EcrisInboxGridService
> {
  public hideStatus: boolean = true;

  constructor(
    public dateFormatService: DateFormatService,
    service: EcrisInboxGridService,
    injector: Injector,
    public loaderService: LoaderService
  ) {
    super("ecris-inbox-search", service, injector);
    this.service.updateUrlStatus(EcrisInboxStatusConstants.Error);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  onShowAllMessageChange(isChacked: boolean) {
    if (isChacked) {
      //removed filter entirely
      this.service.updateUrlStatus();
    } else {
      this.service.updateUrlStatus(EcrisInboxStatusConstants.Error);
    }
    this.hideStatus = !isChacked;
    this.ngOnInit();
  }

  downloadXml(id: string) {
    this.download(id, false);
  }

  downloadXmlTraits(id: string) {
    this.download(id, true);
  }

  download(id: string, traits: boolean) {
    this.loaderService.show();
    this.service.download(id, traits).subscribe(
      (response: any) => {
        let blob = new Blob([response.body]);
        window.URL.createObjectURL(blob);

        let header = response.headers.get("Content-Disposition");
        let filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;

        let fileName = "download";

        var matches = filenameRegex.exec(header);
        if (matches != null && matches[1]) {
          fileName = matches[1].replace(/['"]/g, "");
        }

        fileSaver.saveAs(blob, fileName);
        this.loaderService.hide();
      },
      (error) => {
        this.loaderService.hide();
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast(
          "danger",
          "Грешка при изтегляне на файла: ",
          errorText
        );
      }
    );
  }
}
