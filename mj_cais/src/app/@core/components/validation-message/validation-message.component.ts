import { Component, forwardRef, Input, OnInit } from "@angular/core";
import { NG_VALUE_ACCESSOR } from "@angular/forms";

@Component({
  selector: "cais-validation-message",
  templateUrl: "./validation-message.component.html",
  styleUrls: ["./validation-message.component.scss"],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ValidationMessageComponent),
      multi: true,
    },
  ],
})
export class ValidationMessageComponent {
  @Input() label: string = "";
  @Input() showRequired?: boolean;
  @Input() min?: number;
  @Input() showMin?: boolean;
  @Input() max?: number;
  @Input() showMax: boolean;
  @Input() minLength?: number;
  @Input() showMinLength?: boolean;
  @Input() maxLength?: number;
  @Input() showMaxLength?: boolean;
  @Input() showPattern?: boolean;
  @Input() showEgn: boolean;
  @Input() showLnch: boolean;
  @Input() showAcceptedValues: boolean;
  @Input() showGridMessage: boolean;
  @Input() showCyrillicPattern: boolean;
  @Input() showEmailPattern: boolean;
}
