import { Component, Input, OnInit } from "@angular/core";
import { BaseNomenclatureModel } from "../../../models/nomenclature/base-nomenclature.model";
import { BulletinPersonAliasModel } from "../../shared/bulletin-person-info/_models/bulletin-person-alias.model";
import { PersonForm } from "./_models/person.form";

@Component({
  selector: "cais-person-form",
  templateUrl: "./person-form.component.html",
  styleUrls: ["./person-form.component.scss"],
})
export class PersonFormComponent implements OnInit {
  @Input() personForm: PersonForm;
  @Input() isEditable: boolean;
  @Input() personAliasGridData: BulletinPersonAliasModel[];
  @Input() personAliasTypes: BaseNomenclatureModel[] = [];
  @Input() genderTypes: BaseNomenclatureModel[] = [];
  @Input() countries: BaseNomenclatureModel[] = [];
  @Input() idDocumentCategoryTypes: BaseNomenclatureModel[] = [];

  ngOnInit(): void {}
}
