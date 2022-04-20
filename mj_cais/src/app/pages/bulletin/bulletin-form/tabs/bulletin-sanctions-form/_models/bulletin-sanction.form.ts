import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class BulletinSanctionForm {
  public group: FormGroup;
  public id: FormControl;
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
  public suspentionDurationHours: FormControl;

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.sanctCategoryId = new FormControl(null, [Validators.required]);
    this.sanctCategoryName = new FormControl(null);
    this.ecrisSanctCategId = new FormControl(null);
    this.ecrisSanctCategName = new FormControl(null);
    this.descr = new FormControl(null);
    this.decisionDurationYears = new FormControl(null);
    this.decisionDurationMonths = new FormControl(null);
    this.decisionDurationDays = new FormControl(null);
    this.decisionDurationHours = new FormControl(null);
    this.fineAmount = new FormControl(null, [Validators.required]);
    this.detenctionDescr = new FormControl(null);
    this.suspentionDurationYears = new FormControl(null);
    this.suspentionDurationMonths = new FormControl(null);
    this.suspentionDurationDays = new FormControl(null);
    this.suspentionDurationHours = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
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
      suspentionDurationHours: this.suspentionDurationHours,
    });
  }
}
