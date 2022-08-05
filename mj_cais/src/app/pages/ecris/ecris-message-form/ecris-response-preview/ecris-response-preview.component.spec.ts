import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EcrisResponsePreviewComponent } from './ecris-response-preview.component';

describe('EcrisResponsePreviewComponent', () => {
  let component: EcrisResponsePreviewComponent;
  let fixture: ComponentFixture<EcrisResponsePreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EcrisResponsePreviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EcrisResponsePreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
