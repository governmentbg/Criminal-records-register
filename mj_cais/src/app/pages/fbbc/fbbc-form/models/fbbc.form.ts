import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AddressForm } from "../../../../@core/components/forms/address-form/_model/address.form";
import { LookupForm } from "../../../../@core/components/forms/inputs/lookup/models/lookup.form";
import { PersonContextEnum } from "../../../../@core/components/forms/person-form/_models/person-context-enum";
import { PersonForm } from "../../../../@core/components/forms/person-form/_models/person.form";
import { BaseForm } from "../../../../@core/models/common/base.form";

export class FbbcForm extends BaseForm {
  public group: FormGroup;

  public countryLookup: LookupForm;
  public docTypeId: FormControl;
  public sanctionTypeId: FormControl;
  public receiveDate: FormControl;
  public issueDate: FormControl;
  public countryDescr: FormControl;
  public person: PersonForm;
  public offenceStartDate: FormControl;
  public offenceEndDate: FormControl;
  public annotation: FormControl;
  public gdkpNumber: FormControl;
  public gdkpDate: FormControl;
  public gdkpCaseNumber: FormControl;
  public gdkpTom: FormControl;
  public gdkpStr: FormControl;
  public njrCountry: FormControl;
  public njrIdentifier: FormControl;
  public njrFirstId: FormControl;
  public ecrisMsgId: FormControl;
  public ecrisConvId: FormControl;
  public ecrisUpdConvTypeId: FormControl;
  public ecrisUpdConvId: FormControl;
  public isAdministrative: FormControl;
  public convDecisionDate: FormControl;
  public convDecFinalDate: FormControl;
  public sequentialIndex: FormControl;
  public destroyedDate: FormControl;
  public personId: FormControl;
  public version: FormControl;
  public address: AddressForm;
  public statusCode: FormControl;
  public oldId: FormControl;

  constructor() {
    super();
    this.countryLookup = new LookupForm(false);
    this.docTypeId = new FormControl(null);
    this.sanctionTypeId = new FormControl(null);
    this.receiveDate = new FormControl(null);
    this.issueDate = new FormControl(null);
    this.countryDescr = new FormControl(null);
    this.person = new PersonForm(PersonContextEnum.Fbbc,false);
    this.offenceStartDate = new FormControl(null);
    this.offenceEndDate = new FormControl(null);
    this.annotation = new FormControl(null);
    this.gdkpNumber = new FormControl(null);
    this.gdkpDate = new FormControl(null);
    this.gdkpCaseNumber = new FormControl(null);
    this.gdkpTom = new FormControl(null);
    this.gdkpStr = new FormControl(null);
    this.njrCountry = new FormControl(null);
    this.njrIdentifier = new FormControl(null);
    this.njrFirstId = new FormControl(null);
    this.ecrisMsgId = new FormControl(null);
    this.ecrisConvId = new FormControl(null);
    this.ecrisUpdConvTypeId = new FormControl(null);
    this.ecrisUpdConvId = new FormControl(null);
    this.isAdministrative = new FormControl(null);
    this.convDecisionDate = new FormControl(null);
    this.convDecFinalDate = new FormControl(null);
    this.sequentialIndex = new FormControl(null);
    this.destroyedDate = new FormControl(null);
    this.personId = new FormControl(null);
    this.version = new FormControl(null);
    this.address = new AddressForm();
    this.statusCode = new FormControl(null);
    this.oldId = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      // countryId: this.countryId,
      countryLookup: this.countryLookup.group,
      docTypeId: this.docTypeId,
      sanctionTypeId: this.sanctionTypeId,
      receiveDate: this.receiveDate,
      issueDate: this.issueDate,
      countryDescr: this.countryDescr,
      person:this.person.group,
      offenceStartDate: this.offenceStartDate,
      offenceEndDate: this.offenceEndDate,
      annotation: this.annotation,
      gdkpNumber: this.gdkpNumber,
      gdkpDate: this.gdkpDate,
      gdkpCaseNumber: this.gdkpCaseNumber,
      gdkpTom: this.gdkpTom,
      gdkpStr: this.gdkpStr,
      njrCountry: this.njrCountry,
      njrIdentifier: this.njrIdentifier,
      njrFirstId: this.njrFirstId,
      ecrisMsgId: this.ecrisMsgId,
      ecrisConvId: this.ecrisConvId,
      ecrisUpdConvTypeId: this.ecrisUpdConvTypeId,
      ecrisUpdConvId: this.ecrisUpdConvId,
      isAdministrative: this.isAdministrative,
      convDecisionDate: this.convDecisionDate,
      convDecFinalDate: this.convDecFinalDate,
      sequentialIndex: this.sequentialIndex,
      destroyedDate: this.destroyedDate,
      personId: this.personId,
      address: this.address.group,
      statusCode: this.statusCode,
      oldId: this.oldId,
    });
  }
}
