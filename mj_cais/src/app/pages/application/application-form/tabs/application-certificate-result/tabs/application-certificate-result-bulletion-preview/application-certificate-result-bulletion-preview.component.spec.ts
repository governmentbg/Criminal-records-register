import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationCertificateResultBulletionPreviewComponent } from './application-certificate-result-bulletion-preview.component';

describe('ApplicationCertificateResultBulletionPreviewComponent', () => {
  let component: ApplicationCertificateResultBulletionPreviewComponent;
  let fixture: ComponentFixture<ApplicationCertificateResultBulletionPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationCertificateResultBulletionPreviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationCertificateResultBulletionPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
