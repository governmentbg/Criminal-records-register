import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NbDialogRef } from '@nebular/theme';
import { EcrisRequestPreviewService } from '../ecris-req-preview/_data/ecris-request-preview.service';

@Component({
  selector: 'cais-ecris-not-preview',
  templateUrl: './ecris-not-preview.component.html',
  styleUrls: ['./ecris-not-preview.component.scss']
})
export class EcrisNotPreviewComponent implements OnInit {
  displayTitle: string = 'Нотификация';
  ecrisId: string;
  ecrisType: string;
  formGroup: FormGroup; 
  constructor(
    private formBuilder: FormBuilder,
    private ecrisRequestPreviewService: EcrisRequestPreviewService,
    protected ref: NbDialogRef<EcrisNotPreviewComponent>,
  ) {}

  ngOnInit(): void {
   
    this.ecrisRequestPreviewService
      .getEcrisRequest(this.ecrisId,this.ecrisType)
      .subscribe((result) => {
        this.formGroup = this.buildFormImpl();
        this.formGroup.patchValue(result);
      });
  }

  buildFormImpl(): FormGroup {
    return this.formBuilder.group({
      id: [{ value: "", disabled: true }],
      ecrisId: [{ value: "", disabled: true }],
    
      firstName: [{ value: "", disabled: true }],
      middleName: [{ value: "", disabled: true }],
      lastName: [{ value: "", disabled: true }],
      lastNameSecond: [{ value: "", disabled: true }],
      fullName: [{ value: "", disabled: true }],
      nationality: [{ value: "", disabled: true }],
      countryPerson: [{ value: "", disabled: true }],
      municipalityPerson: [{ value: "", disabled: true }],
      cityPerson: [{ value: "", disabled: true }],
      personId: [{ value: "", disabled: true }],
      sex: [{ value: "", disabled: true }],
      birthday: [{ value: "", disabled: true }],
 
      country: [{ value: "", disabled: true }],
      municipality: [{ value: "", disabled: true }],
      city: [{ value: "", disabled: true }],
      street: [{ value: "", disabled: true }],
      postCode: [{ value: "", disabled: true }],
      fullAdress: [{ value: "", disabled: true }],
      adressNumber: [{ value: "", disabled: true }], 
      
      convictionSanctions: [{ value: "", disabled: true }], 

      decisions: [{ value: "", disabled: true }], 
    });
  }

  public dialogSimpleCancelFunction = () => {
    this.ref.close();
  };
}
