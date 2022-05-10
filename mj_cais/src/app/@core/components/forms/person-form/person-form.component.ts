import { Component, Input, OnInit } from "@angular/core";
import { BaseNomenclatureModel } from "../../../models/nomenclature/base-nomenclature.model";
import { PersonAliasModel } from "../../../models/common/person-alias.model";
import { PersonForm } from "./_models/person.form";
import { PersonContextEnum } from "./_models/person-context-enum";

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

  ngOnInit(): void {
    // when form is init context type must be set
   
    this.isFbbcContext = this.contextType == PersonContextEnum.Fbbc;
    this.isBulletinContext =  this.contextType == PersonContextEnum.Bulletin;
    this.isPersonContext =  this.contextType == PersonContextEnum.Person;
    this.isApplicationContext =  this.contextType == PersonContextEnum.Application;
  }
}
