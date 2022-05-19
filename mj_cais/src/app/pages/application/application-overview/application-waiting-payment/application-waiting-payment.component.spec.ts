import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationWaitingPaymentComponent } from './application-waiting-payment.component';

describe('ApplicationWaitingPaymentComponent', () => {
  let component: ApplicationWaitingPaymentComponent;
  let fixture: ComponentFixture<ApplicationWaitingPaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationWaitingPaymentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationWaitingPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
