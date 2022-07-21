import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EcrisReqPreviewComponent } from './ecris-req-preview.component';

describe('EcrisReqPreviewComponent', () => {
  let component: EcrisReqPreviewComponent;
  let fixture: ComponentFixture<EcrisReqPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EcrisReqPreviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EcrisReqPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
