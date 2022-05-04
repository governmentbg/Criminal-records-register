import { Component, Input, OnInit } from "@angular/core";
import { BaseNomenclatureModel } from "../../../models/nomenclature/base-nomenclature.model";
import { PersonAliasModel } from "../../../models/common/person-alias.model";
import { PersonForm } from "./_models/person.form";
import { PersonContextEnum } from "./_models/person-context-enum";

@Component({
  selector: "cais-person-form",
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

  public isFbbcContext: boolean;
  public isBulletinContext: boolean;
  public isPersonContext: boolean;
  public isApplicationContext: boolean;

  ngOnInit(): void {
    // when form is init context type must be set
    let context = this.personForm.contextType.value;
    this.isFbbcContext = context == PersonContextEnum.Fbbc;
    this.isBulletinContext = context == PersonContextEnum.Bulletin;
    this.isPersonContext = context == PersonContextEnum.Person;
    this.isApplicationContext = context == PersonContextEnum.Application;
  }
}
