import { BaseGridModel } from "../../../../../@core/models/common/base-grid.model";

export class ApplicationSearchGridModel extends BaseGridModel {
  public certificateRegistrationNumber: string;
  public statusCode: string;
  public validFrom: Date;
  public validTo: Date;
  public registrationNumber: string;
  public personIdentificator: string;
  public names: string;


  public CsAuthorityId: string;


  constructor(init?: Partial<ApplicationSearchGridModel>) {
    super(init);
    if (init) {
        this.certificateRegistrationNumber = init.certificateRegistrationNumber ?? null;
        this.statusCode = init.statusCode ?? null;
        this.validFrom = init.validFrom ?? null;
        this.validTo = init.validTo ?? null;
        this.registrationNumber = init.registrationNumber ?? null;
        this.personIdentificator = init.personIdentificator ?? null;
        this.names = init.names ?? null;



        this.CsAuthorityId = init.CsAuthorityId ?? null;
    }
  }
}
