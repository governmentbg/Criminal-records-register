import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NbDialogRef } from '@nebular/theme';

@Component({
  selector: 'cais-cancel-dialog',
  templateUrl: './cancel-dialog.component.html',
  styleUrls: ['./cancel-dialog.component.scss']
})
export class CancelDialogComponent implements OnInit {
  formGroup: FormGroup;
  constructor(protected ref: NbDialogRef<CancelDialogComponent>,private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.formGroup = this.buildFormImpl();
  }

  buildFormImpl(): FormGroup {
    return this.formBuilder.group({
        description: [{ value: "", disabled: false },Validators.required,],
    });
}

  success() {
    this.ref.close(this.formGroup.value);
  }

  dismiss() {
    this.ref.close(false);
  }

}
