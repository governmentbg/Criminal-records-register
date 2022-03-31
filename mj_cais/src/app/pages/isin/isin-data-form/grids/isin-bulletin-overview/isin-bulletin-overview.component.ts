import { Component, Injector } from "@angular/core";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { IsinBulletinGridService } from "./_data/isin-bulletin-grid.service";
import { IsinBulletinGridModel } from "./_models/isin-bulletin-grid.model";

@Component({
  selector: "cais-isin-bulletin-overview",
  templateUrl: "./isin-bulletin-overview.component.html",
  styleUrls: ["./isin-bulletin-overview.component.scss"],
})
export class IsinBulletinOverviewComponent extends RemoteGridWithStatePersistance<
  IsinBulletinGridModel,
  IsinBulletinGridService
> {
  constructor(
    public service: IsinBulletinGridService,
    public injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("bulletins-search", service, injector);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  selectBulletin(bulletinId: string) {
    let id = this.activatedRoute.snapshot.params["ID"];
    this.service.selectBulletin(id, bulletinId).subscribe(
      (res) => {
        this.toastr.showToast("success", "Успешно идентифицирано съобщение");
       // todo: navigate to ?
      },

      (error) => {
        var errorText = error.status + " " + error.statusText;
        this.toastr.showBodyToast(
          "danger",
          "Възникна грешка при избор на бюлетин:",
          errorText
        );
      }
    );
  }
}
