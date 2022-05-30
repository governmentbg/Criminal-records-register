import { AddressModel } from "../../../../@core/components/forms/address-form/_model/address.model";
import { LookupModel } from "../../../../@core/components/forms/inputs/lookup/models/lookup.model";
import { PersonModel } from "../../../../@core/components/forms/person-form/_models/person.model";
import { BaseModel } from "../../../../@core/models/common/base.model";

export class FbbcModel extends BaseModel {
  public countryLookup: LookupModel;
  public docTypeId: string = null;
  public sanctionTypeId: string = null;
  public receiveDate: Date = null;
  public issueDate: Date = null;
  public countryDescr: string = null;
  public offenceStartDate: Date = null;
  public offenceEndDate: Date = null;
  public annotation: string = null;
  public person: PersonModel = null;
  public gdkpNumber: string = null;
  public gdkpDate: Date = null;
  public gdkpCaseNumber: string = null;
  public gdkpTom: string = null;
  public gdkpStr: string = null;
  public njrCountry: string = null;
  public njrIdentifier: string = null;
  public njrFirstId: string = null;
  public ecrisMsgId: string = null;
  public ecrisConvId: string = null;
  public ecrisUpdConvTypeId: string = null;
  public ecrisUpdConvId: string = null;
  public isAdministrative: number = null;
  public convDecisionDate: Date = null;
  public convDecFinalDate: Date = null;
  public sequentialIndex: number = null;
  public destroyedDate: Date = null;
  public personId: string = null;
  public version: number = null;
  public address: AddressModel = new AddressModel();
  public statusCode: string = null;
  public oldId: number = null;

  constructor(init?: Partial<FbbcModel>) {
    super(init);
    if (init) {
      this.countryLookup = init.countryLookup ?? null;
      this.docTypeId = init.docTypeId ?? null;
      this.sanctionTypeId = init.sanctionTypeId ?? null;
      this.receiveDate = init.receiveDate ?? null;
      this.issueDate = init.issueDate ?? null;
      this.countryDescr = init.countryDescr ?? null;
      this.person = init.person ?? null;
      this.offenceStartDate = init.offenceStartDate ?? null;
      this.offenceEndDate = init.offenceEndDate ?? null;
      this.annotation = init.annotation ?? null;
      this.gdkpNumber = init.gdkpNumber ?? null;
      this.gdkpDate = init.gdkpDate ?? null;
      this.gdkpCaseNumber = init.gdkpCaseNumber ?? null;
      this.gdkpTom = init.gdkpTom ?? null;
      this.gdkpStr = init.gdkpStr ?? null;
      this.njrCountry = init.njrCountry ?? null;
      this.njrIdentifier = init.njrIdentifier ?? null;
      this.njrFirstId = init.njrFirstId ?? null;
      this.ecrisMsgId = init.ecrisMsgId ?? null;
      this.ecrisConvId = init.ecrisConvId ?? null;
      this.ecrisUpdConvTypeId = init.ecrisUpdConvTypeId ?? null;
      this.ecrisUpdConvId = init.ecrisUpdConvId ?? null;
      this.isAdministrative = init.isAdministrative ?? null;
      this.convDecisionDate = init.convDecisionDate ?? null;
      this.convDecFinalDate = init.convDecFinalDate ?? null;
      this.sequentialIndex = init.sequentialIndex ?? null;
      this.destroyedDate = init.destroyedDate ?? null;
      this.personId = init.personId ?? null;
      this.version = init.version ?? null;
      this.address = init.address ?? new AddressModel();
      this.statusCode = init.statusCode ?? null;
      this.oldId = init.oldId ?? null;
    }
  }
}
