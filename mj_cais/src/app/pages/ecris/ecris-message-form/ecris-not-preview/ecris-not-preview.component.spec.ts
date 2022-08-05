import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EcrisNotPreviewComponent } from './ecris-not-preview.component';

describe('EcrisNotPreviewComponent', () => {
  let component: EcrisNotPreviewComponent;
  let fixture: ComponentFixture<EcrisNotPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EcrisNotPreviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EcrisNotPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
