import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationCertificateCanceledComponent } from './application-certificate-canceled.component';

describe('ApplicationCertificateCanceledComponent', () => {
  let component: ApplicationCertificateCanceledComponent;
  let fixture: ComponentFixture<ApplicationCertificateCanceledComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationCertificateCanceledComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationCertificateCanceledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
