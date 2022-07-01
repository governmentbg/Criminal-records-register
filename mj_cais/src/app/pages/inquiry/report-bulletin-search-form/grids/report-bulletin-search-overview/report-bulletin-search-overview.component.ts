import { Component, Injector, Input, OnInit } from "@angular/core";
import { IgxExcelExporterOptions } from "@infragistics/igniteui-angular";
import { NgxSpinnerService } from "ngx-spinner";
import { map } from "rxjs/operators";
import { CommonConstants } from "../../../../../@core/constants/common.constants";
import { RemoteGridWithStatePersistance } from "../../../../../@core/directives/remote-grid-with-state-persistance.directive";
import { DateFormatService } from "../../../../../@core/services/common/date-format.service";
import { BulletinTypeConstants } from "../../../../bulletin/bulletin-form/_models/bulletin-type-constants";
import { ReportBulletinSearchForm } from "../../_models/report-bulletin-search.form";
import { ReportBulletinGridService } from "./_data/report-bulletin-grid.service";
import { ExportBulletinModel } from "./_models/export-bulletin.model";
import { ReportBulletinGridModel } from "./_models/report-bulletin-grid.model";

@Component({
  selector: "cais-report-bulletin-search-overview",
  templateUrl: "./report-bulletin-search-overview.component.html",
  styleUrls: ["./report-bulletin-search-overview.component.scss"],
})
export class ReportBulletinSearchOverviewComponent extends RemoteGridWithStatePersistance<
  ExportBulletinModel,
  ReportBulletinGridService
