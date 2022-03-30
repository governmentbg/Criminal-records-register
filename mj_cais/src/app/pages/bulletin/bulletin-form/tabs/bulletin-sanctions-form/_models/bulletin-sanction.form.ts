import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";

export class BulletinSanctionForm {
  public group: FormGroup;
  public id: FormControl;
  public sanctCategoryId: FormControl;
  public sanctCategoryName: FormControl;
  public sanctProbCategId: FormControl;
  public sanctProbCategName: FormControl;
  public ecrisSanctCategId: FormControl;
  public ecrisSanctCategName: FormControl;
  public sanctProbValue: FormControl;
  public sanctProbMeasureId: FormControl;
  public sanctProbMeasureName: FormControl;
  public descr: FormControl;
  public specificToMinor: FormControl;
  public decisionStartDate: FormControl;
  public decisionEndDate: FormControl;
  public decisionDurationYears: FormControl;
  public decisionDurationMonths: FormControl;
  public decisionDurationDays: FormControl;
  public decisionDurationHours: FormControl;
  public executionStartDate: FormControl;
  public executionEndDate: FormControl;
  public executionDurationYears: FormControl;
  public executionDurationMonths: FormControl;
  public executionDurationDays: FormControl;
  public executionDurationHours: FormControl;
  public fineAmount: FormControl;
  public detenctionDescr: FormControl;
  public suspentionDurationYears: FormControl;
  public suspentionDurationMonths: FormControl;
  public suspentionDurationDays: FormControl;
  public suspentionDurationHours: FormControl;
  public probationDescr: FormControl;
  public sanctActivityId: FormControl;
  public sanctActivityName: FormControl;
  public sanctActivityDescr: FormControl;

  constructor() {
    var guid = Guid.create().toString();
    this.id = new FormControl(guid, [Validators.required]);
    this.sanctCategoryId = new FormControl(null, [Validators.required]);
    this.sanctCategoryName = new FormControl(null);
    this.sanctProbCategId = new FormControl(null, [Validators.maxLength(50)]);
    this.sanctProbCategName = new FormControl(null);
    this.ecrisSanctCategId = new FormControl(null, [Validators.maxLength(50)]);
    this.ecrisSanctCategName = new FormControl(null);
    this.sanctProbValue = new FormControl(null);
    this.sanctProbMeasureId = new FormControl(null, [Validators.maxLength(50)]);
    this.sanctProbMeasureName = new FormControl(null);
    this.descr = new FormControl(null);
    this.specificToMinor = new FormControl(null);
    this.decisionStartDate = new FormControl(null);
    this.decisionEndDate = new FormControl(null);
    this.decisionDurationYears = new FormControl(null);
    this.decisionDurationMonths = new FormControl(null);
    this.decisionDurationDays = new FormControl(null);
    this.decisionDurationHours = new FormControl(null);
    this.executionStartDate = new FormControl(null);
    this.executionEndDate = new FormControl(null);
    this.executionDurationYears = new FormControl(null);
    this.executionDurationMonths = new FormControl(null);
    this.executionDurationDays = new FormControl(null);
    this.executionDurationHours = new FormControl(null);
    this.fineAmount = new FormControl(null, [Validators.required]);
    this.detenctionDescr = new FormControl(null);
    this.suspentionDurationYears = new FormControl(null);
    this.suspentionDurationMonths = new FormControl(null);
    this.suspentionDurationDays = new FormControl(null);
    this.suspentionDurationHours = new FormControl(null);
    this.probationDescr = new FormControl(null, [Validators.required]);
    this.sanctActivityId = new FormControl(null, [Validators.maxLength(50)]);
    this.sanctActivityName = new FormControl(null);
    this.sanctActivityDescr = new FormControl(null);

    this.group = new FormGroup({
      id: this.id,
      sanctCategoryId: this.sanctCategoryId,
      sanctCategoryName: this.sanctCategoryName,
      sanctProbCategId: this.sanctProbCategId,
      sanctProbCategName: this.sanctProbCategName,
      ecrisSanctCategId: this.ecrisSanctCategId,
      ecrisSanctCategName: this.ecrisSanctCategName,
      sanctProbValue: this.sanctProbValue,
      sanctProbMeasureId: this.sanctProbMeasureId,
      sanctProbMeasureName: this.sanctProbMeasureName,
      descr: this.descr,
      specificToMinor: this.specificToMinor,
      decisionStartDate: this.decisionStartDate,
      decisionEndDate: this.decisionEndDate,
      decisionDurationYears: this.decisionDurationYears,
      decisionDurationMonths: this.decisionDurationMonths,
      decisionDurationDays: this.decisionDurationDays,
      decisionDurationHours: this.decisionDurationHours,
      executionStartDate: this.executionStartDate,
      executionEndDate: this.executionEndDate,
      executionDurationYears: this.executionDurationYears,
      executionDurationMonths: this.executionDurationMonths,
      executionDurationDays: this.executionDurationDays,
      executionDurationHours: this.executionDurationHours,
      fineAmount: this.fineAmount,
      detenctionDescr: this.detenctionDescr,
      suspentionDurationYears: this.suspentionDurationYears,
      suspentionDurationMonths: this.suspentionDurationMonths,
      suspentionDurationDays: this.suspentionDurationDays,
      suspentionDurationHours: this.suspentionDurationHours,
      probationDescr: this.probationDescr,
      sanctActivityId: this.sanctActivityId,
      sanctActivityName: this.sanctActivityName,
      sanctActivityDescr: this.sanctActivityDescr,
    });
  }
}
