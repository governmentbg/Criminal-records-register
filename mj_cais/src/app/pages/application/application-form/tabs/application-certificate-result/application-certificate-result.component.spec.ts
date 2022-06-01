import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationCertificateResultComponent } from './application-certificate-result.component';

describe('ApplicationCertificateResultComponent', () => {
  let component: ApplicationCertificateResultComponent;
  let fixture: ComponentFixture<ApplicationCertificateResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationCertificateResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationCertificateResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