> {
  constructor(
    service: ReportBulletinGridService,
    injector: Injector,
    private spinner: NgxSpinnerService,
    public dateFormatService: DateFormatService
  ) {
    super("report-bulletin-search", service, injector);
  }

  @Input() searchForm: ReportBulletinSearchForm;
  private interval;

  ngOnInit() {}

  ngOnDestroy() {
    if (this.interval) {
      clearInterval(this.interval);
    }
  }

  public onSearch = () => {
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    let filterQuery = this.getFilterQuery();
    this.service.updateUrl(`inquiry/search-bulletins?${filterQuery}`);
    debugger;
    super.ngOnInit();
  };

  // Overriding default behaviour
  protected configureExcelExportService() {
    debugger;
    if (!this.searchForm.group.valid) {
      this.searchForm.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");
      return;
    }

    this.showSpinner();

    let filterQuery = this.getFilterQuery();

    this.service
      .excelExportBulletins(filterQuery)
      .pipe(
        map((items: []) => {
          return items.map((item) => {
            return this.excelExportMapItem(item);
          });
        })
      )
      .subscribe((data) => {
        // Here are the changes
        let title = this.title ?? "Справка по характеристики на бюлетини";
        let options = new IgxExcelExporterOptions(title);
        options.columnWidth = 30;
        this.exportService.exportData(data, options);
      });
  }

  // Overriding default behaviour
  protected excelExportMapItem(item: ExportBulletinModel) {
    let result = {};

    result["Дата на създаване"] = new Date(item.createdOn).toLocaleDateString(
      CommonConstants.bgLocale
    );

    result["Номер на бюлетин"] = item.registrationNumber;
    result["Бюро съдимост, в което се съхранява"] = item.caseAuthName;
    result["Статус"] = item.statusName;
    result["Входящ номер към азбучния указател"] = item.alphabeticalIndex;
    result["ID на осъждане в ECRIS"] = item.ecrisConvictionId;
    result["Датата на постъпване на хартиения бюлетин"] = new Date(
      item.bulletinReceivedDate
    ).toLocaleDateString(CommonConstants.bgLocale);
    result["Тип"] = item.bulletinType;
    result["Съд изготвил бюлетина"] = item.bulletinAuthorityName;
    result["Дата на съставяне на бюлетина"] = new Date(
      item.bulletinCreateDate
    ).toLocaleDateString(CommonConstants.bgLocale);

    result["Съставил (имена)"] = item.createdByNames;
    result["Съставил (длъжност)"] = item.createdByPosition;
    result["Проверил (имена)"] = item.approvedByNames;
    result["Проверил (длъжност)"] = item.approvedByPosition;
    result["Вътрешен системен идентификатор на лицето"] = item.suid;
    result["Име на кирилица"] = item.firstname;
    result["Презиме на кирилица"] = item.surname;
    result["Фамилия на кирилица"] = item.familyname;
    result["Имена на кирилица"] = item.fullname;
    result["Име на латиница"] = item.firstnameLat;
    result["Презиме на латиница"] = item.surnameLat;
    result["Фамилия на латиница"] = item.familynameLat;
    result["Имена на латиница"] = item.fullnameLat;
    result["Пол"] = item.sex;
    result["Дата на раждане"] = new Date(item.birthDate).toLocaleDateString(
      CommonConstants.bgLocale
    );
    result["ЕГН"] = item.egn;
    result["ЛНЧ"] = item.lnch;
    result["ЛН"] = item.ln;
    result["Място на раждане (държава)"] = item.birthCountryName;
    result["Място на раждане (област)"] = item.birthMunName;
    result["Място на раждане (община)"] = item.birthDistrictName;
    result["Място на раждане (град)"] = item.birthCityName;
    result["Място на раждане (описание)"] = item.birthPlaceOther;
    //result["Гражданство"] = item.familyName;// todo
    result["Номер на документ за самоличност"] = item.idDocNumber;
    result["Категория на документ за самоличност"] = item.idDocCategoryName;
    result["Друга категория в случай че липсва"] = item.idDocTypeDescr;
    result["Издаващ орган на документ за самоличност"] =
      item.idDocIssuingAuthority;
    result["Дата на издаване на документ за самоличност"] =
      item.idDocIssuingDate;
    result["Дата на валидност на документ за самоличност"] =
      item.idDocValidDate;
    result["Майка - име"] = item.motherFirstname;
    result["Майка - презиме"] = item.motherSurname;
    result["Майка - фамилия"] = item.motherFamilyname;
    result["Майка - имена"] = item.motherFullname;
    result["Баща - име"] = item.fatherFirstname;
    result["Баща - презиме"] = item.fatherSurname;
    result["Баща - фамилия"] = item.fatherFamilyname;
    result["Баща - имена"] = item.fatherFirstname;
    result["Вид на акта"] = item.decisionTypeName;
    result["Номер на акта"] = item.decisionNumber;
    result["Дата на издаване на акта"] = new Date(
      item.decisionDate
    ).toLocaleDateString(CommonConstants.bgLocale);
    result["Дата на влизане в сила на акта"] = new Date(
      item.decisionFinalDate
    ).toLocaleDateString(CommonConstants.bgLocale);
    result["Съдебен орган издал акта"] = item.decidingAuthName;
    result["ECLI номер"] = item.decisionEcli;
    result["Вид на делото"] = item.caseTypeName;
    result["Номер на делото"] = item.caseNumber;
    result["Година на делото"] = item.caseYear;
    result["Съд на делото"] = item.caseAuthName;
    result["Постановено изтърпяване на предходна условна присъда"] =
      item.prevSuspSent == true ? "Да" : "Не";
    result["Номер на предходна присъда"] = item.prevSuspSentDescr;
    result["Лицето е гражданин на ЕС"] = item.euCitizen == true ? "Да" : "Не";
    result["Лицето е гражданин на държава от трети страни"] =
      item.tcnCitizen == true ? "Да" : "Не";
    result["Деецът не е наказан съгласно НК"] =
      item.noSanction == true ? "Да" : "Не";
    result["Дата на унищожаване"] = new Date(item.updatedOn).toLocaleDateString(
      CommonConstants.bgLocale
    );
    result["Дата на реабилитация"] = new Date(
      item.updatedOn
    ).toLocaleDateString(CommonConstants.bgLocale);
    result["Дата на последна промяна"] = new Date(
      item.updatedOn
    ).toLocaleDateString(CommonConstants.bgLocale);
    result["Потребител създал бюлетин"] = item.createdByUsername;
    result["Потребител извършил последна промяна на бюлетин"] =
      item.updatedByUsername;

    return result;
  }

  private getFilterQuery(): string {
    let formObj = this.searchForm.group.getRawValue();
    let offenceCategory = {};
    offenceCategory["offenceCategory"] =
      this.searchForm.offenceCategory.id.value;
    let filterQuery = this.service.constructQueryParamsByFilters(formObj, "");
    filterQuery = this.service.constructQueryParamsByFilters(
      offenceCategory,
      filterQuery
    );
    return filterQuery;
  }

  private showSpinner() {
    this.service.isLoading = true;
    this.spinner.show();
    this.interval = setInterval(() => {
      let isHiden = this.hideSpinner();
      if (isHiden) {
        clearInterval(this.interval);
      }
    }, 500);
  }

  private hideSpinner(): boolean {
    if (!this.service.isLoading) {
      this.spinner.hide();
      return true;
    }
    return false;
  }
}
