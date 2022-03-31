import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EcrisReqWaitingFormComponent } from './ecris-req-waiting-form.component';

describe('EcrisReqWaitingFormComponent', () => {
  let component: EcrisReqWaitingFormComponent;
  let fixture: ComponentFixture<EcrisReqWaitingFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EcrisReqWaitingFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EcrisReqWaitingFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
