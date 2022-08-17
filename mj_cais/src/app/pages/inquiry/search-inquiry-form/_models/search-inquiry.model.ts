import { BaseModel } from "../../../../@core/models/common/base.model";

export class SearchInquiryModel extends BaseModel {
  public registrationNumber: string;
  public statusCodeDisplayValue: string;
  public validFrom: Date;
  public validTo: Date;
  public repApplRegistrationNumber: string;
  public applicantData: string;
  public personIdentificators: string;
  public names: string;
  public purpose: string;
  public firstSigner: string;
  public secondSigner: string;

  constructor(init?: Partial<SearchInquiryModel>) {
    super(init);
    if (init) {
      this.registrationNumber = init.registrationNumber ?? null;
      this.statusCodeDisplayValue = init.statusCodeDisplayValue ?? null;
      this.validFrom = init.validFrom ?? null;
      this.validTo = init.validTo ?? null;
      this.repApplRegistrationNumber =
        init.repApplRegistrationNumber ?? null;
      this.applicantData = init.applicantData ?? null;
      this.personIdentificators = init.personIdentificators ?? null;
      this.names = init.names ?? null;
      this.purpose = init.purpose ?? null;
      this.firstSigner = init.firstSigner ?? null;
      this.secondSigner = init.secondSigner ?? null;
    }
  }
}
