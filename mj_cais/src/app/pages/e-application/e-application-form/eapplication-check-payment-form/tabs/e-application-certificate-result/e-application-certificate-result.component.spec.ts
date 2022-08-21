import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EApplicationCertificateResultComponent } from './e-application-certificate-result.component';

describe('EApplicationCertificateResultComponent', () => {
  let component: EApplicationCertificateResultComponent;
  let fixture: ComponentFixture<EApplicationCertificateResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EApplicationCertificateResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EApplicationCertificateResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
