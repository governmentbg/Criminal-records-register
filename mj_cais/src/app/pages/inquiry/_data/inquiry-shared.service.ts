import { Injectable } from "@angular/core";
import { NgxSpinnerService } from "ngx-spinner";
import { CommonConstants } from "../../../@core/constants/common.constants";
import { GenderConstants } from "../../../@core/constants/gender.constants";
import { ExportBulletinModel } from "../_models/export-bulletin.model";

@Injectable({
  providedIn: "root",
})
export class InquirySharedService {
  constructor(private spinner: NgxSpinnerService) {}
  private interval;

  public showSpinner(service) {
    service.isLoading = true;
    this.spinner.show();
    this.interval = setInterval(() => {
      let isHiden = this.hideSpinner(service);
      if (isHiden) {
        clearInterval(this.interval);
      }
    }, 500);
  }

  public hideSpinner(service): boolean {
    if (!service.isLoading) {
      this.spinner.hide();
      return true;
    }
    return false;
  }

  public getInterval() {
    return this.interval;
  }

  public clearInterval() {
    clearInterval(this.interval);
  }

  public excelExportBulletinMapItem(item: ExportBulletinModel) {
    let result = {};

    let createdOn = item.createdOn
      ? new Date(item.createdOn).toLocaleDateString(CommonConstants.bgLocale)
      : "";

    result["Дата на създаване"] = createdOn;
    result["Номер на бюлетин"] = item.registrationNumber;
    result["Бюро съдимост, в което се съхранява"] = item.csAuthorityName;
    result["Статус"] = item.statusName;
    result["Входящ номер към азбучния указател"] = item.alphabeticalIndex;
    result["ID на осъждане в ECRIS"] = item.ecrisConvictionId;

    let bulletinReceivedDate = item.bulletinReceivedDate
      ? new Date(item.bulletinReceivedDate).toLocaleDateString(
          CommonConstants.bgLocale
        )
      : "";

    result["Датата на постъпване на хартиения бюлетин"] = bulletinReceivedDate;
    result["Тип"] = item.bulletinType;
    result["Съд изготвил бюлетина"] = item.bulletinAuthorityName;

    let bulletinCreateDate = item.bulletinCreateDate
      ? new Date(item.bulletinCreateDate).toLocaleDateString(
          CommonConstants.bgLocale
        )
      : "";

    result["Дата на съставяне на бюлетина"] = bulletinCreateDate;
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

    result["Пол"] = item.sex == GenderConstants.female.id ? GenderConstants.female.name :
    item.sex == GenderConstants.male.id ? GenderConstants.male.name :  
    item.sex == GenderConstants.unknown.id ? GenderConstants.unknown.name : '';

    let birthDate = item.birthDate
      ? new Date(item.birthDate).toLocaleDateString(CommonConstants.bgLocale)
      : "";

    result["Дата на раждане"] = birthDate;
    result["Гражданство 1"] = item.countryName1;
    result["Гражданство 2"] = item.countryName2;
    result["Гражданство 3"] = item.countryName3;
    result["Гражданство 4"] = item.countryName4;
    result["ЕГН"] = item.egn;
    result["ЛНЧ"] = item.lnch;
    result["ЛН"] = item.ln;
    result["Място на раждане (държава)"] = item.birthCountryName;
    result["Място на раждане (област)"] = item.birthMunName;
    result["Място на раждане (община)"] = item.birthDistrictName;
    result["Място на раждане (град)"] = item.birthCityName;
    result["Място на раждане (описание)"] = item.birthPlaceOther;
    result["Номер на документ за самоличност"] = item.idDocNumber;
    result["Категория на документ за самоличност"] = item.idDocCategoryName;
    result["Друга категория в случай че липсва"] = item.idDocTypeDescr;
    result["Издаващ орган на документ за самоличност"] =
      item.idDocIssuingAuthority;

    let idDocIssuingDate = item.idDocIssuingDate
      ? new Date(item.idDocIssuingDate).toLocaleDateString(
          CommonConstants.bgLocale
        )
      : "";
    result["Дата на издаване на документ за самоличност"] = idDocIssuingDate;

    let idDocValidDate = item.idDocValidDate
      ? new Date(item.idDocValidDate).toLocaleDateString(
          CommonConstants.bgLocale
        )
      : "";

    result["Дата на валидност на документ за самоличност"] = idDocValidDate;
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

    let decisionDate = item.decisionDate
      ? new Date(item.decisionDate).toLocaleDateString(CommonConstants.bgLocale)
      : "";

    result["Дата на издаване на акта"] = decisionDate;

    let decisionFinalDate = item.decisionFinalDate
      ? new Date(item.decisionFinalDate).toLocaleDateString(
          CommonConstants.bgLocale
        )
      : "";

    result["Дата на влизане в сила на акта"] = decisionFinalDate;
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

    let deleteDate = item.deleteDate
      ? new Date(item.deleteDate).toLocaleDateString(CommonConstants.bgLocale)
      : "";

    result["Дата на унищожаване"] = deleteDate;

    let rehabilitationDate = item.rehabilitationDate
      ? new Date(item.rehabilitationDate).toLocaleDateString(
          CommonConstants.bgLocale
        )
      : "";

    result["Дата на реабилитация"] = rehabilitationDate;

    let updatedOn = item.updatedOn
      ? new Date(item.updatedOn).toLocaleDateString(CommonConstants.bgLocale)
      : "";
    result["Дата на последна промяна"] = updatedOn;
    result["Потребител създал бюлетин"] = item.createdByUsername;
    result["Потребител извършил последна промяна на бюлетин"] =
      item.updatedByUsername;

    return result;
  }
}
