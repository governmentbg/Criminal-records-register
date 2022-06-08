import { Component, Injector } from "@angular/core";
import { CommonConstants } from "../../../../@core/constants/common.constants";
import { RemoteGridWithStatePersistance } from "../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../@core/services/common/date-format.service";
import { BulletinGridService } from "../_data/bulletin-grid.service";
import { BulletinGridModel } from "../_models/bulletin-grid.model";
import { BulletinStatusTypeEnum } from "../_models/bulletin-status-type.enum";

@Component({
  selector: "cais-bulletin-active-overview",
  templateUrl: "./bulletin-active-overview.component.html",
  styleUrls: ["./bulletin-active-overview.component.scss"],
})
export class BulletinActiveOverviewComponent extends RemoteGridWithStatePersistance<
  BulletinGridModel,
  BulletinGridService
> {
  constructor(
    service: BulletinGridService,
    injector: Injector,
    public dateFormatService: DateFormatService
  ) {
    super("bulletins-search", service, injector);
    this.service.updateUrlStatus(BulletinStatusTypeEnum.Active);
    this.title = "Актуални бюлетини";
  }

  public hideStatus: boolean = true;

  ngOnInit() {
    super.ngOnInit();
  }

  onShowAllBulletinChange(isChacked: boolean) {
    if (isChacked) {
      this.service.updateUrlStatus();
    } else {
      this.service.updateUrlStatus(BulletinStatusTypeEnum.Active);
    }
    this.hideStatus = !isChacked;
    this.ngOnInit();
  }

  protected excelExportMapItem(item: BulletinGridModel) {
    let result = {};
    result["Тип"] = item.bulletinType;
    if (!this.hideStatus) {
      result["Статус"] = item.statusName;
    }

    result["Дата на създаване"] = new Date(item.createdOn).toLocaleDateString(
      CommonConstants.bgLocale
    );
    result["Номер на бюлетин"] = item.registrationNumber;
    result["Входящ номер в азб. указател"] = item.alphabeticalIndex;
    result["Име"] = item.firstName;
    result["Презиме"] = item.surName;
    result["Фамилия"] = item.familyName;

    return result;
  }
}
