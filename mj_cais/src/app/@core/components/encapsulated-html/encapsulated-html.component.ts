import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'cais-encapsulated-html',
  templateUrl: './encapsulated-html.component.html',
  styleUrls: ['./encapsulated-html.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EncapsulatedHtmlComponent implements OnInit {

  @Input()
  Html = '';
  constructor() { }

  ngOnInit() {
  }

}
