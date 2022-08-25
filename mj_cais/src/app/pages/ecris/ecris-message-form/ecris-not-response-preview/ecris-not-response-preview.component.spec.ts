import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EcrisNotResponsePreviewComponent } from './ecris-not-response-preview.component';

describe('EcrisNotResponsePreviewComponent', () => {
  let component: EcrisNotResponsePreviewComponent;
  let fixture: ComponentFixture<EcrisNotResponsePreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EcrisNotResponsePreviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EcrisNotResponsePreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
