import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'cais-ecris-not-descision',
  templateUrl: './ecris-not-descision.component.html',
  styleUrls: ['./ecris-not-descision.component.scss']
})
export class EcrisNotDescisionComponent implements OnInit {

  @Input()
  public decisions: any;

  formGroup: FormGroup;
  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    debugger;
    this.formGroup = this.buildFormImpl();
    this.formGroup.patchValue(this.decisions);
  }

  buildFormImpl(): FormGroup {
    return this.formBuilder.group({
      decisionDate: [{ value: "", disabled: true }],
      decisionChangeType: [{ value: "", disabled: true }],
      decidingAuthorityName: [{ value: "", disabled: true }],
    });
  }
}
