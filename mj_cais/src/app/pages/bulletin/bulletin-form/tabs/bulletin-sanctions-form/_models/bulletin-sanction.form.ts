import { FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseForm } from "../../../../../../@core/models/common/base.form";

export class BulletinSanctionForm extends BaseForm {
  public group: FormGroup;

  public sanctCategoryId: FormControl;
  public sanctCategoryName: FormControl;
  public ecrisSanctCategId: FormControl;
  public ecrisSanctCategName: FormControl;
  public descr: FormControl;
  public decisionDurationYears: FormControl;
  public decisionDurationMonths: FormControl;
  public decisionDurationDays: FormControl;
  public decisionDurationHours: FormControl;
  public fineAmount: FormControl;
  public detenctionDescr: FormControl;
  public suspentionDurationYears: FormControl;
  public suspentionDurationMonths: FormControl;
  public suspentionDurationDays: FormControl;
  public probationsTransactions: FormControl;
  public probations: FormControl;

  constructor() {
    super();
    this.sanctCategoryId = new FormControl(null);
    this.sanctCategoryName = new FormControl(null);
    this.ecrisSanctCategId = new FormControl(null);
    this.ecrisSanctCategName = new FormControl(null);
    this.descr = new FormControl(null, [Validators.maxLength(2000)]);
    this.decisionDurationYears = new FormControl(null);
    this.decisionDurationMonths = new FormControl(null);
    this.decisionDurationDays = new FormControl(null);
    this.decisionDurationHours = new FormControl(null);
    this.fineAmount = new FormControl(null);
    this.detenctionDescr = new FormControl(null, [Validators.maxLength(2000)]);
    this.suspentionDurationYears = new FormControl(null);
    this.suspentionDurationMonths = new FormControl(null);
    this.suspentionDurationDays = new FormControl(null);
    this.probationsTransactions = new FormControl(null);
    this.probations = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      version: this.version,
      sanctCategoryId: this.sanctCategoryId,
      sanctCategoryName: this.sanctCategoryName,
      ecrisSanctCategId: this.ecrisSanctCategId,
      ecrisSanctCategName: this.ecrisSanctCategName,
      descr: this.descr,
      decisionDurationYears: this.decisionDurationYears,
      decisionDurationMonths: this.decisionDurationMonths,
      decisionDurationDays: this.decisionDurationDays,
      decisionDurationHours: this.decisionDurationHours,
      fineAmount: this.fineAmount,
      detenctionDescr: this.detenctionDescr,
      suspentionDurationYears: this.suspentionDurationYears,
      suspentionDurationMonths: this.suspentionDurationMonths,
      suspentionDurationDays: this.suspentionDurationDays,
      probationsTransactions: this.probationsTransactions,
      probations: this.probations,
    });
  }
}
