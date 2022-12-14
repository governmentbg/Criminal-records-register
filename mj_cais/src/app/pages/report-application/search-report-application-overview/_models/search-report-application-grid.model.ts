import { BaseGridModel } from "../../../../@core/models/common/base-grid.model";

export class SearchReportApplicationGridModel extends BaseGridModel {
  public registrationNumber: string;
  public egn: string;
  public statusCodeDisplayValue: string;
  public validFrom: Date;
  public validTo: Date;
  public reportApplicationRegistrationNumber: string;
  public applicantData: string;
  public personIdentificators: string;
  public names: string;
  public purpose: string;
  public firstSigner: string;
  public secondSigner: string;

  constructor(init?: Partial<SearchReportApplicationGridModel>) {
    super(init);
    if (init) {
      this.registrationNumber = init.registrationNumber ?? null;
      this.egn = init.egn ?? null;
      this.statusCodeDisplayValue = init.statusCodeDisplayValue ?? null;
      this.validFrom = init.validFrom ?? null;
      this.validTo = init.validTo ?? null;
      this.reportApplicationRegistrationNumber =
        init.reportApplicationRegistrationNumber ?? null;
      this.applicantData = init.applicantData ?? null;
      this.personIdentificators = init.personIdentificators ?? null;
      this.names = init.names ?? null;
      this.purpose = init.purpose ?? null;
      this.firstSigner = init.firstSigner ?? null;
      this.secondSigner = init.secondSigner ?? null;
    }
  }
}
