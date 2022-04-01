import { Component, Input, OnInit } from '@angular/core';
import { IsinDataPreviewModel } from '../_models/isin-data-preview.model';

@Component({
  selector: 'cais-isin-data-form',
  templateUrl: './isin-data-form.component.html',
  styleUrls: ['./isin-data-form.component.scss']
})
export class IsinDataFormComponent implements OnInit {

  @Input() model: IsinDataPreviewModel;
  constructor() { }

  ngOnInit(): void {
  }

}
