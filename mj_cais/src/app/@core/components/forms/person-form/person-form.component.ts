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
  public showEgnDisplay: boolean;
  public showInvalidEgnMessage: boolean = false;
  public showInvalidLnchMessage: boolean = false;
  public showInvalidEgnOrLnchMessage: boolean = false;

  ngOnInit(): void {
    // when form is init context type must be set
    this.isFbbcContext = this.contextType == PersonContextEnum.Fbbc;
    this.isBulletinContext = this.contextType == PersonContextEnum.Bulletin;
    this.isPersonContext = this.contextType == PersonContextEnum.Person;
    this.isApplicationContext =
      this.contextType == PersonContextEnum.Application;

    if (this.isFbbcContext && this.personForm.egn.value) {
      this.personForm.egnDisplay.patchValue(this.personForm.egn.value);
      this.personForm.egnDisplay.disable();
      this.showEgnDisplay = true;
    }

    if (this.isApplicationContext && this.personForm.egn.value !== null) {
      this.personForm.lnch.disable();
      this.personForm.ln.disable();
    }

    if (this.isApplicationContext && this.personForm.lnch.value !== null) {
      this.personForm.egn.disable();
      this.personForm.ln.disable();
    }

    if (this.isApplicationContext && this.personForm.ln.value !== null) {
      this.personForm.egn.disable();
      this.personForm.lnch.disable();
    }

    this.setPidWarningMessages();
  }

  private setPidWarningMessages() {
    if (this.isApplicationContext || this.isBulletinContext) {
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
            if(isValidEgn || isValidLnch){
              this.showInvalidEgnOrLnchMessage = false;
            }else{
              this.showInvalidEgnOrLnchMessage = true;
            }
          } else {
            this.showInvalidEgnOrLnchMessage = false;
          }
        });
    }
  }
}
