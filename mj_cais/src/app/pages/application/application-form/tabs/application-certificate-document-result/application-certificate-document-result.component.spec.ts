import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationCertificateDocumentResultComponent } from './application-certificate-document-result.component';

describe('ApplicationCertificateDocumentResultComponent', () => {
  let component: ApplicationCertificateDocumentResultComponent;
  let fixture: ComponentFixture<ApplicationCertificateDocumentResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationCertificateDocumentResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationCertificateDocumentResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
