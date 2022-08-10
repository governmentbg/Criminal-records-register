import { Component, Input, OnInit } from "@angular/core";
import { BaseNomenclatureModel } from "../../../models/nomenclature/base-nomenclature.model";
import { PersonAliasModel } from "../../../models/common/person-alias.model";
import { PersonForm } from "./_models/person.form";
import { PersonContextEnum } from "./_models/person-context-enum";
import { EgnUtils } from "../../../utils/egn.utils";
import { LnchUtils } from "../../../utils/lnch.utils";

@Component({
  selector: "cais-person-form[contextType]",
  templateUrl: "./person-form.component.html",
  styleUrls: ["./person-form.component.scss"],
})
export class PersonFormComponent implements OnInit {
  @Input() personForm: PersonForm;
  @Input() isEditable: boolean;
  @Input() personAliasGridData: PersonAliasModel[];
  @Input() personAliasTypes: BaseNomenclatureModel[] = [];
  @Input() genderTypes: BaseNomenclatureModel[] = [];
  @Input() countries: BaseNomenclatureModel[] = [];
  @Input() idDocumentCategoryTypes: BaseNomenclatureModel[] = [];
  @Input() contextType: string;

  public isFbbcContext: boolean;
  public isBulletinContext: boolean;
  public isPersonContext: boolean;
  public isApplicationContext: boolean;
  public isReportApplicationContext: boolean;
  public showEgnDisplay: boolean;
  public showInvalidEgnMessage: boolean = false;
  public showInvalidLnchMessage: boolean = false;
  public showInvalidEgnOrLnchMessage: boolean = false;
  public hasDataFromRegix: boolean = false;

  ngOnInit(): void {
    this.isFbbcContext = this.contextType == PersonContextEnum.Fbbc;
    this.isBulletinContext = this.contextType == PersonContextEnum.Bulletin;
    this.isPersonContext = this.contextType == PersonContextEnum.Person;
    this.isApplicationContext =
      this.contextType == PersonContextEnum.Application;
    this.isReportApplicationContext =
      this.contextType == PersonContextEnum.ReportApplication;

    if (this.isFbbcContext && this.personForm.egn.value) {
      this.personForm.egnDisplay.patchValue(this.personForm.egn.value);
      this.personForm.egnDisplay.disable();
      this.showEgnDisplay = true;
    }

    if(this.isApplicationContext || this.isReportApplicationContext) {
      // has data from regix
      this.hasDataFromRegix = this.personForm.egn.value !== null ||
      this.personForm.lnch.value !== null;
      this.personForm.ln.value !== null;
      if(this.hasDataFromRegix){
        this.personForm.egnDisplay.patchValue(this.personForm.egn.value);
        this.personForm.lnchDisplay.patchValue(this.personForm.lnch.value);
        this.personForm.lnDisplay.patchValue(this.personForm.ln.value);
        this.personForm.suidDisplay.patchValue(this.personForm.suid.value);
      }
    }

    this.personForm.suidDisplay.patchValue(this.personForm.suid.value);
    this.setPidWarningMessages();
  }

  private setPidWarningMessages() {
    if (this.isApplicationContext || this.isBulletinContext || this.isReportApplicationContext) {
      this.personForm.group
        .get("egn")
        .valueChanges.subscribe((selectedValue) => {
          if (selectedValue) {
            let isValidEgn = EgnUtils.isValid(selectedValue);
            this.showInvalidEgnMessage = !isValidEgn;
          } else {
            this.showInvalidEgnMessage = false;
          }
        });

      this.personForm.group
        .get("lnch")
        .valueChanges.subscribe((selectedValue) => {
          if (selectedValue) {
            let isValidLnch = LnchUtils.isValid(selectedValue);
            this.showInvalidLnchMessage = !isValidLnch;
          } else {
            this.showInvalidLnchMessage = false;
          }
        });
    } else if (this.isFbbcContext) {
      this.personForm.group
        .get("egn")
        .valueChanges.subscribe((selectedValue) => {
          if (selectedValue) {
            let isValidEgn = EgnUtils.isValid(selectedValue);
            let isValidLnch = LnchUtils.isValid(selectedValue);
            if (isValidEgn || isValidLnch) {
              this.showInvalidEgnOrLnchMessage = false;
            } else {
              this.showInvalidEgnOrLnchMessage = true;
            }
          } else {
            this.showInvalidEgnOrLnchMessage = false;
          }
        });
    }
  }
}
