import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { NgxSpinnerService } from "ngx-spinner";
import { FormUtils } from "../../../../utils/form.utils";
import { LookupForm } from "./models/lookup.form";

@Component({
  selector: "cais-lookup",
  templateUrl: "./lookup.component.html",
  styleUrls: ["./lookup.component.scss"],
})
export class LookupComponent implements OnInit {
  @Input() label: string;
  @Input() inputControl: LookupForm;
  @Input() parentGroup: FormGroup;
  //@Input() isLoading: boolean = false;
  @Input() openFunction: () => any;
  @Output() onClearSelection: EventEmitter<void> = new EventEmitter<void>();

  inputId: string = "id";
  inputName: string = "displayName";

  //private interval;

  constructor(
    public formUtils: FormUtils,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {}

  // ngOnDestroy() {
  //   if (this.interval) {
  //     clearInterval(this.interval);
  //   }
  // }

  public openLookupFunction() {
    this.spinner.show();
    setTimeout(() => {
      /** spinner ends after 5 seconds */
      this.spinner.hide();
    }, 500);

    // todo: get isLoading from dialog component?
    // this.interval = setInterval(() => {
    //   let isHiden = this.hideSpinner();
    //   if (isHiden) {
    //     clearInterval(this.interval);
    //   }
    // }, 500);
    this.openFunction();
  }

  public setInvalidContainer(): string {
    return this.inputControl.displayName.invalid &&
      this.inputHasBeenInteracted()
      ? "ng-invalid"
      : "";
  }

  public validationCss(): string {
    let result =
      this.inputControl.displayName.invalid && this.inputHasBeenInteracted()
        ? "status-danger"
        : "";

    return result;
  }

  public inputHasBeenInteracted(): boolean {
    return (
      this.inputControl.displayName.touched ||
      this.inputControl.displayName.dirty
    );
  }

 public clearSelection(): void {
    this.inputControl.id.setValue(null);
    this.inputControl.displayName.setValue(null);
    this.onClearSelection.emit();
  }

  // private hideSpinner(): boolean {
  //   if (!this.isLoading) {
  //     this.spinner.hide();
  //     return true;
  //   }
  //   return false;
  // }
}
